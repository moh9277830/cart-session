import { computed, ref } from 'vue'
import { ProductService } from '../../api/product.service'
import { Product } from './types'

export function useProductList() {
    
    const products = ref<Product[]>([])
    const api = new ProductService()

    const search = async () => {
        const { data } = await api.search();
        products.value = data;
    }

    return {
        products,
        search
    }
}