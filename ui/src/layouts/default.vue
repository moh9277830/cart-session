<script lang="ts">
import { defineComponent } from 'vue'
import { ShoppingBagIcon } from '@heroicons/vue/24/outline'
import { useCart } from '../composables/useCart'

export default defineComponent({
  name: 'layout-default',
  components: {
    ShoppingBagIcon
  },
  setup() {
    const { cart } = useCart()

    return { cart }
  }
})
</script>

<template>
    <div>
        <header class="relative">
            <nav aria-label="Top">
                <!-- Top navigation -->
                <div class="bg-gray-900">
                    <div class="mx-auto flex h-10 max-w-7xl items-center justify-between px-4 sm:px-6 lg:px-8">
                        <div></div>
                        <div class="flex items-center space-x-6">
                            <router-link :to="{ name: 'login' }" class="text-sm font-medium text-white hover:text-gray-100">Sign in</router-link>
                            <router-link :to="{ name: 'signup' }" class="text-sm font-medium text-white hover:text-gray-100">Create an account</router-link>
                        </div>
                    </div>
                </div>

                <!-- Secondary navigation -->
                <div class="bg-white">
                    <div class="mx-auto max-w-7xl sm:px-6 lg:px-8">
                        <div class="border-b border-gray-200 px-4 pb-14 sm:px-0 sm:pb-0">
                            <div class="flex h-16 items-center justify-between">
                                <!-- Logo -->
                                <div class="flex flex-1">
                                    Logo
                                </div>
                                <!-- Menus -->
                                <div class="flex">
                                    <router-link :to="{ name: 'home' }" class="text-sm font-medium text-gray-900 hover:text-gray-900">Home</router-link>
                                    <router-link :to="{ name: 'admin' }" class="ml-8 text-sm font-medium text-gray-900 hover:text-gray-900">Admin Test</router-link>
                                </div>
                                <div class="flex flex-1 items-center justify-end">
                                    <!-- Search -->
            
                                    <!-- Cart -->
                                    <div class="ml-4 flow-root lg:ml-8">
                                        <router-link :to="{ name: 'cart' }" class="group -m-2 flex items-center p-2">
                                            <ShoppingBagIcon class="h-6 w-6 flex-shrink-0 text-gray-400 group-hover:text-gray-500" aria-hidden="true" />
                                            <span class="ml-2 text-sm font-medium text-gray-700 group-hover:text-gray-800">{{ cart.length }}</span>
                                            <span class="sr-only">items in cart, view bag</span>
                                        </router-link>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </nav>
        </header>
        <main>
            <slot name="default" />
        </main>
    </div>
</template>