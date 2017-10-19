import Vue from "vue";
import AccessTypeList from "@admin/AccessType/index.vue";
import ClientList from "@admin/Clients";
import ProjectList from "@admin/Projects";
import ElementUI from "element-ui";
import store from '@admin/store'
import locale from 'element-ui/lib/locale/lang/ru-RU'
import 'element-ui/lib/theme-chalk/index.css'
import vSelect from 'vue-select'

Vue.config.productionTip = false;

Vue.use(ElementUI, { locale });
Vue.component('v-select', vSelect)

new Vue({
  el: "#app",
  components: {
    "access-type-list": AccessTypeList,
    "clients-list": ClientList,
    "projects-list": ProjectList
  },
  store
});

