<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'
import AdminLayout from '../../layouts/admin.vue'
import { NewProduct, Product, useProduct, useProductList } from '../../composables/useProduct'

export default defineComponent({
    name: 'pages-admin-products',
    components: {
        AdminLayout
    },
    setup() {
        const { products, search } = useProductList()
        const { create, remove } = useProduct()
        const selectedProduct = ref<Product | null>(null)
        const newProduct = ref<NewProduct>({
            name: '',
            unitPrice: 0
        })

        onMounted(async () => {
            await search()
        })

        const handleSelectProduct = (product: Product) => {
            selectedProduct.value = product
        }

        const handleAddProduct = async (e: Event) => {
            e.preventDefault()
            await create(newProduct.value)
            await search()
        }

        const handleRemoveProduct = async (id: number) => {
            await remove(id)
            await search()
        }

        return { 
            AdminLayout,
            products,
            newProduct,
            handleSelectProduct,
            handleAddProduct,
            handleRemoveProduct
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
            <div class=" grid grid-cols-1 gap-4 lg:col-span-2">
                <section v-for="product in products" :key="product.id" 
                    class="bg-gray-800 p-4 rounded-lg"
                    @click="handleSelectProduct(product)">
                    <h2 class="text-xl font-semibold">{{ product.name }}</h2>
                    <p class="text-gray-400">{{ product.unitPrice }}</p>
                    <button @click="handleRemoveProduct(product.id)" class="p-2 bg-gray-700 text-gray-200 rounded-lg">delete</button>
                </section>
            </div>

            <!-- Right column -->
            <div class="bg-gray-300 text-gray-800 rounded-md grid grid-cols-1 gap-4">
                <section class="p-4">
                    <h2 class="text-xl font-semibold text-gray-800">Add Product</h2>
                    <form class="mt-4 flex flex-col gap-2">
                        <div class="flex flex-col">
                            <label for="name" class="text-gray-700">Name</label>
                            <input type="text" v-model="newProduct.name" id="name" name="name" class="p-2 rounded-lg bg-gray-800 text-gray-200" />
                        </div>
                        <div class="flex flex-col">
                            <label for="unitPrice" class="text-gray-700">Unit Price</label>
                            <input type="number" min="0" max="1000" v-model="newProduct.unitPrice" id="unitPrice" name="unitPrice" class="p-2 rounded-lg bg-gray-800 text-gray-200" />
                        </div>
                        <!-- <div class="flex flex-col">
                            <label for="description" class="text-gray-700">Description</label>
                            <textarea id="description" name="description" class="p-2 rounded-lg bg-gray-800 text-gray-200"></textarea>
                        </div> -->
                        <!-- <div class="flex flex-col">
                            <label for="image" class="text-gray-700">Image</label>
                            <input type="file" id="image" name="image" class="p-2 rounded-lg bg-gray-800 text-gray-200" />
                        </div> -->
                        <div class="flex flex-col">
                            <button type="submit" @click="handleAddProduct" class="p-2 bg-gray-800 text-gray-200 rounded-lg">Add Product</button>
                        </div>
                    </form>
                </section>
            </div>
        </div>
    </div>
</template>