Vue.use(Buefy.default, {
	defaultIconPack: 'fa'
});

function transactionList() {}

transactionList.years = [];
transactionList.months = [
    {value:1, text: "January"}, 
    {value: 2, text: "February"}, 
    {value: 3, text: "March"}, 
    {value: 4, text: "April"},
    {value: 5, text: "May"}, 
    {value: 6, text: "June"}, 
    {value: 7, text: "July"}, 
    {value: 8, text: "August"}, 
    {value: 9, text: "September"}, 
    {value: 10, text: "October"}, 
    {value: 11, text: "November"}, 
    {value: 12, text: "December"}
];

transactionList.getTransactions = function(year, month) {
    let obj = {
        url: '/api/v1/transactions/year/' + year + '/month/' + month,
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',         
        },
    };
    return app.ajaxRequest(obj);
};

transactionList.vm = new Vue({
    el: '#v-transactions',
    data: {
        date: new Date(),
        sortKey: 'amount',
        reverse: false,
        searchQuery: '',
        loading: true,
        gridColumns: ['description', 'date', 'name', 'amount'],
        gridData: [],
        sortByAmount: false,
        selectedMonth: (new Date()).getMonth() + 1,
        selectedYear: (new Date()).getFullYear(),
        months: transactionList.months,
    },
    mounted: function() {
        this.populateTransactions();
    },
    computed: {
        years: function() {
            let y = [];
            let yearNow = (new Date()).getFullYear();
            for(i = yearNow; i >= 2000; i--) {
                y.push(i);
            }
            return y;
        },
        totalIncome: function() {
            let sum = _(this.gridData)
            .filter(x => x.isIncome == true)
            .reduce(function(objs, transaction) {
                return objs += transaction.amount;
            }, 0); // initialize total with 0 that will be passed as objs
            return sum.toFixed(2);
        },
        totalExpenses: function() {
            let total = 0;
            let sum = _(this.gridData)
            .filter(x => x.isIncome == false)
            .reduce(function(objs, transaction) {
                return objs += transaction.amount;
            }, 0);
            return sum.toFixed(2);
        },
    },   
    methods: {
        monthChanged: function() {
            this.loading = true;
            this.populateTransactions(year=this.selectedYear, month=this.selectedMonth);
        },
        yearChanged: function() {
            this.loading = true;
            this.populateTransactions(year=this.selectedYear, month=this.selectedMonth);
        },
        populateTransactions: function(year=0, month=0) {
            transactionList.getTransactions(year, month)
            .then(data => {
                let result = JSON.parse(data);
                this.gridData = result;
                this.loading = false;
            })
            .catch(error => {
                console.log('error:' + error);
                this.loading = false;
            });
        },
        getByDate: function (event) {
            var date = document.getElementById('transaction_date').value;
            if (date != '') {
                var list = date.split('/');
                this.populateTransactions(list[1], list[0]);
                console.log(date);
            }
        },
    },
});
