import Vue from 'vue'
import Vuex from 'vuex'
import { HTTP } from '@shared/config/api.js'

Vue.use(Vuex)

const state = {
    accessTypeList: [],
    sortOrder: {},
    currentPage: 1,
    totalItems: 0,
    pageSize: 10,
    filter: ''
}

const getters = {
    getAccessTypeList: state => state.accessTypeList,
    getTotalItems: state => state.totalItems,
    getCurrentPage: state => state.currentPage,
    getPageSize: state => state.pageSize
}

const mutations = {
    set(state, { type, value }) {
        state[type] = value
    },
    delete(state, { id }) {
        state.accessTypeList = state.accessTypeList.filter(w => w.Id != id)
        state.totalItems-=1
    },
    deleteMultiple(state, { ids }) {
        state.accessTypeList = state.accessTypeList.filter(w => ids.indexOf(w.Id) === -1)
        state.totalItems-=ids.length
    },
    update(state, { accessType }) {
        let index = state.accessTypeList.findIndex(w => w.Id === accessType.Id)
        if (index != -1) {
            state.accessTypeList[index].Title = accessType.Title
        }
    }
}

const actions = {
    setOrder({ commit }, sortOrder) {
        commit('set', {
            type: 'sortOrder',
            value: sortOrder
        })
    },
    setFilter({ commit }, value) {
        commit('set', {
            type: 'filter',
            value: value
        })
    },
    setPage({ commit }, page) {
        commit('set', {
            type: 'currentPage',
            value: page
        })
    },
    setPageSize({ commit }, pageSize) {
        commit('set', {
            type: 'pageSize',
            value: pageSize
        })
    },
    getAccessType({ commit }) {
        let query = {
            params: {
                search: this.state.filter,
                page: this.state.currentPage,
                pageSize: this.state.pageSize,
                sortColumn: this.state.sortOrder.prop != undefined ? this.state.sortOrder.prop : '',
                sortOrder: this.state.sortOrder.order != undefined ? this.state.sortOrder.order : '',
            }
        }
        HTTP.get('api/AccessType', query)
            .then((response) => {
                commit('set', {
                    type: 'accessTypeList',
                    value: response.data.Model.Items
                })
                commit('set', {
                    type: 'totalItems',
                    value: response.data.Model.Total
                })
            })
            .catch((error) => {
                window.console.error(error)
            })
    },
    createAccessType({ dispatch }, accessType) {
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
