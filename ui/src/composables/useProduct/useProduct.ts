import { ref } from 'vue'
import { ProductService } from '../../api/product.service'
import { NewProduct, Product } from './types'

export function useProduct() {
    
    const product = ref<Product>({} as Product)
    const api = new ProductService()

    const load = async (id: number) => {
        const { data } = await api.get(id);
        product.value = data;
    }

    const create = async (payload: NewProduct) => {
        const { data } = await api.create(payload);
        product.value = data;
    }

    const update = async (payload: Product) => {
        const { data } = await api.update(payload);
        product.value = data;
    }

    const remove = async (id: number) => {
        await api.delete(id);
    }

    return {
        product,
        load,
        create,
        update,
        remove
    }
}