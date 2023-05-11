var data = [];
var datatable;
var val;
var obj;
var Tindex;
var nameinput, DOBinput, inputval, emailinput, numinput, countryinput, Stateinputr, cityinput;
var rowindex;
var counter = 1;
var validname, validemail, validnumber;
var Information = [];
$(document).ready(function () {
    datatable = $('#Table').DataTable({
        data: data,
        columns: [
            {
                title: "",
                data: null,
                defaultContent: "",
                class: "dt-control",
            },
            {
                title: "id",
                data: "id",
                class:"displaynone",
            },
            {
                title: "Name",
                data: "Name",
            },
            {
                title: "Age",
                data: "Age",
            },
            {
                title: "Contact Number",
                data: "ContactNumber",
            },
            {
                title: "Address",
                data: "Address",
            },
            {
                title: "Action",
                data: null,
                defaultContent: `<button type="button" id="treatment" class="btn btn-secondary btn-sm" data-bs-toggle="modal" data-bs-target="#exampleModal">Add Treatment</button>
                <br><button type="button" id="edit" class="btn btn-primary btn-sm mt-1"><i class="fa-sharp fa-solid fa-pen-to-square"></i></button>
                <button type="button" id="delete" class="btn btn-danger btn-sm mt-1 mx-1"><i class="fa-solid fa-trash-xmark"></i>Delete</button>`,
            },

        ]

    });
    $('#Save').on('click', function () {
        validnumber = /^((\+[1-9]{1,4}[ \-]*)|(\([0-9]{2,3}\)[ \-]*)|([0-9]{2,4})[ \-]*)*?[0-9]{3,4}?[ \-]*[0-9]{3,4}?$/;
        validemail = /[^\s@]+@[^\s@]+\.[^\s@]+/;
        validname = /^[A-Z a-z]+$/;
        var nameinput = $('#nameinput').val();
        var DOBinput = $('#DOBinput').val();
        inputval = $("input[id='gender']:checked").val();
        var emailinput = $('#emailinput').val();
        var numinput = $('#numinput').val();
        var countryinput = $('#countryinput').val();
        var Stateinput = $('#Stateinput').val();
        var cityinput = $('#cityinput').val();
        var address = cityinput + " " + Stateinput + " " + countryinput;
        var year = new Date(DOBinput).getFullYear();
        var currentyear = new Date().getFullYear();
        var age = currentyear - year;
        if (nameinput == "" || !validname.test(nameinput)) {
            $(".nameinput").text("Enter Valid Name")
            $(".nameinput").css('color', 'red')
            //return false;
        } if (DOBinput == "") {
            $(".DOBinput").text("Please Enter Date")
            $(".DOBinput").css('color', 'red')
            //return false;
        } if (!inputval) {
            $(".inputval").text("Please select Gender Date")
            $(".inputval").css('color', 'red')
            // return false;
        } if (emailinput == "" || !validemail.test(emailinput)) {
            $(".emailinput").text("Enter Valid Email")
            $(".emailinput").css('color', 'red')
            // return false;
        }
        if (numinput == "" || !validnumber.test(numinput)) {
            $(".numinput").text("Enter Valid Number")
            $(".numinput").css('color', 'red')
            // return false;
        } if (!countryinput) {
            $(".countryinput").text("Please select Country ")
            $(".countryinput").css('color', 'red')
            //return false;
        } if (!Stateinput) {
            $(".Stateinput").text("Please select State ")
            $(".Stateinput").css('color', 'red')
            // return false;
        } if (!cityinput) {
            $(".cityinput").text("Please select City ")
            $(".cityinput").css('color', 'red')
            // return false;
        } else if (age <= 15) {
            alert("age is not valid ")
            return false;
        } else {
            obj = { id: counter++, Name: nameinput, Age: age, ContactNumber: numinput, Address: address, Information: [] }
            data.push(obj)
            datatable.row.add(obj).draw();
            $('.modal').modal('hide')
            return true;
        }
    })
    $('.table').on('click', '#treatment', function (ele) {
         Tindex = $(this).parents('tr').index()
        console.log(index)
    })
    $('#AddSave').on('click', function () {        
        var Disease = $('#Disease')
        var Doctor = $('#Doctor')
        var Medicine = $('#Medicine')
        var FollowUpDate = $('#FollowUpDate')
        Array.from(Disease).forEach((element, index) => {
                obj.Information.push({
                    disease: element.value,
                    doctor: Doctor[index].value,
                    medicine: Medicine[index].value,
                    followupdate: FollowUpDate[index].value
                })            
        })
    })
    $('#AddData').on('click', function () {
        $('.modal-body input').val('')
        $('.modal-body select').val('')
    })
    $('.table').on('click', '#delete', function (ele) {
        datatable.row($(this).parents('tr'))
            .remove()
            .draw();
    })
    $('.table').on('click', '#edit', function () {
        rowindex = $(this).parents('tr');
        console.log(datatable.row(rowindex).data())
        $('#nameinput').val(nameinput);
        $('#DOBinput').val(DOBinput);
        //$("input[id='gender']:checked").val();
        $('#emailinput').val(emailinput);
        $('#numinput').val(numinput);
        $('#countryinput').val(countryinput);
        $('#Stateinput').val(Stateinputr);
        $('#cityinput').val(cityinput);
    })
    $('table tbody').on('click', '.dt-control', function () {
        var tr = $(this).closest('tr');
        var index = $(this).parents('tr').index()
        var row = datatable.row(tr);
        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        } else {
            // Open this row
            row.child(format(data[index].Information)).show();
            tr.addClass('shown');
        }
    });
});
function format(d) {
    var str = `<div class="container"><table>
    <thead>
        <tr>
            <th>Disease</th>
            <th>Doctor</th>
            <th>Medicine</th>
            <th>Follow Up Date</th>
        </tr>
    </thead><tbody>`
    d.forEach(element => {
        str += `<tr><td>${element.disease}</td> <td>${element.doctor}</td> <td>${element.medicine}</td> <td>${element.followupdate}</td></tr>`
    })
    str += `</tbody></table></div>`
    return (str);
}











