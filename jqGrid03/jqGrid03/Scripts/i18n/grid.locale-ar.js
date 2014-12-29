;(function($){
/**
 * jqGrid Arabic Translation
 * 
 * http://trirand.com/blog/ 
 * Dual licensed under the MIT and GPL licenses:
 * http://www.opensource.org/licenses/mit-license.php
 * http://www.gnu.org/licenses/gpl.html
**/
$.jgrid = $.jgrid || {};
$.extend($.jgrid,{
	defaults : {
		recordtext: "تسجیل {0} - {1} على {2}",
		emptyrecords: "لا یوجد تسجیل",
		loadtext: "تحمیل...",
		pgtext : "صفحة {0} على {1}"
	},
	search : {
		caption: "بحث...",
		Find: "بحث",
		Reset: "إلغاء",
		odata: [{ oper:'eq', text:"یساوی"},{ oper:'ne', text:"یختلف"},{ oper:'lt', text:"أقل"},{ oper:'le', text:"أقل أو یساوی"},{ oper:'gt', text:"أکبر"},{ oper:'ge', text:"أکبر أو یساوی"},{ oper:'bw', text:"یبدأ بـ"},{ oper:'bn', text:"لا یبدأ بـ"},{ oper:'in', text:"est dans"},{ oper:'ni', text:"n'est pas dans"},{ oper:'ew', text:"ینته بـ"},{ oper:'en', text:"لا ینته بـ"},{ oper:'cn', text:"یحتوی"},{ oper:'nc', text:"لا یحتوی"},{ oper:'nu', text:'is null'},{ oper:'nn', text:'is not null'}],
		groupOps: [	{ op: "مع", text: "الکل" },	{ op: "أو",  text: "لا أحد" }],
		operandTitle : "Click to select search operation.",
		resetTitle : "Reset Search Value"
},
	edit : {
		addCaption: "اضافة",
		editCaption: "تحدیث",
		bSubmit: "تثبیث",
		bCancel: "إلغاء",
		bClose: "غلق",
		saveData: "تغیرت المعطیات هل ترید التسجیل ?",
		bYes: "نعم",
		bNo: "لا",
		bExit: "إلغاء",
		msg: {
			required: "خانة إجباریة",
			number: "سجل رقم صحیح",
			minValue: "یجب أن تکون القیمة أکبر أو تساوی 0",
			maxValue: "یجب أن تکون القیمة أقل أو تساوی 0",
			email: "برید غیر صحیح",
			integer: "سجل عدد طبییعی صحیح",
			url: "لیس عنوانا صحیحا. البدایة الصحیحة ('http://' أو 'https://')",
			nodefined : " لیس محدد!",
			novalue : " قیمة الرجوع مطلوبة!",
			customarray : "یجب على الدالة الشخصیة أن تنتج جدولا",
			customfcheck : "الدالة الشخصیة مطلوبة فی حالة التحقق الشخصی"
		}
	},
	view : {
		caption: "رأیت التسجیلات",
		bClose: "غلق"
	},
	del : {
		caption: "حذف",
		msg: "حذف التسجیلات المختارة ?",
		bSubmit: "حذف",
		bCancel: "إلغاء"
	},
	nav : {
		edittext: " ",
		edittitle: "تغییر التسجیل المختار",
		addtext:" ",
		addtitle: "إضافة تسجیل",
		deltext: " ",
		deltitle: "حذف التسجیل المختار",
		searchtext: " ",
		searchtitle: "بحث عن تسجیل",
		refreshtext: "",
		refreshtitle: "تحدیث الجدول",
		alertcap: "تحذیر",
		alerttext: "یرجى إختیار السطر",
		viewtext: "",
		viewtitle: "إظهار السطر المختار"
	},
	col : {
		caption: "إظهار/إخفاء الأعمدة",
		bSubmit: "تثبیث",
		bCancel: "إلغاء"
	},
	errors : {
		errcap : "خطأ",
		nourl : "لا یوجد عنوان محدد",
		norecords: "لا یوجد تسجیل للمعالجة",
		model : "عدد العناوین (colNames) <> عدد التسجیلات (colModel)!"
	},
	formatter : {
		integer : {thousandsSeparator: " ", defaultValue: '0'},
		number : {decimalSeparator:",", thousandsSeparator: " ", decimalPlaces: 2, defaultValue: '0,00'},
		currency : {decimalSeparator:",", thousandsSeparator: " ", decimalPlaces: 2, prefix: "", suffix:"", defaultValue: '0,00'},
		date : {
			dayNames:   [
				"الأحد", "الإثنین", "الثلاثاء", "الأربعاء", "الخمیس", "الجمعة", "السبت",
				"الأحد", "الإثنین", "الثلاثاء", "الأربعاء", "الخمیس", "الجمعة", "السبت"
			],
			monthNames: [
				"جانفی", "فیفری", "مارس", "أفریل", "مای", "جوان", "جویلیة", "أوت", "سبتمبر", "أکتوبر", "نوفمبر", "دیسمبر",
				"جانفی", "فیفری", "مارس", "أفریل", "مای", "جوان", "جویلیة", "أوت", "سبتمبر", "أکتوبر", "نوفمبر", "دیسمبر"
			],
			AmPm : ["صباحا","مساءا","صباحا","مساءا"],
			S: function (j) {return j == 1 ? 'er' : 'e';},
			srcformat: 'Y-m-d',
			newformat: 'd/m/Y',
			parseRe : /[#%\\\/:_;.,\t\s-]/,
			masks : {
				ISO8601Long:"Y-m-d H:i:s",
				ISO8601Short:"Y-m-d",
				ShortDate: "n/j/Y",
				LongDate: "l, F d, Y",
				FullDateTime: "l, F d, Y g:i:s A",
				MonthDay: "F d",
				ShortTime: "g:i A",
				LongTime: "g:i:s A",
				SortableDateTime: "Y-m-d\\TH:i:s",
				UniversalSortableDateTime: "Y-m-d H:i:sO",
				YearMonth: "F, Y"
			},
			reformatAfterEdit : false
		},
		baseLinkUrl: '',
		showAction: '',
		target: '',
		checkbox : {disabled:true},
		idName : 'id'
	}
});
})(jQuery);