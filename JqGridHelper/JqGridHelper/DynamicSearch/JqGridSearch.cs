using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading;
using JqGridHelper.Models;
using JqGridHelper.Utils;
using Newtonsoft.Json;
using System.Linq.Dynamic; //Import the Dynamic LINQ library

namespace JqGridHelper.DynamicSearch
{
    //single field search
    //_search=true&nd=1403935889318&rows=10&page=1&sidx=Id&sord=asc&searchField=Id&searchString=4444&searchOper=eq&filters=

    //multi-field search
    //_search=true&nd=1403935941367&rows=10&page=1&sidx=Id&sord=asc&filters=%7B%22groupOp%22%3A%22AND%22%2C%22rules%22%3A%5B%7B%22field%22%3A%22Id%22%2C%22op%22%3A%22eq%22%2C%22data%22%3A%2244%22%7D%2C%7B%22field%22%3A%22SupplierID%22%2C%22op%22%3A%22eq%22%2C%22data%22%3A%221%22%7D%5D%7D&searchField=&searchString=&searchOper=
    // filters -> {"groupOp":"AND","rules":[{"field":"All","op":"cn","data":"fffff"},{"field":"Price","op":"bn","data":"ffff"}]}

    //toolbar search
    //_search=true&nd=1403935593036&rows=10&page=1&sidx=Id&sord=asc&Id=2&Name=333&SupplierID=1&CategoryID=1&Price=44

    /// <summary>
    /// this class is based on the default values of `prmNames`
    /// </summary>
    public class JqGridSearch
    {
        /// <summary>
        /// هر اپراتوری را به هر نوع داده‌ای نمی‌توان اعمال کرد
        /// </summary>
        private static readonly Dictionary<string, string> _validOperators =
                     new Dictionary<string, string>
					 {
						 { "Object"   ,  "" },
						 { "Boolean"  ,  "eq:ne:" },
						 { "Char"     ,  "" },
						 { "String"   ,  "eq:ne:lt:le:gt:ge:bw:bn:cn:nc:" },
						 { "SByte"    ,  "" },
						 { "Byte"     ,  "eq:ne:lt:le:gt:ge:" },
						 { "Int16"    ,  "eq:ne:lt:le:gt:ge:" },
						 { "UInt16"   ,  "" },
						 { "Int32"    ,  "eq:ne:lt:le:gt:ge:" },
						 { "UInt32"   ,  "" },
						 { "Int64"    ,  "eq:ne:lt:le:gt:ge:" },
						 { "UInt64"   ,  "" },
						 { "Decimal"  ,  "eq:ne:lt:le:gt:ge:" },
						 { "Single"   ,  "eq:ne:lt:le:gt:ge:" },
						 { "Double"   ,  "eq:ne:lt:le:gt:ge:" },
						 { "DateTime" ,  "eq:ne:lt:le:gt:ge:" },
						 { "TimeSpan" ,  "" },
						 { "Guid"     ,  "eq:ne:" }
					 };

        private static readonly Dictionary<string, string> _whereOperation =
                     new Dictionary<string, string>
					 {
						{"in" , " {0} = @{1} "},//is in
						{"eq" , " {0} = @{1} "},
						{"ni" , " {0} != @{1} "},//is not in
						{"ne" , " {0} != @{1} "},
						{"lt" , " {0} < @{1} "},
						{"le" , " {0} <= @{1} "},
						{"gt" , " {0} > @{1} "},
						{"ge" , " {0} >= @{1} "},
						{"bw" , " {0}.StartsWith(@{1}) "},//begins with
						{"bn" , " !{0}.StartsWith(@{1}) "},//does not begin with
						{"ew" , " {0}.EndsWith(@{1}) "},//ends with
						{"en" , " !{0}.EndsWith(@{1}) "},//does not end with
						{"cn" , " {0}.Contains(@{1}) "},//contains
						{"nc" , " !{0}.Contains(@{1}) "}//does not contain
					 };

        private readonly DateTimeType _dateTimeType;
        private readonly NameValueCollection _form;
        private readonly JqGridRequest _request;
        private int _parameterIndex;

        public JqGridSearch(JqGridRequest request, NameValueCollection form, DateTimeType dateTimeType)
        {
            _request = request;
            _form = form;
            _dateTimeType = dateTimeType;
        }
        public IQueryable<T> ApplyFilter<T>(IQueryable<T> query)
        {
            if (!_request._search)
                return query;

            if (!string.IsNullOrWhiteSpace(_request.searchString) &&
                !string.IsNullOrWhiteSpace(_request.searchOper) &&
                !string.IsNullOrWhiteSpace(_request.searchField))
            {
                return manageSingleFieldSearch(query);
            }

            return !string.IsNullOrWhiteSpace(_request.filters) ?
                    manageMultiFieldSearch(query) : manageToolbarSearch(query);
        }


