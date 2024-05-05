<script lang="ts">
import { defineComponent } from 'vue'
import AdminLayout from '../../layouts/admin.vue'

export default defineComponent({
    name: 'pages-admin-orders',
    components: {
        AdminLayout
    },
    setup() {

        return { 
            AdminLayout
        }
    }
})
</script>

<template>
    <div class="text-gray-200">
        <h1>orders</h1>
    </div>
</template>