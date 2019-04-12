function fn_hide_span_yesno(o) {
    $('#span_yesno').fadeIn();
    $('#span_deleteback').fadeOut();

    if (o === 0) {
        $('#span_yesno').fadeOut();
        $('#span_deleteback').fadeIn();
    }
}

function fn_clicked() {
    var radios = document.getElementsByName("rad_duration");
    for (var i = 0, len = radios.length; i < len; i++) {
        if (radios[i].checked) { // radio checked?
            val = radios[i].id; // if so, hold its value in val
            break; // and break out of for loop
        }
    }



    $.ajax({
        url: "../Tag/Index",
        data: { tagFilter: $('#inp_filtertag').val(), radioId: val },
        type: "POST",
        success: function () { },
        error: function () { }
    });
}

function init() {
    $('#span_yesno').hide();
    $('span_deletetag').show();


    
    $('.css-sm-animate-fadeIn').fadeIn(1000);

    
    $('.css-sm-animate-slideUp').delay(300).fadeIn(700);

    fn_showchart($('#myChart11'));
}

function fn_showchart(o) {
    var ctx1 = o[0];
    var ctx = ctx1.getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ["Accepted", "Processed", "Delivered", "Failed", "Unique Opens", "Unique Clicks"],
            datasets: [{
                label: 'Hide',
                data: [
                    $('#hdn_accepted').val(),
                    $('#hdn_processed').val(),
                    $('#hdn_delivered').val(),
                    $('#hdn_failed').val(),
                    $('#hdn_uniqueopens').val(),
                    $('#hdn_uniqueclicks').val()
                    
                    ],
                backgroundColor: [
                    'rgba(126, 234, 234, 0.6)',
                    'rgba(208, 181, 112, 0.8)',
                    'rgba(39, 206, 122, 0.6)',
                    'rgba(255, 99, 132, 0.6)',
                    'rgba(56, 59, 185, 0.6)',
                    'rgba(202, 65, 222, 0.6)'
                    
                ],
                borderColor: [
                    'rgb(100, 186, 186, 1)',
                    'rgba(180, 161, 112, 1)',
                    'rgba(29, 147, 88, 1 )',
                    'rgba(255,99,132,1)',
                    'rgba(29, 56, 129, 1)',
                    'rgba(202, 65, 222, 1)'
                    
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}

function fn_showhide_chart(o) {
    $('#myChart11').slideToggle(600);

    //if ($('#span_hideshowchart').html("Chart -"))
    //{
    //    $('#span_hideshowchart').html("Chart +");
    //}

    //if ($('#span_hideshowchart').html("Chart +"))
    //{
    //    $('#span_hideshowchart').html("Chart -");
    //}
}

function fn_sortby(o) {
    alert("'firstseen'");
}