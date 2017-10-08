import Vue from 'vue'
import Vuex from 'vuex'
import { HTTP } from "@shared/config/api.js"

Vue.use(Vuex)

const state = {
    accessTypeList: [],
    currentPage: 1,
    pageSize: 10,
}

const getters = {
    getAccessTypeList: state => state.accessTypeList
}

const mutations = {
    set(state, { type, items }) {
        state[type] = items
    },
    delete(state, { id }) {
        state.accessTypeList = state.accessTypeList.filter(w => w.Id != id)
    },
    deleteMultiple(state, { ids }) {
        state.accessTypeList = state.accessTypeList.filter(w => ids.indexOf(w.Id) === -1 )
    },
    update(state, { accessType }) {
        let index = state.accessTypeList.findIndex(w => w.Id === accessType.Id)
        if (index != -1) {
            state.accessTypeList[index].Title = accessType.Title
        }
    }
}

const actions = {
    getAccessType({ commit }) {
        HTTP.get('api/AccessType')
            .then((response) => {
                commit('set', {
                    type: 'accessTypeList',
                    items: response.data.items
                })
            })
            .catch((error) => {
                window.console.error(error)
            })
    },
    createAccessType({ dispatch, commit }, accessType) {
        HTTP.post('api/AccessType', accessType)
            .then(() => {
                dispatch('getAccessType')
            })
            .catch((error) => {
                window.console.error(error)
            })
    },
    updateAccessType({ commit }, accessType) {
        HTTP.patch('api/AccessType', accessType)
            .then(() => {
                commit('update', { accessType: accessType })
            })
            .catch((error) => {
                window.console.error(error)
            })
    },
    deleteAccessType({ commit }, accessType) {
        HTTP.delete('api/AccessType', accessType)
            .then(() => {
                commit('delete', { id: accessType.params.id })
            })
            .catch((error) => {
                window.console.error(error)
            })
    },
    deleteMultipleAccessTypes({ commit }, accessTypes) {
        HTTP.delete('api/AccessType', accessTypes)
            .then(() => {
                commit('deleteMultiple', { ids: accessTypes.params.ids })
            })
            .catch((error) => {
                window.console.error(error)
            })
    }
}


export default new Vuex.Store({
    state,
    mutations,
    actions,
    getters
})
