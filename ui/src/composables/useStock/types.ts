export interface NewStock {
    productId: number
    description: string
    qty: number
}

export interface Stock extends NewStock {
    id: number
}