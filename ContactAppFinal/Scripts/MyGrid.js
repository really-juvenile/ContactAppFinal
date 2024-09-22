//$(document).ready(function () {
//    $("#grid").jqGrid({
//        url: "/ContactDetails/GetData",
//        datatype: "json",
//        colNames: ["Type", "Value"],
//        colModel: [{ name: "Type" }, { name: "Value" }], /*hidden: true },*/
//        //{ name: "Type", editable: true, searchoptions: { sopt: ['eq'] } },
//        //{ name: "Value", editable: true },

//        /*],*/
//        height: "250",
//        caption: "Contact Details",
//        loadonce: true,
//        jsonReader: {
//            root: function (obj) { return obj; },
//            repeatitems: false
//        }
//        //pager: "#pager",
//        //rowNum: 5,
//        //rowList: [5, 10, 15],
//        //sortname: 'id',
//        //sortorder: 'asc',
//        //viewrecords: true,
//        //width: 650,

//        //gridComplete: function () {
//        //    $("#grid").jqGrid('navGrid', '#pager', { edit: true, add: true, del: true, search: true, refresh: true },
//        //        {
//        //            //edit
//        //            url: "/ContactDetails/Edit",
//        //            closeAfterEdit: true,
//        //            width: 600,

//        //            afterSubmit: function (response, postdata) {
//        //                var result = JSON.parse(response.responseText);
//        //                if (result.success) {
//        //                    alert(result.message);
//        //                    return [true];
//        //                } else {
//        //                    alert(result.message);
//        //                    return [false];
//        //                }
//        //            }
//        //        },
//        //        {

//        //            url: "/ContactDetails/Add",
//        //            closeAfterAdd: true,
//        //            width: 600,

//        //            aftersubmit: function (response, postdata) {
//        //                var result = JSON.parse(response.responseText);
//        //                if (result.success) {
//        //                    alert(result.message);
//        //                    return [true];

//        //                }
//        //                else {
//        //                    alert(result.message);
//        //                    return [false];
//        //                }
//        //            }


//        //        },
//        //        {
//        //            url: "/ContactDetails/Delete",

//        //            aftersubmit: function (response, postdata) {
//        //                var result = JSON.parse(response.responseText);
//        //                if (result.success) {
//        //                    alert(result.message);
//        //                    return [true];
//        //                } else {
//        //                    alert(result.message);
//        //                    return [false];
//        //                }
//        //            }


//        //        },
//        //        {
//        //            multipleSearch: false,
//        //            closeAfterSearch: true
//        //        }
//        //    );


//    //}
//    })
//})

function loadContactDetails(contactId) {
    $("#grid").jqGrid({
        url: "/ContactDetails/GetData?contactId=" + contactId, // Pass contactId to the server
        datatype: "json",
        colNames: ["Type", "Value"],
        colModel: [
            { name: "Type" },
            { name: "Value" }
        ],
        height: "250",
        caption: "Contact Details",
        loadonce: true,
        jsonReader: {
            root: function (obj) { return obj; },
            repeatitems: false
        }
    }).trigger('reloadGrid'); // Refresh the grid after setting the new URL
}
