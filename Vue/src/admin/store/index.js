import Vue from 'vue'
import Vuex from 'vuex'
import AccessType from '@admin/store/modules/accessTypeListStore'
import Clients from '@admin/store/modules/clientListStore'
import Projects from '@admin/store/modules/projectListStore'

Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    AccessType,
    Clients,
    Projects
  },
})