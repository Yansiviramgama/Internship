var datatable;
var data = [];
var NewState;
var NewCity;
var mainobj;
var Count = 1;
var EID = 1;
//var mainarr = [];
var modal;
var Modal
var EduIndex;
var EditObj;
var ID;
var EditIndex;
$(document).ready(function () {
    modal = $('.Modal1');
    Modal = $('.modal2');
    $(modal).on('hide.bs.modal', () => {
        $(modal).find('input').val("");
        $(modal).find('select').val("");
        $("#State").select2({
            data: [],
            placeholder: "Select State",
            width: 450,
            allowClear: true
        }).html("")
        
        $("#City").select2({
            data: [],
            placeholder: "Select City",
            width: 450,
            allowClear: true
        }).html("")
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
                title: "Id",
                data: "Id",
                class: "Id"
            },
            {
                title: "Name",
                data: "Name",
                class: "Name"
            },
            {
                title: "Employee ID",
                data: "EmployeeID",
                class: "EmployeeID"
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
                defaultContent: ` <button type="button" class="btn btn-outline-primary btn-sm" id="Edit">Edit</button>    
                <button type="button" class="btn btn-outline-danger btn-sm" id="Delete">Delete</button><br>
                <button type="button" class="btn btn-outline-success btn-sm mt-1" data-bs-toggle="modal"
                data-bs-target="#exampleModal" id="Education">Add Education</button>`
            }

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
    var Degree = [
        { id: 1, text: "B.Tech" }, { id: 2, text: "M.Tech" }, { id: 3, text: "MBA" }, { id: 4, text: "Mcom" }, { id: 5, text: "MCA" },
        { id: 6, text: "Bcom" }, { id: 7, text: "BBA" }, { id: 8, text: "BCA" }, { id: 9, text: "Bsc" }, { id: 10, text: "Msc" }, { id: 11, text: "B.Arch" }
    ]
    var College = [
        { id: 1, text: "GEC-Rajkot" }, { id: 2, text: "GEC-Bhavnagar" }, { id: 3, text: "GEC-Patan" }, { id: 4, text: "GEC-Dahod" }, { id: 5, text: "GEC-Modasa" },
        { id: 6, text: "Marwadi" }, { id: 7, text: "Darshan" }, { id: 8, text: "Atmiya" }, { id: 9, text: "Ahmedabad University" }, { id: 10, text: "LD-College" },
        { id: 11, text: "Anant Architecture" }, { id: 12, text: "CK-Pithavala" }, { id: 13, text: "CEPT Ahmedabad" }, { id: 14, text: "SVNIT-Surat" }, { id: 15, text: "VVP Rajkot" }
    ]
    var Department = [
        { id: 1, text: "Computer Engineering" },
        { id: 2, text: "Information Technology" },
        { id: 3, text: "Electrical Engineering" },
        { id: 4, text: "Mechenical Engineering" },
        { id: 5, text: "Automobile Engineering" },
        { id: 6, text: "Civil Engineering" },
        { id: 7, text: "Bcom" },
        { id: 8, text: "Bsc-It" },
        { id: 9, text: "Bsc-Physics" },
        { id: 10, text: "Diploma" },
        { id: 11, text: "Interior" },
        { id: 12, text: "BCA" }
    ]
    var Subjects = [
        { id: 1, text: "Web Programing" },
        { id: 2, text: "System Software" },
        { id: 3, text: "Water Manegment" },
        { id: 4, text: "Basic Mechanical" },
        { id: 5, text: "Machine Parts" },
        { id: 6, text: "Accounts" },
        { id: 7, text: "Paython Data Science" },
        { id: 8, text: "PHP" },
        { id: 9, text: "MVC Dot Net" },
        { id: 10, text: "Mathematics-I" },
        { id: 11, text: "Mathematics-II" },
        { id: 12, text: "Mathematics-III" },
        { id: 13, text: "Basic Civil" },
        { id: 14, text: "Cricuit Management" },
        { id: 15, text: "Physics" }
    ]
    $("#Country").select2({
        data: Country,
        placeholder: "Select Country",      
        width: 450,
        allowClear: true
    })
    $("#State").select2({
        data: ``,
        placeholder: "Select State",
        width: 450,
        allowClear: true
    })
    $("#City").select2({
        data: ``,
        placeholder: "Select City",
        width: 450,
        allowClear: true
    })
    $("#Degree").select2({
        data: Degree,
        placeholder: "Select Degree",
        width: 450,
        allowClear: true
    })
    $("#College").select2({
        data: College,
        placeholder: "Select Degree",
        width: 450,
        allowClear: true
    })
    $("#Department").select2({
        data: Department,
        placeholder: "Select Degree",
        width: 450,
        allowClear: true
    })
    $("#Subjects").select2({
        data: Subjects,
        multiple: true,
        placeholder: "Select Degree",
        width: 450,
        allowClear: true
    })
    $(document).on("change", "#Country", function () {
        // $("#State").val('').trigger('change')
        // $("#City").val('').trigger('change')
        NewState = State.filter(x => x.Countryid == $("#Country").val());
        $('#State').select2({
            data: NewState,
            placeholder: "Select State",
            width: 450
        })        
        $("#City").select2({
            data: [],
            placeholder: "Select City",
            width: 450,
            allowClear: true
        })
    })
    $(document).on("change", "#State", function () {
       // $("#City").val('').trigger('change')
        NewCity = City.filter(x => x.Stateid == $("#State").val());
        $("#City").select2({
            data: NewCity,
            placeholder: "Select City",
            width: 450,
            allowClear: true
        })
    
    });
    $("#AddData").on('click', function () {
    
        // $("#Country").val('').trigger('change')
        // $("#State").val('').trigger('change')
        // $("#City").val('').trigger('change')
        $("#Update").css("display", "none");
        $("#Save").css("display", "block")
        
      
    })
    $('#Save').on('click', function () {

        var Name = $("#Name").val();
        var EmployeeID = $("#EmployeeID").val();
        var CountryName = Country.find(x => x.id == $("#Country").val()).text;
        var StateName = NewState.find(x => x.id == $("#State").val()).text;
        var CityName = NewCity.find(x => x.id == $("#City").val()).text;
        mainobj = {
            Id: Count++,
            Name: Name,
            EmployeeID: EmployeeID,
            Country: CountryName,
            State: StateName,
            City: CityName,
            Education: []
        }
        data.push(mainobj)
        datatable.row.add(mainobj).draw();
        $(modal).modal('hide');
        $("#Country").val('').trigger('change')
        $("#State").val('').trigger('change')
        $("#City").val('').trigger('change')
    })
    $("#SaveEducation").on('click', function () {

        var DegreeName = Degree.find(x => x.id == $("#Degree").val()).text
        var CollegeName = College.find(x => x.id == $("#College").val()).text
        var DepartmentName = Department.find(x => x.id == $("#Department").val()).text
        // Get Value
        // var categoryVal = $('#Subjects').val();
        // console.log(categoryVal);
        // Get Text
        var SubjectsText = $('#Subjects').select2("data");
        var SubjectsName = SubjectsText.map(x => x.text).toString();
        var EducationObj = {
            id: EID++,
            Degree: DegreeName,
            College: CollegeName,
            Department: DepartmentName,
            Subjects: SubjectsName
        }
        mainobj.Education.push(EducationObj);
        $(Modal).modal('hide');
    })
    $("#Table").on('click', '.dt-control', function () {
        var tr = $(this).closest('tr')
        var row = datatable.row(tr)
        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass('shown')
        }
        else {
            row.child(format(row.data().Education)).show();
            tr.addClass('shown');
        }
    })
    function format(d) {
        '<ul>'
        return ((d.map((ele) => {
            EduIndex = ele.id;
            return `<li>` + `Degree : ` + ele.Degree + `</li>` +
                `<li class="mt-1">` + `College : ` + ele.College + `</li>` +
                `<li class="mt-1">` + `Department : ` + ele.Department + `</li>` +
                `<li class="mt-1">` + `Subjects : ` + ele.Subjects + `</li>`
        })).toString() + `</ul>`
        );
    }
    $("#Table").on('click', '#Delete', function () {
        datatable.row($(this).parents('tr')).remove().draw();
    })
    $("#Table").on('click', '#Edit', function () {
        $("#Save").css("display", "none");
        $("#Update").css("display", "block");
        $(modal).modal('show')
        EditIndex = $(this).parents('tr').index();
        console.log(EditIndex);
        ID = data[EditIndex].Id
        console.log(ID);
        var EditName = data[EditIndex].Name
        var EditEmployeID = data[EditIndex].EmployeeID
        $("#Name").val(EditName);
        $("#EmployeeID").val(EditEmployeID);
    })
    $("#Update").on('click', function () {
        ID = data[EditIndex].Id
        console.log(ID)
        var Name = $("#Name").val();
        var EmployeeID = $("#EmployeeID").val();
        var CountryName = Country.find(x => x.id == $("#Country").val()).text;
        var StateName = NewState.find(x => x.id == $("#State").val()).text;
        var CityName = NewCity.find(x => x.id == $("#City").val()).text;
        EditObj = {
            Id: ID,
            Name: Name,
            EmployeeID: EmployeeID,
            Country: CountryName,
            State: StateName,
            City: CityName,
            Education: []
        }
        data[EditIndex] = EditObj       
        datatable.clear().draw();   
        datatable.rows.add(data).draw()      
        $(modal).modal('hide');
    })
})