import { ref } from 'vue'
import { StockService } from '../../api/stock.service'
import { NewStock, Stock } from './types'

export function useStock() {
    
    const stock = ref<Stock>({} as Stock)
    const api = new StockService()

    const create = async (payload: NewStock) => {
        const { data } = await api.create(payload);
        stock.value = data;
    }

    const remove = async (id: number) => {
        await api.delete(id);
    }

    return {
        stock,
        create,
        remove
    }
}