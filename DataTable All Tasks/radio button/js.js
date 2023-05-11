var DataTable;
var row;
var arr = [];
$(document).ready(function () {
    DataTable = $(".table").DataTable({
        "ordering": false
    });
    $(".Data").css('display', 'none');
    $(".edit").css('display', 'none');
    $(".ADDNEW").click(function () {

        $(".Main").css('display', 'none');
        $(".Data").css('display', 'block');
        $(".edit").css('display', 'none');


    });

    $(".Submit").click(function () {
        //get value
        var show = `<button type="button" class="btn btn-outline-primary Edit">Edit</button>`;
        var ID = $("#ID").val();
        var Name = $("#Name").val();
        var Email = $("#Email").val();
        var Company = $("#Company").val();
        var Contact = $("#Number").val();
        var Edit = `<button type="button" class="btn btn-outline-primary Edit">Edit</button>`;
        var DeleteB = `<button type="button" class="btn btn-outline-danger Delete">Delete</button>`;
        var filter = /^((\+[1-9]{1,4}[ \-]*)|(\([0-9]{2,3}\)[ \-]*)|([0-9]{2,4})[ \-]*)*?[0-9]{3,4}?[ \-]*[0-9]{3,4}?$/;
        var regex = /[^\s@]+@[^\s@]+\.[^\s@]+/;
        var validName = /^[A-Z a-z]+$/;


        var radioValue = $("input[name='btn']:checked").val();
        //validation 
        if (!ID) {
            alert("Please Enter ID");
        } if (!Name || !validName.test(Name)) {
            alert("Please Enter valid Name");
            return false;
        } if (!Email || !regex.test(Email)) {
            alert("Please Enter Valid Email");
            return false;
        } if (!Company || !validName.test(Company)) {
            alert("Please Enter valid Company Name");
            return false;
        } if (!Contact || !filter.test(Contact)) {
            alert("Please Enter Valid Contact Number");
            return false;
        } if (!radioValue) {
            alert("Please Enter radioValue");
        } else {
            DataTable.row.add([ID, Name, Email, Company, Contact, radioValue, Edit, DeleteB]).draw();
            arr.push([{ 'id': ID, 'name': Name, 'Email': Email, 'Company': Company, 'Contact': Contact, 'radio': radioValue, 'Edit': Edit, 'DeleteB': DeleteB }]);
            $(".Data").css('display', 'none');
            $(".Main").css('display', 'block');
            $(".edit").css('display', 'none');
            $(".employeeform").trigger('reset');

            return true;
        }

    });

    $(".table").on("click", ".Edit", function () {

        var value1 = $(this).parents('tr').find('td:eq(0)').html();
        var value2 = $(this).parents('tr').find("td:eq(1)").html();
        var value3 = $(this).parents('tr').find("td:eq(2)").html();
        var value4 = $(this).parents('tr').find("td:eq(3)").html();
        var value5 = $(this).parents('tr').find("td:eq(4)").html();
        var value6 = $(this).parents('tr').find("td:eq(5)").html();
        row = $(this).parents('tr').index();

        $("#ID1").val(value1);
        $("#Name1").val(value2);
        $("#Email1").val(value3);
        $("#Company1").val(value4);
        $("#Number1").val(value5);
        // console.log( $(`input[value=${value6}]`))
        $(`input[value=${value6}]`).prop("checked", true);


        $(".Data").css('display', 'none');
        $(".Main").css('display', 'none');
        $(".edit").css('display', 'block');
    });
    $(".Save").click(function () {

        var ID1 = $("#ID1").val();
        var Name1 = $("#Name1").val();
        var Email1 = $("#Email1").val();
        var Company1 = $("#Company1").val();
        var Contact1 = $("#Number1").val();
        var Edit1 = `<button type="button" class="btn btn-outline-primary Edit">Edit</button>`;
        var DeleteB1 = `<button type="button" class="btn btn-outline-danger Delete">Delete</button>`;
        var filter = /^((\+[1-9]{1,4}[ \-]*)|(\([0-9]{2,3}\)[ \-]*)|([0-9]{2,4})[ \-]*)*?[0-9]{3,4}?[ \-]*[0-9]{3,4}?$/;
        var regex = /[^\s@]+@[^\s@]+\.[^\s@]+/;
        var validName = /^[A-Za-z]+$/;


        var radioValue1 = $("input[name='btn']:checked").val();
        console.log(radioValue1)
        //validation 
        if (!ID1) {
            alert("Please Enter ID");
        } if (!Name1 || !validName.test(Name1)) {
            alert("Please Enter valid Name");

        } if (!Email1 || !regex.test(Email1)) {
            alert("Please Enter Valid Email");

        } if (!Company1 || !validName.test(Company1)) {
            alert("Please Enter valid Company Name");

        } if (!Contact1 || !filter.test(Contact1)) {
            alert("Please Enter Valid Contact Number");

        } if (!radioValue1) {
            alert("Please Enter radioValue");
        } else {
            var editarr = { 'id': ID1, 'name': Name1, 'Email': Email1, 'Company': Company1, 'Contact': Contact1, 'radio': radioValue1, 'Edit': Edit1, 'Delete': DeleteB1 };
            // DataTable.row.add([ID, Name, Email, Company, Contact, radioValue, Edit, DeleteB]).draw();


            arr[row] = editarr;
            DataTable.clear().draw()
            arr.forEach(ele =>
                DataTable.row.add([ele.id, ele.name, ele.Email, ele.Company, ele.Contact, ele.radio, ele.Edit, ele.Delete]).draw()
            )
            $(".Data").css('display', 'none');
            $(".Main").css('display', 'block');
            $(".edit").css('display', 'none');

        }


        $(".Data").css('display', 'none');
        $(".Main").css('display', 'block');
        $(".edit").css('display', 'none');

    })

    $(".table").on("click", ".Delete", function () {
        DataTable.row($(this).parents('tr'))
            .remove()
            .draw();
    })

})