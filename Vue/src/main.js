import Vue from 'vue'
import App from './App'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import locale from 'element-ui/lib/locale/lang/ru-RU'

Vue.config.locale = 'ru-RU'
Vue.use(ElementUI, {locale})
Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
    el: '#app',
    render: h => h(App)
})
