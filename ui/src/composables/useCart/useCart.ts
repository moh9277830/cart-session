import { ref } from 'vue'
import { CartService } from '../../api/cart.service'
import { CartItem } from './types'

const cart = ref<CartItem[]>([])

export function useCart() {    
    const api = new CartService()

    const addItem = async (payload: { productId: number, stockId: number, qty: number}) => {
        await api.create(payload);
        await refresh();
    }

    const updateItem = async (payload: { productId: number, stockId: number, qty: number}) => {
        await api.update(payload);
        await refresh();
    }
    
    const removeItem = async (stockId: number, qty: number) => {
        await api.remove(stockId, qty);
        await refresh();
    }

    const removeOne = async (stockId: number) => {
        await api.remove(stockId, 1);
        await refresh();
    }

    const refresh = async () => {
        const { data } = await api.get();
        cart.value = data;
    }

    // add: (product: Product, quantity: number) => void
    // remove: (product: Product) => void
    // clear: () => void

    return {
        cart,
        addItem,
        updateItem,
        removeItem,
        removeOne,
        refresh
    }
}