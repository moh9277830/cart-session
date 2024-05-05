<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'
import AdminLayout from '../../layouts/admin.vue'
import { Product, useProductList } from '../../composables/useProduct'
import { NewStock, Stock, useStock, useStockList } from '../../composables/useStock'

export default defineComponent({
    name: 'pages-admin-stock',
    components: {
        AdminLayout
    },
    setup() {
        // const { products, search } = useProductList()
        const { create, remove, stock: responseStock } = useStock()
        const { update, search, products } = useStockList()
        const selectedProduct = ref<Product | null>(null)
        const newStock = ref<NewStock>({ 
            productId: 0,
            description: 'size', 
            qty: 0
        })

        onMounted(async () => {
            await search()
            console.log(products.value)
        })

        const handleSelectProduct = (product: Product) => {
            selectedProduct.value = product
            newStock.value.productId = product.id
        }

        const handleUpdateStock = async () => {
            var payload = selectedProduct.value?.stocks.map(x => {
                return {
                    id: x.id,
                    productId: x.productId,
                    description: x.description,
                    qty: x.qty
                }
            })

            if(payload)
            {
                await update({ stock: payload })
            }
        }

        const handleNewStock = async () => {
            await create(newStock.value);
            products.value.find(product => product.id === newStock.value.productId)?.stocks.push(responseStock.value)
        }

        const handleDeleteStock = async (id: number, index: number) => {
            await remove(id);
            if (selectedProduct.value) {
                selectedProduct.value.stocks.splice(index, 1)
            }
        }

        return { 
            AdminLayout,
            products,
            selectedProduct,
            newStock,
            handleSelectProduct,
            handleUpdateStock,
            handleNewStock,
            handleDeleteStock
        }
    }
})
</script>

<template>
            <div class="text-gray-200">
                <h1 class="text-xl font-bold uppercase text-gray-300">products</h1>
                
                <!-- Main 3 column grid -->
                <div class="mt-8 grid grid-cols-1 gap-4 lg:grid-cols-3 lg:gap-8">
                    <!-- Left column -->
                    <div class="flex flex-col gap-4 lg:col-span-2">
                        <section v-for="product in products" :key="product.id" 
                            class="bg-gray-800 p-4 rounded-lg cursor-pointer hover:bg-gray-700 transition-colors duration-300 ease-in-out"
                            @click="handleSelectProduct(product)">
                            <h2 class="text-xl font-semibold">{{ product.name }}</h2>
                            <p class="text-gray-400">{{ product.unitPrice }}</p>
                        </section>
                    </div>

                    <!-- Right column -->
                    <div class="bg-gray-300 text-gray-800 rounded-md grid grid-cols-1 gap-4">
                        <section v-if="selectedProduct" class="p-4">
                            <h2 class="text-xl font-semibold text-gray-800">Update Stocks</h2>
                            <div class="mt-4">
                                <div v-for="(stock, stockIdx) in selectedProduct?.stocks" :key="stock.id" class="grid grid-cols-3 gap-2">
                                    <div class="text-gray-700">
                                        <input type="text" v-model="stock.description" class="w-full p-2 rounded-lg bg-gray-800 text-gray-200" />
                                    </div>
                                    <div class="text-gray-700">
                                        <input type="text" v-model="stock.qty" class="w-full p-2 rounded-lg bg-gray-800 text-gray-200" />
                                    </div>
                                    <div class="text-gray-700">
                                        <button @click="handleDeleteStock(stock.id, stockIdx)" class="p-2 bg-red-600 text-gray-200 rounded-lg">Delete</button>
                                    </div>
                                </div>
                                <button @click="handleUpdateStock" class="mt-4 p-2 bg-yellow-600 text-gray-200 rounded-lg">Update Stock</button>
                            </div>
                            <form class="mt-4 grid grid-cols-2 gap-4 bg-slate-400 p-4 rounded-md" @submit.prevent="handleNewStock">
                                <div class="flex flex-col">
                                    <label for="description" class="text-gray-700">Description</label>
                                    <input type="text" id="description" name="description" v-model="newStock.description" class="p-2 rounded-lg bg-gray-800 text-gray-200" />
                                </div>
                                <div class="flex flex-col">
                                    <label for="quantity" class="text-gray-700">Quantity</label>
                                    <input type="text" id="quantity" name="quantity" v-model="newStock.qty" class="p-2 rounded-lg bg-gray-800 text-gray-200" />
                                </div>
                                <div class="col-span-2">
                                    <button type="submit" class="w-full p-2 bg-green-800 text-gray-200 rounded-lg">Add New Stock</button>
                                </div>
                            </form>
                        </section>
                        <section v-else class="p-4">
                            <div>No product selected</div>
                        </section>
                    </div>
                </div>
            </div>
</template>