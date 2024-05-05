<script lang="ts">
import { defineComponent, onMounted } from 'vue'
import { useCart } from '../composables/useCart'
import { ShoppingCartIcon } from '@heroicons/vue/24/outline'
import api from '../api/api.config'
import { useStockList } from '../composables/useStock'
import { Product } from '../composables/useProduct'

export default defineComponent({
    name: 'home',
    components: {
        ShoppingCartIcon
    },
    setup() {
        const { cart, addItem } = useCart()
        const { products, search } = useStockList()

        onMounted(async () => {
            await search()
        })

        const handleAddToCart = async (product: Product) => {
            await addItem({ 
                productId: product.id,
                stockId: product.stocks[0].id, 
                qty: 1
            })
        }

        const handleRequest = async (status: number) => {
            await api.get(`/status/${status}`);
        }

        return {
            products,
            cart,
            handleAddToCart,
            handleRequest,
        }
    }
})
</script>

<template>
    <div class="mx-auto max-w-7xl overflow-hidden sm:px-6 lg:px-8">
        <section aria-labelledby="products-heading" class="mx-auto max-w-7xl overflow-hidden sm:px-6 lg:px-8">
            <h2 id="products-heading" class="sr-only">Products</h2>
            <!-- <div class="-mx-px grid grid-cols-2 border-l border-gray-200 sm:mx-0 md:grid-cols-3 lg:grid-cols-4"> -->
            <div class="mt-8 -mx-px grid grid-cols-2 border-l border-t border-gray-200 sm:mx-0 md:grid-cols-3 lg:grid-cols-4">
                <div v-for="product in products" :key="product.id" class="group relative border-b border-r border-gray-200 p-4 sm:p-6">
                    <div class="aspect-h-1 aspect-w-1 overflow-hidden rounded-lg bg-gray-200 group-hover:opacity-75">
                        <svg class="h-full w-full text-gray-200 dark:text-gray-600" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 18">
                            <path d="M18 0H2a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h16a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2Zm-5.5 4a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3Zm4.376 10.481A1 1 0 0 1 16 15H4a1 1 0 0 1-.895-1.447l3.5-7A1 1 0 0 1 7.468 6a.965.965 0 0 1 .9.5l2.775 4.757 1.546-1.887a1 1 0 0 1 1.618.1l2.541 4a1 1 0 0 1 .028 1.011Z"/>
                        </svg>
                    </div>
                    <div class="pb-4 pt-10 text-center">
                        <h3 class="text-sm font-medium text-gray-200">
                            <router-link :to="{ name: 'product', params: { id: product.id } }">
                                <span aria-hidden="true" class="absolute inset-0" />
                                {{ product.name }}
                            </router-link>
                        </h3>
                        <div>
    
                        </div>
                        <p class="mt-4 text-base font-medium text-gray-200">{{ product.unitPrice }}</p>
                    </div>
                </div>
                
            </div>
        </section>
        
        <div class="mt-8 bg-gray-500 rounded-md p-4">
            <div class="mt-4 space-x-2">
                <button @click="handleRequest(500)"
                    class="rounded-md bg-indigo-600 px-2.5 py-1.5 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">500</button>
                <button @click="handleRequest(401)"
                    class="rounded-md bg-indigo-600 px-2.5 py-1.5 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">401</button>
                <button @click="handleRequest(403)"
                    class="rounded-md bg-indigo-600 px-2.5 py-1.5 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">403</button>
                <button @click="handleRequest(404)"
                    class="rounded-md bg-indigo-600 px-2.5 py-1.5 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">404</button>
            </div>
        </div>
    </div>
</template>