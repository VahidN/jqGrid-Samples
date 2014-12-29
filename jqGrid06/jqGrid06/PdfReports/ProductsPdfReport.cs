using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using jqGrid06.Models;
using PdfRpt.Core.Contracts;
using PdfRpt.Core.Helper;
using PdfRpt.FluentInterface;

namespace jqGrid06.PdfReports
{
    public class ProductsPdfReport
    {
        public IPdfReportData CreatePdfReport(IList<Product> dataSourceList, string[] hiddenColumns)
        {
            return new PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.RightToLeft);
                doc.Orientation(PageOrientation.Portrait);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Vahid", Application = "PdfRpt", Keywords = "Products Rpt.", Subject = "Test Rpt", Title = "Test" });
                doc.Compression(new CompressionSettings
                {
                    EnableCompression = true,
                    EnableFullCompression = true
                });
            })
            .DefaultFonts(fonts =>
            {
                fonts.Path(System.IO.Path.Combine(AppPath.ApplicationPath, "Content\\fonts\\irsans.ttf"),
                           System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\verdana.ttf"));
                fonts.Size(9);
                fonts.Color(System.Drawing.Color.Black);
            })
            .PagesFooter(footer =>
            {
                footer.DefaultFooter("تاریخ: " + DateTime.Now.ToPersianDateTime().FixWeakCharacters(),
                     PdfRunDirection.RightToLeft);
            })
            .PagesHeader(header =>
            {
                header.DefaultHeader(defaultHeader =>
                {
                    defaultHeader.RunDirection(PdfRunDirection.RightToLeft);
                    defaultHeader.ImagePath(System.IO.Path.Combine(AppPath.ApplicationPath, "Content\\Images\\01.png"));
                    defaultHeader.Message("گزارش محصولات");
                });
            })
            .MainTableTemplate(template =>
            {
                template.BasicTemplate(BasicTemplate.ClassicTemplate);
            })
            .MainTablePreferences(table =>
            {
                table.ColumnsWidthsType(TableColumnWidthType.Relative);
            })
            .MainTableDataSource(dataSource =>
            {
                dataSource.StronglyTypedList(dataSourceList);
            })
            .MainTableSummarySettings(summarySettings =>
            {
                summarySettings.PreviousPageSummarySettings("منقول از صفحه قبل");
                summarySettings.PageSummarySettings("جمع صفحه");
                summarySettings.OverallSummarySettings("جمع");
            })
            .MainTableColumns(columns =>
            {
                columns.AddColumn(column =>
                {
                    column.PropertyName("rowNo");
                    column.IsRowNumber(true);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(0);
                    column.Width(1);
                    column.HeaderCell("ردیف");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<Product>(x => x.Id);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(hiddenColumns.All(col => col != "Id"));
                    column.Order(1);
                    column.Width(2);
                    column.HeaderCell("شماره");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<Product>(x => x.Name);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(hiddenColumns.All(col => col != "Name"));
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("نام", horizontalAlignment: HorizontalAlignment.Center);
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<Product>(x => x.AddDate);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(hiddenColumns.All(col => col != "AddDate"));
                    column.Order(3);
                    column.Width(3);
                    column.HeaderCell("تاریخ ثبت");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : ((DateTime)obj).ToPersianDateTime(includeHourMinute: false));
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<Product>(x => x.Price);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(hiddenColumns.All(col => col != "Price"));
                    column.Order(4);
                    column.Width(2);
                    column.HeaderCell("قیمت");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                    column.AggregateFunction(aggregateFunction =>
                    {
                        aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                        aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                });

            })
            .MainTableEvents(events =>
            {
                events.DataSourceIsEmpty(message: "اطلاعاتی برای نمایش یافت نشد.");
            })
            .Export(export =>
            {
                export.ToExcel();
            })
            .Generate(data =>
            {
                var fileName = string.Format("{0}-گزارش محصولات.pdf", Guid.NewGuid().ToString("N"));
                fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
                data.FlushInBrowser(fileName, FlushType.Attachment);
            }); // creating an in-memory PDF file
        }
    }
}