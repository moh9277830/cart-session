<script lang="ts">
import { defineComponent, onMounted } from 'vue'
import { useRouter } from "vue-router";
import { useLayout } from '../composables/useLayout';

export default defineComponent({
    name: 'layout-admin',
    setup() {
        const router = useRouter()
        const layout = useLayout()

        onMounted(() => {
            layout.setLayout('admin')
            console.log('admin layout')
        })

        return { 
            router
        }
    }
})
</script>

<template>
    <div class="text-gray-200">
        <h1>Admin Layout</h1>
        <div class="space-x-2">
            <button @click="router.push({ name: 'AdminDashboard' })" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
                Dashboard
            </button>
            <button @click="router.push({ name: 'AdminProducts' })" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
                Products
            </button>
            <button @click="router.push({ name: 'AdminOrders' })" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
                Orders
            </button>
            <button @click="router.push({ name: 'AdminStock' })" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
                Stock
            </button>
        </div>
        <div class="mt-8 max-w-7xl m-auto bg-gray-500 rounded-md p-4">
            <slot name="default" />
        </div>
    </div>
</template>