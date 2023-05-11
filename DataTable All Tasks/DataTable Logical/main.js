var datatable;
var data = [];
var modal
var isValid;
$(document).ready(function () {
    modal = $('.modal');
    $(modal).on('hide.bs.modal', () => {
        $(modal).find('input').val("");
    })
    datatable = $("#Table").DataTable({
        data: data,
        columns: [
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
                title: "Date Of Birth",
                data: "DOB",
                class: "DOB"
            },
            {
                title: "Ticket Date",
                data: "TicketDate",
                class: "TicketDate"
            },
            {
                title: "Leave Type",
                data: "LeaveType",
                class: "LeaveType"
            },
            {
                title: "Half Type",
                data: "HalfType",
                class: "HalfType"
            },
            {
                title: "Action",
                data: null,
                defaultContent: `<button type="button" class="btn btn-danger" id="Delete">Delete</button>`
            }
        ]
    })

    $("#ADD").click(function () {
        $("#Country").val('').trigger('change')
        $('.modal-body input').val('')
        $('.modal-body select').val('')
        $('span').html('')
    })


    $("#SelectHalf").hide()
    $("#Days").change(function () {
        var Emailvalue = $("#Email").val();
        var TicketDatevalue = $("#datepicker").val();
        var value = $('option:selected', this).text();
        if (value == "Half Day") {
            $("#SelectHalf").show()
            if((data.some(x => x.Email == Emailvalue && x.TicketDate == TicketDatevalue && x.HalfType == "First Half"))  ){
                $("#Halfs").html("")
                $("#Halfs").html(` <option id="Second">Second Half</option>`)
            }else if((data.some(x => x.Email == Emailvalue && x.TicketDate == TicketDatevalue && x.HalfType == "Second Half")) ){
                $("#Halfs").html("")
                $("#Halfs").html(` <option id="Second">First Half</option>`)
            }
            else{
                $("#Halfs").html("")
                $("#Halfs").append(` <option id="First">First Half</option>`)
                $("#Halfs").append(` <option id="Second">Second Half</option>`)
            }
        }   
         else {
            $("#SelectHalf").hide()
        }
    })

    $("#Save").on("click", function () {
        var namevalue = $("#Name").val();
        var Emailvalue = $("#Email").val();
        var DateValue = $("#Date").val();
        var Year = new Date(DateValue).getFullYear();
        var currentyear = new Date().getFullYear();
        var Age = currentyear - Year;
        console.log(Age)
        var TicketDatevalue = $("#datepicker").val();
        var Days = $("#Days").val();
        var Halfs = $("#Halfs").val();
        var nameregx = /^[A-Z a-z]+$/
        var Emailregx = /[^\s@]+@[^\s@]+\.[^\s@]+/

        isValid = true;
        if (namevalue == "") {
            $(".Name1").text("Please Enter Name")
            $(".Name1").css('color', 'red')
            isValid = false;

        } else if (!nameregx.test(namevalue)) {
            $(".Name1").text("Please Enter Valid Name")
            $(".Name1").css('color', 'red')
            isValid = false;
        }

        if (Emailvalue == "") {
            $(".Email1").text("Please Enter Email")
            $(".Email1").css('color', 'red')
            isValid = false;
        } else if (!Emailregx.test(Emailvalue)) {
            $(".Email1").text("Please Enter Valid Email")
            $(".Email1").css('color', 'red')
            isValid = false;
        } else if (data.some(x => x.Email == Emailvalue && x.TicketDate == TicketDatevalue )) {
            $(".Email1").text("This Email is Already Used")
            $(".Email1").css('color', 'red')
            $(".Date2").text("Please Enter Ticket Date")
            $(".Date2").css('color', 'red')
            isValid = false;
        }

        if (DateValue == "") {
            $(".Date1").text("Please Enter Date Of Birth")
            $(".Date1").css('color', 'red')
            isValid = false;
        }
        else if (Age < 18) {
            $(".Date1").text("Age Must Be Greater Than 18")
            $(".Date1").css('color', 'red')
            isValid = false;
        }
     
        
        if (TicketDatevalue == "") {
            $(".Date2").text("Please Enter Ticket Date")
            $(".Date2").css('color', 'red')
            isValid = false;
        }
        if (isValid) {

            var dataobj = {
                Name: namevalue,
                Email: Emailvalue,
                DOB: DateValue,
                TicketDate: TicketDatevalue,
                LeaveType: Days,
                HalfType: Halfs
            }
            data.push(dataobj)
            datatable.row.add(dataobj).draw();
             $(modal).modal('hide');
        }
    })

    $("table").on('click', '#Delete', function () {
        var row = $(this).parents('tr').index();
        data.splice(row, 1);
        console.log(data)
        datatable.row($(this).parents('tr')).remove().draw()
    })
})

