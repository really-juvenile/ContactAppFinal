function loadContacts() {
    $.ajax({
        url: '/Contact/GetContacts',
        type: 'GET',

        success: function (data) {
            $("#tblBody").empty()

            $.each(data, function (i, contact) {
                var row = `<tr>
                <td>${contact.FName}</td>
                <td>${contact.LName}</td>
             
                <td>
                <input type="checkbox" class="checkbox" data-user-id="${contact.Id}" ${(contact.IsActive ? "checked" : "")} />
                </td>
                
                
                <td>
                <a href="/Contact/Edit/${contact.Id}" class="btn btn-success">Edit</button>
                </td>
                 <td>
                        <a href="/ContactDetails/Index?id=${contact.Id}" target="_blank" class="btn btn-info">Details</a>
                    </td>
                </tr>`

                $("#tblBody").append(row)
            })
        },
        error: function (err) {
            $("#tblBody").empty()
            alert("No data available")
        }
    })

}

$("#btnAdd").click(() => {
    $("#contactList").hide();
    $("#newRecord").show();
})

$("#btnDetails").click(() => {
    $("#contactList").hide();
    $("#contactDetailsList").show(); //grid name
})


function addNewRecord(newItem) {
    $.ajax({
        url: "/Contact/Create",
        type: "POST",
        data: newItem,
        success: function (item) {
            alert("new Item added Successfully")
            loadContacts()
        },
        error: function (err) {
            alert("Error adding new record")
        }
    })
}

function editNewRecord(newItem) {
    $.ajax({
        url: "/Contact/GetContact",
        type: "GET",
        data: { id: newItem },
        success: function (item) {
            $("#IdEdit").val(success.Id),
                $("#FNameEdit").val(success.FName),
                $("#LNameEdit").val(success.LName)
            $("#contactList").hide()
            $("#editRecord").show()
        },
        error: function (err) {
            alert("Error adding new record")
        }
    })
}

$(document).ready(function () {
    $("#tblBody").on('change', '.checkbox', function () {
        console.log("Change Occured")
        var checkbox = $(this);
        var userId = checkbox.data('user-id');
        var isActive = checkbox.is(':checked');
        if (confirm("Do you really wish to continue ?")) {
            $.ajax({
                url: '/Contact/EditContactStatus',
                type: 'POST',
                data: {
                    userId: userId,
                    isActive: isActive
                },
                success: function (success) {
                    alert("Contact Status Updated Successfully")

                },
                error: function (err) {
                    alert("Error in deactivating contact")
                    checkbox.prop('checked', !isActive)
                }


            })
        } else {
            checkbox.prop('checked', !isActive);
        }
    })

})

//function loadContacts() {
//    $.ajax({
//        url: '/Contact/GetContacts',
//        type: 'GET',
//        success: function (data) {
//            $("#tblBody").empty();

//            $.each(data, function (i, contact) {
//                var row = `<tr>
//                <td>${contact.FName}</td>
//                <td>${contact.LName}</td>
//                <td>
//                    <input type="checkbox" class="checkbox" data-user-id="${contact.Id}" ${(contact.IsActive ? "checked" : "")} />
//                </td>
//                <td>
//                    <button onclick="editNewRecord(${contact.Id})" class="btn btn-success">Edit</button>
//                </td>
//                <td>
//                    <button onclick="loadContactDetails(${contact.Id})" class="btn btn-info">Details</button>
//                </td>
//                <td>
//                    <button onclick="deleteRecord(${contact.Id})" class="btn btn-danger">Delete</button>
//                </td>
//                </tr>`;

//                $("#tblBody").append(row);
//            });
//        },
//        error: function (err) {
//            $("#tblBody").empty();
//            alert("No data available");
//        }
//    });
//}

