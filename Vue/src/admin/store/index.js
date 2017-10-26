import Vue from 'vue'
import Vuex from 'vuex'
import Access from '@admin/store/modules/accessListStore'
import AccessType from '@admin/store/modules/accessTypeListStore'
import Clients from '@admin/store/modules/clientListStore'
import Projects from '@admin/store/modules/projectListStore'

Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        Access,
        AccessType,
        Clients,
        Projects
    },
})
