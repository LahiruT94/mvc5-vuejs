const getters = {
    getParams: state => {
        return {
            search: state.filter,
            page: state.currentPage,
            pageSize: state.pageSize,
            sortColumn: state.sortOrder.prop !== undefined ? state.sortOrder.prop : '',
            sortOrder: state.sortOrder.order !== undefined ? state.sortOrder.order : '',
        }
    },
    getTotalItems: state => state.totalItems,
    getCurrentPage: state => state.currentPage,
    getPageSize: state => state.pageSize
}

const mutations = {
    setup(state, {type, value}) {
        state[type] = value
    },
    increment(state, {type, value}) {
        state[type] += value
    }
}

const actions = {
    setOrder({commit}, sortOrder) {
        commit('setup', {
            type: 'sortOrder',
            value: sortOrder
        })
    },
    setFilter({commit}, value) {
        commit('setup', {
            type: 'filter',
            value: value
        })
    },
    setPage({commit}, page) {
        commit('setup', {
            type: 'currentPage',
            value: page
        })
    },
    setPageSize({commit}, pageSize) {
        commit('setup', {
            type: 'pageSize',
            value: pageSize
        })
    },
    setTotalItems({commit}, totalItems) {
        commit('setup', {
            type: 'totalItems',
            value: totalItems
        })
    },
    addToTotalItems({commit}, count) {
        commit('increment', {
            type: 'totalItems',
            value: count
        })
    }
}

let state = () => {
    return {
        sortOrder: {},
        currentPage: 1,
        totalItems: 0,
        pageSize: 10,
        filter: ''
    }
}

function Store() {
    return {
        state: state(),
        mutations,
        actions,
        getters
    }
}

function StoreFactory() {
    return () => {
        return new Store()
    }
}

export default StoreFactory()

