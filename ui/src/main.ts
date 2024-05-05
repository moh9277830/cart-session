import { createApp } from 'vue'
import './style.css' 
import App from './App.vue'
import router from './router'
import { isAxiosError } from 'axios'
import loadCart from './plugins/loadCart'

const app = createApp(App)

app.use(router)
app.use(loadCart)

app.mount('#app')


app.config.errorHandler = (err, _vm, _info) => {
    if(isAxiosError(err)) {
        switch(err.response?.status) {
            case 401:
                router.push({ name: 'login' })
                break
            case 403:
                router.push({ name: 'forbidden' })
                break
            case 404:
                router.push({ name: 'not-found' })
                break
            case 500:
                router.push({ name: 'server-error' })
                break
            default:
                router.push({ name: 'error' })
        }
    }
}