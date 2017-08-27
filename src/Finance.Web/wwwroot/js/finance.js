function finance() { }

finance.toastrConfirm = function (event) {
    event.preventDefault();
    var msg = '<div>Are you sure?</div><div><a id="okBtn" class="btn btn-danger">Yes</a>' +
        '<button type="button" id="btnCancel" class="btn btn-primary" style="margin: 0 8px 0 8px">Cancel</button></div>';
    toastr.warning(msg);
};

// delete confirmation
finance.toastrConfirmDelete = function(event, url) {
    if (url == null) {
        return;
    }
    event.preventDefault();
    var msg = '<div>Are you sure?</div><div><a id="okBtn" class="btn btn-danger"' + url + '">Yes</a>' +
        '<button type="button" id="btnCancel" class="btn btn-primary" style="margin: 0 8px 0 8px">Cancel</button></div>';
    toastr.warning(msg);
};

// draw 2d pie chart
finance.drawPieChart = function(data, elementId) {
    var pieData = [];
    var pieLabels = [];
    var pieBackgroundColors = [];
    
    for(var item in data){
        pieData.push(data[item]);
        pieLabels.push(item);
        pieBackgroundColors.push(randomColor());
	}

    var config = {
        type: 'pie',
        data: {
            datasets: [{
                data: pieData,
                backgroundColor: pieBackgroundColors
            }],
            labels: pieLabels
        },
        options: { responsive: true },
    };

	// var ctx = document.getElementById(elementId).getContext("2d");
	// window.myPie = new Chart(ctx, config);
};

// draws yearly transactions.
finance.drawLineChart = function(data, elementId){
    var incomes = [];
    var expenses = [];
    var labels = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    
    for (i = 1; i <= 12; i++){
        incomes.push(data["yearlyIncome"][i]);
        expenses.push(data["yearlyExpense"][i]);
    }

    var incomeColor = randomColor();
    var expensesColor = randomColor();
    var config = {
        type: 'line',
        data: { datasets: [
            {
                label: "Income",
                fill: false,
                lineTension: 0.1,
                backgroundColor: '#669900',
                borderColor: '#669900',
                borderCapStyle: 'butt',
                borderDash: [],
                borderDashOffset: 0.0,
                borderJoinStyle: 'miter',
                pointBorderColor: '#669900',
                pointBackgroundColor: "#fff",
                pointBorderWidth: 1,
                pointHoverRadius: 5,
                pointHoverBackgroundColor: '#669900',
                pointHoverBorderColor: "rgba(220,220,220,1)",
                pointHoverBorderWidth: 2,
                pointRadius: 1,
                pointHitRadius: 10,
                data: incomes,
                spanGaps: false,
            },
            {
                label: "Expenses",
                fill: false,
                lineTension: 0.1,
                backgroundColor: '#990033',
                borderColor: '#990033',
                borderCapStyle: 'butt',
                borderDash: [],
                borderDashOffset: 0.0,
                borderJoinStyle: 'miter',
                pointBorderColor: '#990033',
                pointBackgroundColor: "#fff",
                pointBorderWidth: 1,
                pointHoverRadius: 5,
                pointHoverBackgroundColor: '#990033',
                pointHoverBorderColor: "rgba(220,220,220,1)",
                pointHoverBorderWidth: 2,
                pointRadius: 1,
                pointHitRadius: 10,
                data: expenses,
                spanGaps: false,}
            ],
            labels: labels
        },
        options: { responsive: true },
    };

	//var ctx = document.getElementById(elementId).getContext("2d");
	//window.myPie = new Chart(ctx, config);
};
