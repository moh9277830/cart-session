import { App } from 'vue';
import { useCart } from '../composables/useCart'

const loadCart = { 
    install: async (_app: App) => {
        const { refresh } = useCart();
        
        await refresh();
    }
}

export default loadCart;