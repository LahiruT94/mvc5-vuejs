import pagination from '@admin/store/modules/paginationStore'
import {HTTP} from '@shared/config/api'

const state = {
    accessList: [],
}

const getters = {
    getAccessList: state => state.accessList,
}

const mutations = {
    set(state, {type, value}) {
        state[type] = value
    },
    delete(state, {id}) {
        state.accessList = state.accessList.filter(w => w.Id !== id)
    },
    deleteMultiple(state, {ids}) {
        state.accessList = state.accessList.filter(w => ids.indexOf(w.Id) === -1)
    },
    update(state, {access}) {
        let index = state.accessList.findIndex(w => w.Id === access.Id)
        if (index !== -1) {
            state.accessList[index].Address = access.Address
            state.accessList[index].Login = access.Login
            state.accessList[index].Password = access.Password
            state.accessList[index].Note = access.Note
            state.accessList[index].AccessTypeId = access.AccessTypeId
            state.accessList[index].ProjectId = access.ProjectId
        }
    }
}

const actions = {
    getAccess({commit, dispatch, getters}) {
        HTTP.get('api/Access', {params: getters.getParams})
        .then((response) => {
            commit('set', {
                type: 'accessList',
                value: response.data.Items
            })
            dispatch('setTotalItems', response.data.Total, null, {root: true})
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    createAccess({dispatch}, access) {
        let model = {
            Id: access.Id,
            Address: access.Address,
            Login: access.Login,
            Password: access.Password,
            Note: access.Note,
            AccessTypeId: access.AccessType.Id,
            ProjectId: access.Project.Id
        }
        HTTP.post('api/Access', model)
        .then(() => {
            dispatch('getAccess')
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    updateAccess({commit}, access) {
        let model = {
            Id: access.Id,
            Address: access.Address,
            Login: access.Login,
            Password: access.Password,
            Note: access.Note,
            AccessTypeId: access.AccessType.Id,
            ProjectId: access.Project.Id
        }
        HTTP.patch('api/Access', model)
        .then(() => {
            commit('update', {access: model})
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    deleteAccess({dispatch, commit}, access) {
        HTTP.delete('api/Access', access)
        .then(() => {
            commit('delete', {id: access.params.id})
            dispatch('addToTotalItems', -1)
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    deleteMultipleAccess({dispatch, commit}, access) {
        HTTP.delete('api/Access', access)
        .then(() => {
            commit('deleteMultiple', {ids: access.params.ids})
            dispatch('addToTotalItems', -access.params.ids.length)
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
