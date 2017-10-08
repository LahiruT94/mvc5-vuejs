import Vue from "vue";
import AccessType from "@admin/AccessType/index.vue";
import { HTTP } from "@shared/config/api.js";
import ElementUI from "element-ui";
import $ from "../../static/js/jquery-3.2.1.min.js"
import store from '@admin/store/index.js'
import 'element-ui/lib/theme-default/index.css'
Vue.prototype.$http = HTTP;
Vue.prototype.$ = $;

Vue.config.productionTip = false;

Vue.use(ElementUI);
new Vue({
  el: "#app",
  template: "<div class='container'><App/></div>",
  components: { App: AccessType },
  store
});
