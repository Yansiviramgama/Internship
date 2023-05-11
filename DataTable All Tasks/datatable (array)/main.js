var data = [];
var datatable;
var mainarray = [];


var id = 1;
var modal;
var isValid;
var row;
$(document).ready(function () {
    modal = $('.modal');
    $(modal).on('hide.bs.modal', () => {
        $(modal).find('input').val("");
    })
    datatable = $("#Table").DataTable({
        data: data,
        columns: [
            {
                title: "",
                data: null,
                defaultContent: "",
                class: "dt-control"
            },
            {
                title: "Name",
                data: "Name",
                class: "Name"
            },
            {
                title: "Email",
                data: "Email",
                class: "Email"
            },
            {
                title: "Contact Number",
                data: "ContactNumber",
                class: "ContactNumber"
            },
            {
                title: "DOB",
                data: "DOB",
                class: "DOB"
            },
            {
                title: "Country",
                data: "Country",
                class: "Country"
            },
            {
                title: "State",
                data: "State",
                class: "State"
            },
            {
                title: "City",
                data: "City",
                class: "City"
            },
            {
                title: "Action",
                data: null,
                defaultContent: `<button type="button" id="edit" class="btn btn-primary btn-sm mt-1">Edit</button>
                <button type="button" id="delete" class="btn btn-danger btn-sm mt-1 mx-1">Delete</button>`
            },

        ]
    })

    var Country = [
        { id: 1, text: "India" }, { id: 2, text: "USA" }, { id: 3, text: "Australia" }, { id: 4, text: "UK" }, { id: 5, text: "Canada" }
    ]
    var State = [
        { id: 1, Countryid: 1, text: "Gujrat" }, { id: 2, Countryid: 1, text: "Punjab" },
        { id: 3, Countryid: 2, text: "Ohio" }, { id: 4, Countryid: 2, text: "texas" },
        { id: 5, Countryid: 3, text: "Melbourn" }, { id: 6, Countryid: 3, text: "Sydney" },
        { id: 7, Countryid: 4, text: "California" }, { id: 8, Countryid: 4, text: "Kent" },
        { id: 9, Countryid: 5, text: "ontario" }, { id: 10, Countryid: 5, text: "Burha" }
    ]
    var City = [
        { id: 1, Countryid: 1, Stateid: 1, text: "Rajkot" }, { id: 2, Stateid: 1, Countryid: 1, text: "Ahemadabad" },
        { id: 3, Countryid: 2, Stateid: 2, text: "Ludhiyana" }, { id: 4, Countryid: 2, Stateid: 2, text: "Amritsar" },
        { id: 5, Countryid: 3, Stateid: 3, text: "Poland" }, { id: 6, Countryid: 3, Stateid: 4, text: "Rush" },
        { id: 7, Countryid: 4, Stateid: 5, text: "Florida" }, { id: 8, Countryid: 4, Stateid: 6, text: "NewYork" },
        { id: 9, Countryid: 5, Stateid: 7, text: "Dubai" }, { id: 10, Countryid: 5, Stateid: 8, text: "Japan" }
    ]

    $('#Country').select2({
        data: Country,
        placeholder: "Select Country from List",
        width: 450,
        allowClear: true
    }).on('change', function (e) {

        //console.log(e.currentTarget.value)
        var countryid = e.currentTarget.value
        console.log(countryid);
        selectstate();
    })
    function selectstate() {
        $('#State').select2({
            data: State,
            placeholder: "Select State from List",
            width: 450
        }).on('change', function (e) {
            var stateid = e.currentTarget.value
            console.log(stateid);
            seletcity();
        })
    }
    function seletcity() {
        $('#City').select2({
            data: City,
            placeholder: "Select City from List",
            width: 450
        });
    }
    $("#ADD").click(function () {
        $("#Country").val('').trigger('change')
        $('.modal-body input').val('')
        $('.modal-body select').val('')
    })
    $("#save").click(function () {
        var inputId = document.getElementById('ID').value;

        if (inputId == "" || isNaN(inputId)) {
            var value1 = $("#Name").val();
            var value2 = $("#Email").val();
            var value3 = $("#ContactNumber").val();
            var value4 = $("#Date").val();
            var value5 = $("#select2-Country-container").text();
            var value6 = $("#select2-State-container").text();
            var value7 = $("#select2-City-container").text();
            var value8 = $("#Department").val();
            var value9 = $("#Subjects").val();
            isValid = true;

            if (value1 == "") {
                $(".Name1").html('enter your name')
                $(".Name1").css('color', 'red')
                isValid = false;
            } else {
                $(".Name1").html('')
            }

            if (value2 == "") {
                $(".Email1").html('enter your Email')
                $(".Email1").css('color', 'red')
                isValid = false;
            } else {
                $(".Email1").html('')
            }

            if (value3 == "") {
                $(".ContactNumber1").html('enter your Contact Number')
                $(".ContactNumber1").css('color', 'red')
                isValid = false;
            } else {
                $(".ContactNumber1").html('')
            }

            if (value4 == "") {
                $(".Date").html('enter Date')
                $(".Date").css('color', 'red')
                isValid = false;
            } else {
                $(".Date").html('')
            }

            if (value8 == "") {
                $(".Department").html('enter your Department')
                $(".Department").css('color', 'red')
                isValid = false;
            } else {
                $(".Department").html('')
            }

            if (value9 == "") {
                $(".Subjects").html('enter your Subjects')
                $(".Subjects").css('color', 'red')
                isValid = false;
            } else {
                $(".Subjects").html('')
            }

            if (isValid) {
                var obj = {
                     Name: value1, Email: value2, ContactNumber: value3,
                    DOB: value4, Country: value5, State: value6, City: value7, studentarr: []
                };
                var studentobj = { ID: id++, Department: value8, Subjects: value9 }
                obj.studentarr.push(studentobj);
                mainarray.push(obj);
                console.log(mainarray);
                datatable.row.add(obj).draw();
                $(modal).modal('hide');
            }
        }
        else {        
            var name = $("#Name").val();
            var email = $("#Email").val();
            var number = $("#ContactNumber").val();
            var date = $("#Date").val();
            var country = $("#select2-Country-container").text();
            var state = $("#select2-State-container").text();
            var city = $("#select2-City-container").text();
            var department = $("#Department").val();
            var subjects = $("#Subjects").val();  
            var editstudentobj = { ID: id++, Department: department, Subjects: subjects }
            studentarr[row] = editstudentobj;
            studentarr.push(editstudentobj);
            var editarr = {
                 Name: name, Email: email, ContactNumber: number,
                DOB: date, Country: country, State: state, City: city, studentarr: []
            };
            mainarray[row] = editarr;
            datatable.clear().draw();
            mainarray.forEach(ele => {
                datatable.row.add([ele.Name,ele.Email,ele.ContactNumber,ele.DOB,ele.Country,ele.State,ele.City]).draw();
              
            })
        $(modal).modal('hide');
            // var obj = mainarray.find(x => x.ID = inputId);
            //     Object.keys(obj).forEach((key) => {
            //         obj[key] = $('#' + key).val()
            //     })
            //     datatable.clear().draw();
            //     datatable.row.add(obj).draw();
                         
        }
    })
    $("#Table").on('click', '#edit', function () {
      
        var value1 = $(this).parents('tr').find('td:eq(1)').html();
        var value2 = $(this).parents('tr').find('td:eq(2)').html();
        var value3 = $(this).parents('tr').find('td:eq(3)').html();
        var value4 = $(this).parents('tr').find('td:eq(4)').html();
        var value5 = $(this).parents('tr').find('td:eq(5)').html();
        var value6 = $(this).parents('tr').find('td:eq(6)').html();
        var value7 = $(this).parents('tr').find('td:eq(7)').html();
        row = $(this).parents('tr').index();
        $("#Name").val(value1);
        $("#Email").val(value2);
        $("#ContactNumber").val(value3);
        $("#Date").val(value4);
        $("#select2-Country-container").text(value5);
        $("#select2-State-container").text(value6);
        $("#select2-City-container").text(value7);  

        // var editvalue1 = $(this).parents('tr').find('td:eq(1)');
        // var id = editvalue1.html();
        // row = $(this).parents('tr').index();
        // var obj = mainarray.find(y => y.ID == id);
        // Object.keys(obj).forEach((ele) => {
        //     $('#' + ele).val(obj[ele]);
        // });
        $(modal).modal('show');
    })
    $("#Table").on('click', '#delete', function () {
        datatable.row($(this).parents('tr')).remove().draw();
    })
    $('#Table').on('click', '.dt-control', function (ele) {
        var tr = $(this).closest('tr');
        var row = datatable.row(tr);
        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        } else {
            // Open this row
            row.child(format(row.data().studentarr)).show();
            tr.addClass('shown');
        }
    });
    function format(d) {
        // `d` is the original data object for the row
        `<table>`
        return (
            '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
            (d.map((ele) => {
                return '<tr>' +
                    '<td>Department:</td>' +
                    '<td>' +
                    ele.Department +
                    '</td>' +
                    '</tr>' +
                    '<tr>' +
                    '<td>Subjects:</td>' +
                    '<td>' +
                    ele.Subjects +
                    '</td>' +
                    '</tr>'
            })).toString() +
            '</table>'
        );
    }
})