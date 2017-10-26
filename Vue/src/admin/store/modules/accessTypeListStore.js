import pagination from '@admin/store/modules/paginationStore'
import {HTTP} from '@shared/config/api'

const state = {
    accessTypeList: []
}

const getters = {
    getAccessTypeList: state => state.accessTypeList
}

const mutations = {
    set(state, {type, value}) {
        state[type] = value
    },
    delete(state, {id}) {
        state.accessTypeList = state.accessTypeList.filter(w => w.Id !== id)
    },
    deleteMultiple(state, {ids}) {
        state.accessTypeList = state.accessTypeList.filter(w => ids.indexOf(w.Id) === -1)
    },
    update(state, {accessType}) {
        let index = state.accessTypeList.findIndex(w => w.Id === accessType.Id)
        if (index !== -1) {
            state.accessTypeList[index].Title = accessType.Title
        }
    }
}

const actions = {
    getAccessTypes({commit, dispatch, getters}) {
        HTTP.get('api/AccessType', {params: getters.getParams})
        .then((response) => {
            commit('set', {
                type: 'accessTypeList',
                value: response.data.Items
            })
            dispatch('setTotalItems', response.data.Total)
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    createAccessType({dispatch}, accessType) {
        HTTP.post('api/AccessType', accessType)
        .then(() => {
            dispatch('getAccessTypes')
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    updateAccessType({commit}, accessType) {
        HTTP.patch('api/AccessType', accessType)
        .then(() => {
            commit('update', {accessType: accessType})
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    deleteAccessType({commit, dispatch}, accessType) {
        HTTP.delete('api/AccessType', accessType)
        .then(() => {
            commit('delete', {id: accessType.params.id})
            dispatch('addToTotalItems', -1)
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    deleteMultipleAccessTypes({commit, dispatch}, accessTypes) {
        HTTP.delete('api/AccessType', accessTypes)
        .then(() => {
            commit('deleteMultiple', {ids: accessTypes.params.ids})
            dispatch('addToTotalItems', -accessTypes.params.ids.length)
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
