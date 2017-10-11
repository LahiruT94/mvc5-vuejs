import Vue from "vue";
import AccessTypeList from "@admin/AccessType/index.vue";
import ElementUI from "element-ui";
import store from '@admin/store/index.js'
import 'element-ui/lib/theme-chalk/index.css'
import locale from 'element-ui/lib/locale/lang/ru-RU'

Vue.config.productionTip = false;

Vue.use(ElementUI, { locale });

new Vue({
  el: "#app",
  components: { "access-type-list" : AccessTypeList },
  store
});

