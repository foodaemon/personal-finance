function categoryList() {}

categoryList.getCategories = function() {
    let obj = {
        url: '/api/v1/categories',
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',         
        },
    };
    return app.ajaxRequest(obj);
};

// register the grid component
Vue.component('custom-grid', {
    template: '#grid-template',
    props: {
        data: Array,
        columns: Array,
        filterKey: String,
        displayNames: {
            type: Object,
            default() {
                return {};
            }
        },
    },
    data: function () {
        var sortOrders = {};
        this.columns.forEach(function (key) {
            sortOrders[key] = 1
        })
        return {
            sortKey: '',
            sortOrders: sortOrders
        }
    },
    computed: {
        filteredData: function () {
            var sortKey = this.sortKey
            var filterKey = this.filterKey && this.filterKey.toLowerCase()
            var order = this.sortOrders[sortKey] || 1
            var data = this.data
            if (filterKey) {
                data = data.filter(function (row) {
                    return Object.keys(row).some(function (key) {
                        return String(row[key]).toLowerCase().indexOf(filterKey) > -1
                    });
                });
            }
            if (sortKey) {
                data = data.slice().sort(function (a, b) {
                    a = a[sortKey];
                    b = b[sortKey];
                    return (a === b ? 0 : a > b ? 1 : -1) * order;
                })
            }
            return data;
        }
    },
    filters: {
        getDisplayName: function(column, displayNames) {
            if(column in displayNames) {
                return displayNames[column];
            } else {
                return column.charAt(0).toUpperCase() + column.slice(1);
            }
        }
    },
    methods: {
        sortBy: function (key) {
            this.sortKey = key
            this.sortOrders[key] = this.sortOrders[key] * -1
        }
    }
  });  


categoryList.vm = new Vue({
    el: '#v-categories',
    data: {
        searchQuery: '',
        gridColumns: ['name', 'description',],
        displayNames: {
            'name': 'Category',
            'description': 'Description',
        },
        gridData: [],
    },
    created: function() {
        this.populateCategories();
    },   
    methods: {
        populateCategories: function() {
            categoryList.getCategories()
            .then(data => {
                this.gridData = JSON.parse(data);;
            })
            .catch(error => {
                console.log(error);
            });
        },
    }
});
