<script lang="ts">
import { defineComponent } from 'vue';
import { useCustomerInformation } from '../../composables/useCustomerInformation';

export default defineComponent({
    name: 'CustomerInformation',
    setup() {
        const { customerInformation } = useCustomerInformation();

        return {
            customerInformation
        };
    }
});
</script>

<template>
    <div class="bg-gray-50">

        <div class="mx-auto max-w-7xl px-4 pb-24 pt-16 sm:px-6 lg:px-8">
            <div class="mx-auto max-w-2xl lg:max-w-none">
                <form class="lg:grid lg:grid-cols-2 lg:gap-x-12 xl:gap-x-16">
                    <div>
                        <div class="mt-10 border-t border-gray-200 pt-10">
                            <h2 class="text-lg font-medium text-gray-900">Customer information</h2>
                    
                            <div class="mt-4 grid grid-cols-1 gap-y-6 sm:grid-cols-2 sm:gap-x-4">
                                <div>
                                    <label for="first-name" class="block text-sm font-medium text-gray-700">First name</label>
                                    <div class="mt-1">
                                    <input type="text" id="first-name" name="first-name" v-model="customerInformation.firstName" autocomplete="given-name" class="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm" />
                                    </div>
                                </div>
                    
                                <div>
                                    <label for="last-name" class="block text-sm font-medium text-gray-700">Last name</label>
                                    <div class="mt-1">
                                    <input type="text" id="last-name" name="last-name" v-model="customerInformation.lastName" autocomplete="family-name" class="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm" />
                                    </div>
                                </div>
                    
                                <div class="sm:col-span-2">
                                    <label for="email" class="block text-sm font-medium text-gray-700">Email</label>
                                    <div class="mt-1">
                                    <input type="text" name="email" id="email" v-model="customerInformation.email" autocomplete="email" class="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm" />
                                    </div>
                                </div>
                    
                                <div class="sm:col-span-2">
                                    <label for="address1" class="block text-sm font-medium text-gray-700">Address 1</label>
                                    <div class="mt-1">
                                    <input type="text" name="address1" id="address1" v-model="customerInformation.address1" autocomplete="street-address" class="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm" />
                                    </div>
                                </div>
                    
                                <div class="sm:col-span-2">
                                    <label for="address2" class="block text-sm font-medium text-gray-700">Address 2</label>
                                    <div class="mt-1">
                                    <input type="text" name="address2" id="address2" v-model="customerInformation.address2" autocomplete="address-level2" class="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm" />
                                    </div>
                                </div>
                    
                                <div>
                                    <label for="country" class="block text-sm font-medium text-gray-700">Country</label>
                                    <div class="mt-1">
                                    <select id="country" name="country" autocomplete="country-name" class="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm">
                                        <option>United States</option>
                                        <option>Canada</option>
                                        <option>Mexico</option>
                                    </select>
                                    </div>
                                </div>
                    
                                <div>
                                    <label for="region" class="block text-sm font-medium text-gray-700">State / Province</label>
                                    <div class="mt-1">
                                    <input type="text" name="region" id="region" autocomplete="address-level1" class="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm" />
                                    </div>
                                </div>
                    
                                <div>
                                    <label for="postal-code" class="block text-sm font-medium text-gray-700">Postal code</label>
                                    <div class="mt-1">
                                    <input type="text" name="postal-code" id="postal-code" autocomplete="postal-code" class="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm" />
                                    </div>
                                </div>
                    
                                <div class="sm:col-span-2">
                                    <label for="phone" class="block text-sm font-medium text-gray-700">Phone</label>
                                    <div class="mt-1">
                                    <input type="text" name="phone" id="phone" autocomplete="tel" class="block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>