        private static bool isNullable(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        private object getDefaultValue(Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        private Tuple<string, object> getPredicate<T>(string searchField, string searchOper, string searchValue)
        {
            var type = typeof(T).FindFieldType(searchField);
            if (type == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                var defaultValue = getDefaultValue(type);
                searchValue = defaultValue == null ? "null" : defaultValue.ToString();
            }

            var searchOperator = getSearchOperator(searchOper, searchField, type, searchValue);

            if (isNullable(type))
            {
                type = Nullable.GetUnderlyingType(type);
            }

            if (!_validOperators[type.Name].Contains(string.Format("{0}:", searchOper)))
            {
                throw new NotImplementedException(string.Format("{0} & {1} is not supported.", type.Name, searchOper));
            }

            if (type == typeof(decimal))
            {
                if (searchValue == "null")
                {
                    return new Tuple<string, object>(searchOperator, null);
                }

                decimal value;
                if (decimal.TryParse(searchValue, NumberStyles.Any, Thread.CurrentThread.CurrentCulture, out value))
                {
                    return new Tuple<string, object>(searchOperator, value);
                }
            }

            if (type == typeof(DateTime))
            {
                if (searchValue == "null")
                {
                    return new Tuple<string, object>(searchOperator, null);
                }

                DateTime dateTime;
                switch (_dateTimeType)
                {
                    case DateTimeType.Gregorian:
                        dateTime = DateTime.Parse(searchValue, CultureInfo.InvariantCulture);
                        break;
                    case DateTimeType.Persian:
                        var parts = searchValue.Split('/'); //ex. 1391/1/19
                        if (parts.Length != 3) return null;
                        var year = int.Parse(parts[0]);
                        var month = int.Parse(parts[1]);
                        var day = int.Parse(parts[2]);
                        dateTime = new DateTime(year, month, day, new PersianCalendar());
                        break;
                    default:
                        throw new NotSupportedException(string.Format("{0} is not supported.", _dateTimeType));
                }
                return new Tuple<string, object>(searchOperator, dateTime);
            }

            if (type == typeof(Guid))
            {
                var guid = searchValue == "null" ? (Guid?)null : new Guid(searchValue);
                return new Tuple<string, object>(searchOperator, guid);
            }

            var resultValue = Convert.ChangeType(searchValue, type);
            return new Tuple<string, object>(searchOperator, resultValue);
        }

        private string getSearchOperator(string ruleSearchOperator, string searchField, Type type, string searchValue)
        {
            string whereOperation;
            if (!_whereOperation.TryGetValue(ruleSearchOperator, out whereOperation))
            {
                throw new NotSupportedException(string.Format("{0} is not supported.", ruleSearchOperator));
            }

            if (isNullable(type) && (searchValue == "null"))
            {
                switch (ruleSearchOperator)
                {
                    case "eq":
                        whereOperation = " !{0}.HasValue ";
                        break;
                    case "ne":
                        whereOperation = " {0}.HasValue ";
                        break;
                    default:
                        throw new NotSupportedException(string.Format("{0} is not supported.", ruleSearchOperator));
                }
            }

            if (type == typeof(DateTime))
            {
                switch (ruleSearchOperator)
                {
                    case "eq":
                        whereOperation = " {0}.Date = @{1} ";
                        break;
                    case "ne":
                        whereOperation = " {0}.Date != @{1} ";
                        break;
                }
            }

            var searchOperator = string.Format(whereOperation, searchField, _parameterIndex);

            _parameterIndex++;
            return searchOperator;
        }

        private IQueryable<T> manageMultiFieldSearch<T>(IQueryable<T> query)
        {
            var filtersArray = JsonConvert.DeserializeObject<SearchFilter>(_request.filters);
            var groupOperator = filtersArray.groupOp;
            if (filtersArray.rules == null)
                return query;

            var valuesList = new List<object>();
            var filterExpression = String.Empty;
            foreach (var rule in filtersArray.rules)
            {
                var predicate = getPredicate<T>(rule.field, rule.op, rule.data);
                if (predicate == null)
                    continue;

                valuesList.Add(predicate.Item2);
                filterExpression = string.Format("{0}{1} {2} ", filterExpression, predicate.Item1, groupOperator);
            }

            if (string.IsNullOrWhiteSpace(filterExpression))
                return query;

            filterExpression = filterExpression.Remove(filterExpression.Length - groupOperator.Length - 2);
            query = query.Where(filterExpression, valuesList.ToArray());
            return query;
        }

        private IQueryable<T> manageSingleFieldSearch<T>(IQueryable<T> query)
        {
            var predicate = getPredicate<T>(_request.searchField, _request.searchOper, _request.searchString);
            if (predicate != null)
                query = query.Where(predicate.Item1, new[] { predicate.Item2 });
            return query;
        }

        private IQueryable<T> manageToolbarSearch<T>(IQueryable<T> query)
        {
            var filterExpression = String.Empty;
            var valuesList = new List<object>();
            foreach (var postDataKey in _form.AllKeys)
            {
                if (!postDataKey.Equals("nd") && !postDataKey.Equals("sidx")
                    && !postDataKey.Equals("sord") && !postDataKey.Equals("page")
                    && !postDataKey.Equals("rows") && !postDataKey.Equals("_search")
                    && _form[postDataKey] != null)
                {
                    var predicate = getPredicate<T>(postDataKey, "eq", _form[postDataKey]);
                    if (predicate == null)
                        continue;

                    valuesList.Add(predicate.Item2);
                    filterExpression = string.Format("{0}{1} And ", filterExpression, predicate.Item1);
                }
            }

            if (string.IsNullOrWhiteSpace(filterExpression))
            {
                //{_search=true&nd=1450767916380&rows=10&page=1&sidx=Id&sord=asc&searchField=Code&searchString=&searchOper=eq&filters=}
                var predicate = getPredicate<T>(_form["searchField"], _form["searchOper"], _form["searchString"]);
                if (predicate != null)
                {
                    valuesList.Add(predicate.Item2);
                    filterExpression = string.Format("{0}{1} And ", filterExpression, predicate.Item1);
                }
            }

            if (string.IsNullOrWhiteSpace(filterExpression))
                return query;

            filterExpression = filterExpression.Remove(filterExpression.Length - 5);
            var args = valuesList.ToArray();
            query = query.Where(filterExpression, args);
            return query;
        }
    }
}