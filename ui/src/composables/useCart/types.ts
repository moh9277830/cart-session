export interface Cart {
    items: CartItem[]
    total: number
}

export interface CartItem {
    productId: number,
    name: string,
    value: string,
    realValue: number,
    qty: number,
    stockId: number
}

export interface Product {  
    id: number
    name: string
    price: number
    description: string
    image: string
}