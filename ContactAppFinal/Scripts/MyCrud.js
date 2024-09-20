function loadItems() {
    console.log("loadItems() called"); // Add this line

    $.ajax({
        url: "/Contact/GetContacts",
        type: "GET",
        dataType: "json",
        success: function (data) {
            $('#tblBody').empty();
            $.each(data, function (index, item) {
                var row = `<tr>
                <td>${item.FName}</td>
                <td>${item.LName}</td>
                <td>
                <input type="checkbox" class="contact-checkbox" data-user-id = "${item.Id}" ${item.IsActive ? "checked" : ""}/> 
                </td>
                <td>
                <button onclick = "EditContact(${item.Id})" class="btn btn-success">Edit</button>
                <button onclick = "GetContactDetails(${item.Id})" class="btn btn-warning">GetContactDetails</button>
                </td>`
                $('#tblBody').append(row)
            })
        },
        error: function (xhr, status, err) {
            console.log('Error details:', xhr.status, xhr.statusText, status, err);
            console.log('Response text:', xhr.responseText);
            $('#tblBody').empty()
            alert("No data Available")
        }



    })
}

//function GetContactDetails(itemId) {
//    $.ajax({
//        url: "/ContactDetail/GetDetails",
//        type: "POST",
//        data: { id: itemId },
//        success: function (data) {
//        },
//        error: function (err) {
//            alert("Error occured")
//        }
//    })
//}


$("#btnAdd").click(() => {
    $(".listItems").hide()
    $("#newRecord").show()
})

function addNewRecord(item) {
    $.ajax({
        url: "/Contact/Create",
        type: "POST",
        data: item,
        success: function (success) {
            alert("Contact Added Successfully")
            loadItems()
        },
        error: function (error) {
            alert("Error in addition of User")
        }
    })
}

$(".contact-checkbox").change(function () {

    var checkbox = $(this);
    var userId = checkbox.data('user-id');
    var isActive = checkbox.is(':checked');
    if (confirm("Do you really wish to delete ?")) {
        $.ajax({
            url: '/Contact/EditContactStatus',
            type: 'POST',
            data: {
                userId: userId,
                isActive: isActive
            },
            success: function (success) {
                alert("Contact Status Updated Successfully")
                loadItems()
            },
            error: function (err) {
                alert("Error in deactivating contact")
                checkbox.prop('checked', !isActive)
            }


        })
    }
})