$(document).ready(function () {
    //var urlParams = new URLSearchParams(window.location.search);
    //var contactId = urlParams.get('contactId'); // Extract the contactId
    //// Debugging: Log the contactId to ensure it's being passed correctly
    /*console.log("ContactId being passed to jqGrid: ", contactId);*/

    /*if (contactId) {*/
        $("#grid").jqGrid({
            url: "/ContactDetails/GetData",
            datatype: "json",
            colNames: ["Id","Type","Value"],
            colModel: [{ name: "Id", key: true, hidden: true },
                { name: "Type", index: "Type", editable: true },
                { name: "Value", index: "Value", editable: true }
            ],
            /*loadonce: true,*/
            //jsonReader: {
            //    root: function (obj) { return obj; },
            //    repeatitems: false
            //}
            width: "500",
            height: "250",
            caption: "Contact Details Records",
            pager: "#pager",
            rowNum: 5,
            rowList: [5, 10, 15],
            sortname: 'Id',
            sortorder: 'asc',
            viewrecords: true,

            gridComplete: function () {
                $("#grid").jqGrid('navGrid', '#pager', { edit: true, add: true, del: true, search: true },
                    {
                        url: "/ContactDetails/Edit",
                        closeAfterEdit: true,
                        width: 600,

                        afterSubmit: function (response, postdata) {
                            var result = JSON.parse(response.responseText);
                            if (result.success) {
                                alert(result.message);
                                return [true];
                            } else {
                                alert(result.message);
                                return [false];
                            }
                        }
                    },
                    {
                        url: "/ContactDetails/Add",
                        closeAfterAdd: true,
                        width: 600,

                        aftersubmit: function (response, postdata) {
                            var result = JSON.parse(response.responseText);
                            if (result.success) {
                                alert(result.message);
                                return [true];
                            }
                            else {
                                alert(result.message);
                                return [False];
                            }

                        }
                    },
                    {
                        url: "/ContactDetails/Delete",

                        aftersubmit: function (response, postdata) {
                            var result = JSON.parse(response.responseText);
                            if (result.success) {
                                alert(result.message);
                                return [true];
                            } else {
                                alert(result.message);
                                return [false];
                            }
                        }
                    },
                    {
                        multipleSearch: false,
                        closeAfterSearch: true
                    }
                );
            }
        })
    
})

