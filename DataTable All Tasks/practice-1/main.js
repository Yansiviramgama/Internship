var Datatable;
var mainarr = [];
$(document).ready(function(){
   Datatable = $("#Table").DataTable({
        data: mainarr,
        columns:[
            {
                title:"Name",
                data:"Name"
            },
            {
                title:"Email",
                data:"Email"
            },
            {
                title:"Contact Number",
                data:"Number"
            },
            {
                title:"Date Of Birth",
                data:"DOB"
            },
            {
                title:"Degree",
                data:"Degree"
            },
            {
                title:"DegreeType",
                data:"DegreeType"
            },
            {
                title:"Action",
                data:null,
                defaultContent:`<button type="button" id="edit" class="btn btn-primary btn-sm mt-1">Edit</button>
                <button type="button" id="delete" class="btn btn-danger btn-sm mt-1 mx-1">Delete</button>` 
            }
        ]

    })
    var mainobj = {id:1,Name: "Yansi", Email: "yansi24@gmail.com", Number: "9979238704", 
    DOB: "04/02/2002", Degree:"B.Tech", DegreeType:"Computer" };
    mainarr.push(mainobj);
    Datatable.row.add(mainobj).draw();


    
    $("#Table").on("click","#delete",function(){
        Datatable.row($(this).parents('tr')).remove().draw();
    })
})