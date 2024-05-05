import { NewStock, Stock } from "../composables/useStock";
import { Product } from "../composables/useProduct";
import { AxiosResponse } from "axios";
import api from "./api.config";

export class StockService {
    constructor() {}

    search(): Promise<AxiosResponse<Array<Product>>> {
        return api.get<Array<Product>>('/stocks');
    }

    create(payload: NewStock): Promise<AxiosResponse<Stock>> {
        return api.post<Stock>('/stocks', payload);
    }

    update(payload: { stock: Stock[] }): Promise<AxiosResponse<{ stock: Stock[] }>> {
        return api.put<{ stock: Stock[] }>('/stocks', payload);
    }

    delete(id: number): Promise<AxiosResponse<boolean>> {
        return api.delete<boolean>(`/stocks/${id}`);
    }
}