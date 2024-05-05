<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'
import { Product, useProduct } from '../composables/useProduct'
import { CheckIcon } from '@heroicons/vue/20/solid'
import { useCart } from '../composables/useCart'

export default defineComponent({
    name: 'product',
    components: { CheckIcon },
    props: {
        id: {
            type: Number,
            required: true
        }
    },
    setup(props) {
        const { product, load } = useProduct()
        const { addItem } = useCart()
        const selectedStock = ref<number>(0)
        const selectedQty = ref<number>(1)

        onMounted(async () => {
            await load(props.id)

            if (product.value.stocks.length > 0) {
                selectedStock.value = product.value.stocks[0].id
            }
        })

        const handleAddToCart = async (e: Event, productId: number) => {
            e.preventDefault()
            
            await addItem({ 
                productId: productId,
                stockId: selectedStock.value, 
                qty: selectedQty.value
            })
        }

        return {
            product,
            selectedStock,
            selectedQty,
            handleAddToCart
        }
    }
})
</script>

<template>
    <!-- Product -->
    <div class="bg-white">
        <div class="mx-auto max-w-2xl px-4 pb-24 pt-16 sm:px-6 sm:pb-32 sm:pt-24 lg:grid lg:max-w-7xl lg:grid-cols-2 lg:gap-x-8 lg:px-8">
            <!-- Product details -->
            <div class="lg:max-w-lg lg:self-end">
                <nav></nav>
                <div class="mt-4">
                    <h1 class="text-3xl font-bold tracking-tight text-gray-900 sm:text-4xl">{{ product.name }}</h1>
                </div>
                <section aria-labelledby="information-heading" class="mt-4">
                    <h2 id="information-heading" class="sr-only">Product information</h2>
                    <div class="flex items-center">
                        <p class="text-lg text-gray-900 sm:text-xl">{{ product.unitPrice }}</p>
                    </div>
                    <div class="mt-4 space-y-6">
                        <!-- <p class="text-base text-gray-500">{{ product.description }}</p> -->
                        <p class="text-base text-gray-500">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Metus dictum at tempor commodo ullamcorper a. Nam libero justo laoreet sit amet cursus sit.</p>
                    </div>
                    <div v-if="product.stocks[0].qty > 0" class="mt-6 flex items-center">
                        <CheckIcon class="h-5 w-5 flex-shrink-0 text-green-500" aria-hidden="true" />
                        <p class="ml-2 text-sm text-gray-500">In stock and ready to ship</p>
                    </div>
                </section>
            </div>

             <!-- Product image -->
            <div class="mt-10 lg:col-start-2 lg:row-span-2 lg:mt-0 lg:self-center">
                <div class="aspect-h-1 aspect-w-1 overflow-hidden rounded-lg">
                    <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mO8Ww8AAj8BXkQ+xPEAAAAASUVORK5CYII=" width="20" height="20" alt="Transparent image"/>
                </div>
            </div>

            <!-- Product form -->
            <div class="mt-10 lg:col-start-1 lg:row-start-2 lg:max-w-lg lg:self-start">
                <section aria-labelledby="options-heading">
                    <h2 id="options-heading" class="sr-only">Product options</h2>
                    <form>
                        <div class="sm:flex sm:justify-between">
                        </div>
                        <div class="mt-4 flex">
                            <div>
                                <label for="size" class="block text-sm font-medium leading-6 text-gray-900">Size</label>
                                <select id="size" name="size" v-model="selectedStock" class="mt-2 block w-full rounded-md border-0 py-1.5 pl-3 pr-10 text-gray-900 ring-1 ring-inset ring-gray-300 focus:ring-2 focus:ring-indigo-600 sm:text-sm sm:leading-6">
                                    <option v-for="stock in product.stocks" :key="stock.id" :value="stock.id">
                                        {{ stock.description }}
                                    </option>
                                </select>
                            </div>
                        </div>
                        <div class="mt-4 flex">
                            <label for="quantity" class="sr-only">Quantity, {{ product.name }}</label>
                            <select id="quantity" name="quantity" v-model="selectedQty" class="max-w-full rounded-md border border-gray-300 py-1.5 text-left text-base font-medium leading-5 text-gray-700 shadow-sm focus:border-indigo-500 focus:outline-none focus:ring-1 focus:ring-indigo-500 sm:text-sm">
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="7">7</option>
                                <option value="8">8</option>
                            </select>
                        </div>
                        <div class="mt-10">
                            <button type="submit" @click="(e) => handleAddToCart(e, product.id)" class="flex w-full items-center justify-center rounded-md border border-transparent bg-indigo-600 px-8 py-3 text-base font-medium text-white hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 focus:ring-offset-gray-50">Add to bag</button>
                        </div>
                    </form>
                </section>
            </div>
        </div>
    </div>
</template>