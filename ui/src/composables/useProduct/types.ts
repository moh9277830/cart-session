import { Stock } from "../useStock"

export interface NewProduct {
    name: string
    unitPrice: number
}

export interface Product extends NewProduct {  
    id: number
    stocks: Stock[]
}