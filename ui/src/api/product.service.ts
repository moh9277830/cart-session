import { Product, NewProduct } from "../composables/useProduct";
import { AxiosResponse } from "axios";
import api from "./api.config";

export class ProductService {
    constructor() {}

    search(): Promise<AxiosResponse<Array<Product>>> {
        return api.get<Array<Product>>('/products');
    }

    get(id: number): Promise<AxiosResponse<Product>> {
        return api.get(`/products/${id}`)
    }

    create(payload: NewProduct): Promise<AxiosResponse<Product>> {
        return api.post<Product>('/products', payload);
    }

    update(payload: Product): Promise<AxiosResponse<Product>> {
        return api.put<Product>('/products',payload);
    }

    delete(id: number): Promise<AxiosResponse<boolean>> {
        return api.delete<boolean>(`/products/${id}`);
    }
}