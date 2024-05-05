import { RouteRecordRaw } from "vue-router"

const routes: RouteRecordRaw[] = [
    {
        path: '/',
        name: 'home',
        component: () => import('../pages/home.vue')
    },
    {
        path: '/login',
        name: 'login',
        component: () => import('../pages/login.vue')
    },
    {
        path: '/signup',
        name: 'signup',
        component: () => import('../pages/signup.vue')
    },
    {
        path: '/product/:id',
        name: 'product',
        props: true,
        component: () => import('../pages/product.vue')
    },
    {
        path: '/cart',
        name: 'cart',
        component: () => import('../pages/cart.vue')
    },
    {
        path: '/checkout',
        name: 'checkout',
        component: () => import('../pages/checkout/index.vue'),
        redirect: '/customer-information',
        children: [
            {
                path: '/customer-information',
                name: 'customer',
                component: () => import('../pages/checkout/customer-information.vue')
            },
            {
                path: '/payment',
                name: 'payment',
                component: () => import('../pages/checkout/payment.vue')
            }
        ]
    },
    {
        path: '/403',
        name: 'forbidden',
        component: () => import('../pages/403.vue')
    },
    {
        path: '/404',
        name: 'not-found',
        component: () => import('../pages/404.vue')
    },
    {
        path: '/500',
        name: 'server-error',
        component: () => import('../pages/500.vue')
    },
    {
        path: '/admin',
        name: 'admin',
        meta: { layout: 'admin' },
        redirect() {
            return { name: 'AdminDashboard' }
        },
        children: [
            {
                path: '/admin/',
                name: 'AdminDashboard',
                component: () => import('../pages/admin/index.vue')
            },
            {
                path: '/admin/products',
                name: 'AdminProducts',
                component: () => import('../pages/admin/products.vue')
            },
            {
                path: '/admin/orders',
                name: 'AdminOrders',
                component: () => import('../pages/admin/orders.vue')
            },
            {
                path: '/admin/stock',
                name: 'AdminStock',
                component: () => import('../pages/admin/stock.vue')
            }
        ],
    }
]

export default routes