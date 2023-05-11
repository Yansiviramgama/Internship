$(document).ready(function () {
    DataTable = $(".table").DataTable({
    });
    $("#data").click(function () {
        $(".form").trigger('reset');
        $(":checkbox").wrap();
    })
    $("#calculate").click(function () {


        // $('input[type="checkbox"]').each(function () {
        //     if (this.checked) {
        //         console.log($(this).val());
        //     }
        // });
        var value1 = $("#Num-1").val();
        var value2 = $("#Num-2").val();
        var value3 = $('input[name="mathematic-add"]:checked').val();
        var value4 = $('input[name="mathematic-sub"]:checked').val();
        var value5 = $('input[name="mathematic-mul"]:checked').val();
        var value6 = $('input[name="mathematic-divi"]:checked').val();
        var valid = /^[0-9]+$/;
        var arr = [];
        if (!valid.test(value2) || !valid.test(value1)) {
            alert("Number Does Not Contain Character")
        } else {
            if (value3) {
                var add = parseFloat(value1) + parseFloat(value2);
            } else {
                var add = "0";
            }
            if (value4) {
                var sub = parseFloat(value1) - parseFloat(value2);
            } else {
                var sub = "0";  
            }
            if (value5) {
                var mul = parseFloat(value1) * parseFloat(value2);
            } else {
                var mul = "0";
            }
            if (value6) {
                var divi = parseFloat(value1) / parseFloat(value2);
            } else {
                var divi = "0";
            }
            arr.push(value1, value2, add, sub, mul, divi);
            DataTable.row.add(arr).draw();
        }
    })


});
