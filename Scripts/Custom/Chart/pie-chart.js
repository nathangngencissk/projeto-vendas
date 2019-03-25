// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';

// Pie Chart Example
var ctx = document.getElementById("myPieChart");

var myPieChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
        labels: [],
        datasets: [{
            data: [],
            backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc'],
            hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf'],
            hoverBorderColor: "rgba(234, 236, 244, 1)",
        }],
    },
    options: {
        maintainAspectRatio: false,
        tooltips: {
            backgroundColor: "rgb(255,255,255)",
            bodyFontColor: "#858796",
            borderColor: '#dddfeb',
            borderWidth: 1,
            xPadding: 15,
            yPadding: 15,
            displayColors: false,
            caretPadding: 10,
        },
        legend: {
            display: false
        },
        cutoutPercentage: 80,
    },
});

$(document).ready(function () {
    $.ajax({
        url: "/categorias/top3/json",
        type: "post",
        async: true,
        data: {},
        success: function (resp) {
            myPieChart.data.labels.push(resp.categoria1);
            myPieChart.data.labels.push(resp.categoria2);
            myPieChart.data.labels.push(resp.categoria3);
            myPieChart.data.datasets.forEach((dataset) => {
                dataset.data.push(resp.valor1);
                dataset.data.push(resp.valor2);
                dataset.data.push(resp.valor3);
            });
            myPieChart.update();
            $("#categoria1").html(resp.categoria1);
            $("#categoria2").html(resp.categoria2);
            $("#categoria3").html(resp.categoria3);
        },
        error: function (resp) {
            console.log(resp);
        }
    });
});