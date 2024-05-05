import { Cart, CartItem } from "../composables/useCart";
import { AxiosResponse } from "axios";
import api from "./api.config";

export class CartService {
    constructor() {}

    get(): Promise<AxiosResponse<CartItem[]>> {
        return api.get<CartItem[]>('/cart')
    }

    create(payload: {productId: number, stockId: number, qty: number}): Promise<AxiosResponse<Cart>> {
        return api.post<Cart>(`/cart/${payload.productId}`, payload);
    }

    update(payload: {productId: number, stockId: number, qty: number}): Promise<AxiosResponse<Cart>> {
        return api.put<Cart>(`/cart/${payload.productId}`, payload);
    }

    remove(stockId: number, qty: number): Promise<AxiosResponse<void>> {
        return api.delete(`/cart/${stockId}/${qty}`);
    }
}