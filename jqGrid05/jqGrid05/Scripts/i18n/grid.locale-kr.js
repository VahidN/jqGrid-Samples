;(function($){
/**
 * jqGrid English Translation
 * Tony Tomov tony@trirand.com
 * http://trirand.com/blog/ 
 * Dual licensed under the MIT and GPL licenses:
 * http://www.opensource.org/licenses/mit-license.php
 * http://www.gnu.org/licenses/gpl.html
**/
$.jgrid = $.jgrid || {};
$.extend($.jgrid,{
	defaults : {
		recordtext: "ë³´ê¸° {0} - {1} / {2}",
		emptyrecords: "ی‘œى‹œی•  ی–‰ى‌´ ى—†ىٹµë‹ˆë‹¤",
		loadtext: "ى،°یڑŒى¤‘...",
		pgtext : "یژکى‌´ى§€ {0} / {1}"
	},
	search : {
		caption: "ê²€ىƒ‰...",
		Find: "ى°¾ê¸°",
		Reset: "ى´ˆê¸°ی™”",
		odata: [{ oper:'eq', text:"ê°™ë‹¤"},{ oper:'ne', text:"ê°™ى§€ ى•ٹë‹¤"},{ oper:'lt', text:"ى‍‘ë‹¤"},{ oper:'le', text:"ى‍‘ê±°ë‚ک ê°™ë‹¤"},{ oper:'gt', text:"یپ¬ë‹¤"},{ oper:'ge', text:"یپ¬ê±°ë‚ک ê°™ë‹¤"},{ oper:'bw', text:"ë،œ ى‹œى‍‘ی•œë‹¤"},{ oper:'bn', text:"ë،œ ى‹œى‍‘ی•کى§€ ى•ٹëٹ”ë‹¤"},{ oper:'in', text:"ë‚´ى—گ ى‍ˆë‹¤"},{ oper:'ni', text:"ë‚´ى—گ ى‍ˆى§€ ى•ٹë‹¤"},{ oper:'ew', text:"ë،œ ëپ‌ë‚œë‹¤"},{ oper:'en', text:"ë،œ ëپ‌ë‚کى§€ ى•ٹëٹ”ë‹¤"},{ oper:'cn', text:"ë‚´ى—گ ى،´ى‍¬ی•œë‹¤"},{ oper:'nc', text:"ë‚´ى—گ ى،´ى‍¬ی•کى§€ ى•ٹëٹ”ë‹¤"},{ oper:'nu', text:'is null'},{ oper:'nn', text:'is not null'}],
		groupOps: [	{ op: "AND", text: "ى „ë¶€" },	{ op: "OR",  text: "ى‍„ى‌ک" }	],
		operandTitle : "Click to select search operation.",
		resetTitle : "Reset Search Value"
	},
	edit : {
		addCaption: "ی–‰ ى¶”ê°€",
		editCaption: "ی–‰ ىˆکى •",
		bSubmit: "ى „ى†،",
		bCancel: "ى·¨ى†Œ",
		bClose: "ë‹«ê¸°",
		saveData: "ى‍گë£Œê°€ ë³€ê²½ëگکى—ˆىٹµë‹ˆë‹¤! ى €ى‍¥ی•کى‹œê² ىٹµë‹ˆê¹Œ?",
		bYes : "ىکˆ",
		bNo : "ى•„ë‹ˆىک¤",
		bExit : "ى·¨ى†Œ",
		msg: {
			required:"ی•„ىˆکی•­ëھ©ى‍…ë‹ˆë‹¤",
			number:"ىœ یڑ¨ی•œ ë²ˆیک¸ë¥¼ ى‍…ë ¥ی•´ ى£¼ى„¸ىڑ”",
			minValue:"ى‍…ë ¥ê°’ى‌€ یپ¬ê±°ë‚ک ê°™ى•„ى•¼ ی•©ë‹ˆë‹¤",
			maxValue:"ى‍…ë ¥ê°’ى‌€ ى‍‘ê±°ë‚ک ê°™ى•„ى•¼ ی•©ë‹ˆë‹¤",
			email: "ىœ یڑ¨ی•کى§€ ى•ٹى‌€ ى‌´ë©”ى‌¼ى£¼ى†Œى‍…ë‹ˆë‹¤",
			integer: "ىœ یڑ¨ی•œ ىˆ«ى‍گë¥¼ ى‍…ë ¥ی•کى„¸ىڑ”",
			date: "ىœ یڑ¨ی•œ ë‚ ى§œë¥¼ ى‍…ë ¥ی•کى„¸ىڑ”",
			url: "ى‌€ ىœ یڑ¨ی•کى§€ ى•ٹى‌€ URLى‍…ë‹ˆë‹¤. ë¬¸ى‍¥ى•‍ى—گ ë‹¤ى‌Œë‹¨ى–´ê°€ ی•„ىڑ”ی•©ë‹ˆë‹¤('http://' or 'https://')",
			nodefined : " ى‌€ ى •ى‌کëڈ„ى§€ ى•ٹى•کىٹµë‹ˆë‹¤!",
			novalue : " ë°کی™کê°’ى‌´ ی•„ىڑ”ی•©ë‹ˆë‹¤!",
			customarray : "ى‚¬ىڑ©ى‍گى •ى‌ک ی•¨ىˆکëٹ” ë°°ى—´ى‌„ ë°کی™کی•´ى•¼ ی•©ë‹ˆë‹¤!",
			customfcheck : "Custom function should be present in case of custom checking!"
			
		}
	},
	view : {
		caption: "ی–‰ ى،°یڑŒ",
		bClose: "ë‹«ê¸°"
	},
	del : {
		caption: "ى‚­ى œ",
		msg: "ى„ یƒ‌ëگœ ی–‰ى‌„ ى‚­ى œی•کى‹œê² ىٹµë‹ˆê¹Œ?",
		bSubmit: "ى‚­ى œ",
		bCancel: "ى·¨ى†Œ"
	},
	nav : {
		edittext: "",
		edittitle: "ى„ یƒ‌ëگœ ی–‰ یژ¸ى§‘",
		addtext:"",
		addtitle: "ی–‰ ى‚½ى‍…",
		deltext: "",
		deltitle: "ى„ یƒ‌ëگœ ی–‰ ى‚­ى œ",
		searchtext: "",
		searchtitle: "ی–‰ ى°¾ê¸°",
		refreshtext: "",
		refreshtitle: "ê·¸ë¦¬ë“œ ê°±ى‹ ",
		alertcap: "ê²½ê³ ",
		alerttext: "ی–‰ى‌„ ى„ یƒ‌ی•کى„¸ىڑ”",
		viewtext: "",
		viewtitle: "ى„ یƒ‌ëگœ ی–‰ ى،°یڑŒ"
	},
	col : {
		caption: "ى—´ى‌„ ى„ یƒ‌ی•کى„¸ىڑ”",
		bSubmit: "ی™•ى‌¸",
		bCancel: "ى·¨ى†Œ"
	},
	errors : {
		errcap : "ىک¤ë¥ک",
		nourl : "ى„¤ى •ëگœ urlى‌´ ى—†ىٹµë‹ˆë‹¤",
		norecords: "ى²کë¦¬ی•  ی–‰ى‌´ ى—†ىٹµë‹ˆë‹¤",
		model : "colNamesى‌ک ê¸¸ى‌´ê°€ colModelê³¼ ى‌¼ى¹کی•کى§€ ى•ٹىٹµë‹ˆë‹¤!"
	},
	formatter : {
		integer : {thousandsSeparator: ",", defaultValue: '0'},
		number : {decimalSeparator:".", thousandsSeparator: ",", decimalPlaces: 2, defaultValue: '0.00'},
		currency : {decimalSeparator:".", thousandsSeparator: ",", decimalPlaces: 2, prefix: "", suffix:"", defaultValue: '0.00'},
		date : {
			dayNames:   [
				"Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat",
				"ى‌¼", "ى›”", "ی™”", "ىˆک", "ëھ©", "ê¸ˆ", "ی† "
			],
			monthNames: [
				"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
				"1ى›”", "2ى›”", "3ى›”", "4ى›”", "5ى›”", "6ى›”", "7ى›”", "8ى›”", "9ى›”", "10ى›”", "11ى›”", "12ى›”"
			],
			AmPm : ["am","pm","AM","PM"],
			S: function (j) {return j < 11 || j > 13 ? ['st', 'nd', 'rd', 'th'][Math.min((j - 1) % 10, 3)] : 'th'},
			srcformat: 'Y-m-d',
			newformat: 'm-d-Y',
			parseRe : /[#%\\\/:_;.,\t\s-]/,
			masks : {
				ISO8601Long:"Y-m-d H:i:s",
				ISO8601Short:"Y-m-d",
				ShortDate: "Y/j/n",
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