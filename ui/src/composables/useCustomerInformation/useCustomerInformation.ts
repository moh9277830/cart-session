import { ref } from 'vue'
import { CustomerInformation } from './types'

export function useCustomerInformation() {    
    const customerInformation = ref<CustomerInformation>({} as CustomerInformation)

    return {
        customerInformation
    }
}