import { ref } from 'vue'
import { StockService } from '../../api/stock.service'
import { Product } from '../useProduct'
import { Stock } from './types'

export function useStockList() {
    
    const products = ref<Product[]>([])
    const api = new StockService()

    const search = async () => {
        const { data } = await api.search();
        products.value = data;
    }

    const update = async (payload: { stock: Stock[] }) => {
        await api.update(payload);
        const { data } = await api.search();
        products.value = data;
    }

    return {
        products,
        search,
        update
    }
}