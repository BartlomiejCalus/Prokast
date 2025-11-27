import { StoredProduct } from "./StoredProduct/StoredProduct";

export interface Warehouse {
    id: number;
    name: string;
    address: string;
    postalCode: string;
    city: string;
    country: string;
    phoneNumber: string;
    storedProducts?: StoredProduct[];
}