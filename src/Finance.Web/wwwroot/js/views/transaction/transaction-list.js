function transactionList() {}

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
        sortKey: 'amount',
        reverse: false,
        searchQuery: '',
        gridColumns: ['description', 'date', 'name', 'amount'],
        gridData: [],
        sortByAmount: false,
    },
    mounted: function() {
        this.populateTransactions();
    },
    computed: {
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
        populateTransactions: function(year=0, month=0) {
            transactionList.getTransactions(year, month)
            .then(data => {
                let result = JSON.parse(data);
                this.gridData = result;
            })
            .catch(error => {
                console.log('error:' + error);
            });
        },
        sortBy: function(array, param, reverse) {
            var filterA, filterB;
            return array.sort(function(a, b) {
                switch(param) {
                    case 'amount':
                        filterA = a.amount;
                        filterB = b.amount;
                        break;
                }
                if (reverse) {
					return (filterA > filterB) ? 1 : -1;
				} else {
					return (filterA < filterB) ? 1 : -1;
				}
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
    watch: {
        sortByAmount: function(val) {
            var self = this;
            self.gridData = self.sortBy(self.gridData, 'amount', val);
        }
    }
});
