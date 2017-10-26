import pagination from '@admin/store/modules/paginationStore'
import {HTTP} from '@shared/config/api'


const state = {
    projectList: [],
}

const getters = {
    getProjectList: state => state.projectList,
}

const mutations = {
    set(state, {type, value}) {
        state[type] = value
    },
    delete(state, {id}) {
        state.projectList = state.projectList.filter(w => w.Id !== id)
    },
    deleteMultiple(state, {ids}) {
        state.projectList = state.projectList.filter(w => ids.indexOf(w.Id) === -1)
    },
    update(state, {project}) {
        let index = state.projectList.findIndex(w => w.Id === project.Id)
        if (index !== -1) {
            state.projectList[index].Title = project.Title
            state.projectList[index].ClientId = project.ClientId
        }
    }
}

const actions = {
    getProjects({commit, dispatch, getters}) {
        HTTP.get('api/Project', {params: getters.getParams})
        .then((response) => {
            commit('set', {
                type: 'projectList',
                value: response.data.Items
            })
            dispatch('setTotalItems', response.data.Total, null, {root: true})
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    createProject({dispatch}, project) {
        let model = {
            Id: project.Id,
            Title: project.Title,
            ClientId: project.Client.Id
        }
        HTTP.post('api/Project', model)
        .then(() => {
            dispatch('getProjects')
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    updateProject({commit}, project) {
        let model = {
            Id: project.Id,
            Title: project.Title,
            ClientId: project.Client.Id
        }
        HTTP.patch('api/Project', model)
        .then(() => {
            commit('update', {project: model})
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    deleteProject({dispatch, commit}, project) {
        HTTP.delete('api/Project', project)
        .then(() => {
            commit('delete', {id: project.params.id})
            dispatch('addToTotalItems', -1)
        })
        .catch((error) => {
            window.console.error(error)
        })
    },
    deleteMultipleProjects({dispatch, commit}, projects) {
        HTTP.delete('api/Project', projects)
        .then(() => {
            commit('deleteMultiple', {ids: projects.params.ids})
            dispatch('addToTotalItems', -projects.params.ids.length)
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
