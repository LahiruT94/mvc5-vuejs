import pagination from '@admin/store/modules/paginationStore'
import {HTTP} from '@shared/config/api'

const state = {
    clientList: []
}

const getters = {
    getClientList: state => state.clientList
}

const mutations = {
    set(state, {type, value}) {
        state[type] = value
    },
    delete(state, {id}) {
        state.clientList = state.clientList.filter(w => w.Id !== id)
    },
    deleteMultiple(state, {ids}) {
        state.clientList = state.clientList.filter(w => ids.indexOf(w.Id) === -1)
    },
    update(state, {client}) {
        let index = state.clientList.findIndex(w => w.Id === client.Id)
        if (index !== -1) {
            state.clientList[index].Title = client.Title
            state.clientList[index].Email = client.Email
            state.clientList[index].Phone = client.Phone
            state.clientList[index].Note = client.Note
        }
    }
}

const actions = {
    getClients({commit, dispatch, getters}) {
        HTTP.get('api/Client', {params: getters.getParams})
        .then((response) => {
            commit('set', {
                type: 'clientList',
                value: response.data.Items
            })
            dispatch('setTotalItems', response.data.Total)
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    createClient({dispatch}, client) {
        HTTP.post('api/Client', client)
        .then(() => {
            dispatch('getClients')
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    updateClient({commit}, client) {
        HTTP.patch('api/Client', client)
        .then(() => {
            commit('update', {client: client})
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    deleteClient({commit, dispatch}, client) {
        HTTP.delete('api/Client', client)
        .then(() => {
            commit('delete', {id: client.params.id})
            dispatch('addToTotalItems', -1)
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    deleteMultipleClient({commit, dispatch}, clients) {
        HTTP.delete('api/Client', clients)
        .then(() => {
            commit('deleteMultiple', {ids: clients.params.ids})
            dispatch('addToTotalItems', -clients.params.ids.length)
        })
        .catch((error) => {
            window.console.error(error)
        })
    }
}


export default {
    namespaced: true,
    state,
    mutations,
    actions,
    getters,
    modules: {
        Pagination: pagination()
    }
}
