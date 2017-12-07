function dashboard() {}

dashboard.getExpensesByCategory = function() {
    let obj = {
        url: '/transaction/expensesbycategory',
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',         
        },
    };
    return app.ajaxRequest(obj);
};

dashboard.getYearlyIncomeExpense = function() {
    let obj = {
        url: '/transaction/yearly/',
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',         
        },
    };
    return app.ajaxRequest(obj);
};

dashboard.drawBarChart = function(data, labels) {
    var chartData = data;
    var chartLabels = labels;
    var bgColors = ['#4dbd74', '#f86c6b'];
    
    // for(var item in data){
    //     chartData.push(data[item]);
    //     chartLabels.push(item);
    //     bgColors.push(randomColor());
    // }
    
    var ctx = document.getElementById("chartbar");
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: chartLabels,
            datasets: [{
                label: 'Income/Expense',
                data: chartData,
                backgroundColor: bgColors,
            }],
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero:true
                    }
                }]
            }
        }
    });
};

// draw 2d pie chart
dashboard.drawPieChart = function(data, elementId) {
    var chartData = [];
    var chartLabels = [];
    var colors = [];
    
    for(var item in data){
        chartData.push(data[item]);
        chartLabels.push(item);
        colors.push(randomColor());
	}

    var config = {
        type: 'pie',
        data: {
            datasets: [{
                data: chartData,
                backgroundColor: colors,
            }],
            labels: chartLabels
        },
        options: { responsive: true },
    };

	var ctx = document.getElementById(elementId).getContext("2d");
	window.myPie = new Chart(ctx, config);
};

// draws yearly transactions.
dashboard.renderYearlyTransactionLineChart = function(data, elementId){
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
                backgroundColor: '#4dbd74',
                borderColor: '#4dbd74',
                data: incomes,
            },
            {
                label: "Expenses",
                fill: false,
                backgroundColor: '#f86c6b',
                borderColor: '#f86c6b',
                data: expenses,
            }],
            labels: labels
        },
        options: { responsive: true },
    };

	var ctx = document.getElementById(elementId).getContext("2d");
	window.myPie = new Chart(ctx, config);
};

dashboard.vm = new Vue({
    el: '#v-dashboard',
    data: {
        yearlyIncomeExpense: {},
        expenses: [],
    },
    mounted: function() {
        this.populatePieChart();
        this.populateLineChart();
    },
    methods: {
        populatePieChart: function() {
            dashboard.getExpensesByCategory()
            .then(data => {
                let result = JSON.parse(data);
                this.expenses = result;
                dashboard.drawPieChart(this.expenses, 'chartpie');
            })
            .catch(error => {
                console.log('error:' + error);
            });
        },
        populateLineChart: function () {
            dashboard.getYearlyIncomeExpense()
            .then(data => {
                let result = JSON.parse(data);
                this.yearlyIncomeExpense = result;
                dashboard.renderYearlyTransactionLineChart(this.yearlyIncomeExpense, 'chartline');
                dashboard.drawBarChart([this.yearlyIncome, this.yearlyExpense], ['Income/Yr', 'Expense/Yr']);
            })
            .catch(error => {
                console.log('error:' + error);
            });
        },
    },
    computed: {
        yearlyIncome: function() {
            var result = _.sum(_.map(this.yearlyIncomeExpense.yearlyIncome, function(n){ return +n;}));
            return result.toFixed(2);
        },
        yearlyExpense: function() {
            var result = _.sum(_.map(this.yearlyIncomeExpense.yearlyExpense, function(n){ return +n;}));
            return result.toFixed(2);
        },
        incomeThisMonth: function() {
            var month = new Date().getMonth() + 1;
            return 0.0;
        },
        expenseThisMonth: function() {
            return 0.0;
        },
        today: function() {
            return new Date().toLocaleDateString("en-US");
        },
    },
});
