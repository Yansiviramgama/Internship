var arr = [];
var arrName = [];
var arrMarks = [];
var studData = [];
var Result_Index = [];
var Result_Marks = [];
var avg = [];
var row;
var obj;
var val1,val2,val3;
$(document).ready(function () {
    var datatable = $('#Table').DataTable({
        "ordering": false
    })
    var dataT = $('#DataTable').DataTable({
        "ordering": false
    })
    var dataThird = $('#DataT').DataTable({
        "ordering": false
    })
    $(".second").css('display', 'none');
    $(".Third").css('display', 'none');
    $('#Add').click(function () {
         val1 = `<input type="text" class="name"/>`;
         val2 = `<input type="text" class="subname"/>`;
         val3 = `<input type="number" class="marks"/>`;
        var val4 = ` <button type="button" class="btn btn-outline-success btn-sm" id="pass" >Pass</button>
                        <button type="button" class="btn btn-outline-danger btn-sm" id="fail">Fail</button>`;
        var val5 = `<button type="button" class="btn btn-secondary btn-sm" id="Delete">Delete</button>`;
        if(val1==''){
            alert
        }
         datatable.row.add([val1, val2, val3, val4, val5]).draw();
    });
    $('table').on('click', '#Delete', function () {
        datatable.row($(this).parents('tr')).remove().draw()
    })
    $('table').on('click', '#pass', function () {      
        $(this).parent().parent().find('#pass').css("background-color", "green");
        $(this).parent().parent().find('#pass').css("color", "white");
        $(this).parent().parent().find('#fail').css("background-color","#fff");
        $(this).parent().parent().find('#fail').css("color", "red");
        var value1 = ($(this).parents(`tr`).find('td:eq(0)').children('input').val())
        var value2 = ($(this).parents(`tr`).find('td:eq(1)').children('input').val())
        var value3 = ($(this).parents(`tr`).find('td:eq(2)').children('input').val())
        row = $(this).parents(`tr`).index();
        obj = { 'name': value1, 'subname': value2, 'marks': value3 };
        arr.push(obj);
        arrName.push(value1);
        arrMarks.push(value3);
    })
    $('table').on('click', '#fail', function () {  
        $(this).parent().parent().find('#fail').css("background-color", "red");
        $(this).parent().parent().find('#fail').css("color", "white");
        $(this).parent().parent().find('#pass').css("background-color","#fff");
        $(this).parent().parent().find('#pass').css("color", "green");
    })
    $('#submit').click(function () {
        $(".second").css('display', 'block');
        arr.forEach(ele =>
            dataT.row.add([ele.name, ele.subname, ele.marks]).draw()
        )
    })
    $('#Result').click(function () {
        $(".Third").css('display', 'block');
        arrName.forEach(element => {
            if (!studData.includes(element)) {
                studData.push(element);
            }
        });
        for (var i = 0; i < studData.length; i++) {
            var count = 0;
            var totalmarks = 0;
            for (var j = 0; j < arrName.length; j++) {
                if (arrName[j] == studData[i]) {
                    totalmarks = parseFloat(arrMarks[j]) + parseFloat(totalmarks);
                    count++;
                }
            }
            Result_Marks.push(totalmarks);
            Result_Index.push(count);
        }
        for (var i = 0; i < Result_Marks.length; i++) {
            var percentage = parseFloat(Result_Marks[i]) / parseFloat(Result_Index[i]);
            avg.push(percentage);
        }
        for (var i = 0; i < studData.length; i++) {
            dataThird.row.add([studData[i], avg[i]]).draw();
        }
    })
})