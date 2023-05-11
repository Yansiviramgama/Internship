var datatable;
var data = [];
var Order = []
$(document).ready(function () {
    datatable = $("#Table").DataTable({
        data: data,
        columns: [
            {
                title: "",
                data: "",
                defaultContent: "",
                class: "dt-control"
            },
            {
                title: "Sr.No",
                data: "ID",
                class: "ID"
            },
            {
                title: "Order Number",
                data: "OrderNum",
                classL: "OrderNum"
            },
            {
                title: "Customer Name",
                data: "CustomerName",
                class: "CustomerName"
            },
            {
                title: "Restaurant Name",
                data: "RestaurantName",
                class: "RestaurantName"
            },
            {
                title: "Contact Number",
                data: "ContactNumber",
                class: "ContactNumber"
            },
            {
                title: "Order Type",
                data: "OrderType",
                class: "OrderType"
            },
            {
                title: "Action",
                data: null,
                defaultContent: `<button class="btn btn-info" type="button" id="Edit"><i
                class="fa-solid fa-pen-to-square"></i></button>
                <button class="btn btn-danger" type="button" id="Delete"><i
                class="fa-solid fa-trash"></i></button>`
            }
        ]
    })










    
    var Restaurant = [{id:1,text:"Honest"},
    {id:2,text:"Ghee Gud"},
    {id:3,text:"Pizza Hut"}]
$("#Restaurant").select2({
    data: Restaurant,
    placeholder:`---Select Restaurant---`   
})
})