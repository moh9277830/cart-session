import { createRouter, createWebHistory } from 'vue-router'
import routes from './routes'
import { layoutMiddleware } from './middlerware'

const router = createRouter({
    history: createWebHistory(import.meta.env.Base_URL),
    routes
})

router.beforeEach(layoutMiddleware)

export default router