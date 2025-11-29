export interface StoredProduct {
    id: number;
    productId: number;
    warehouseId: number;
    quantity: number;
    minQuantity: number;
    lastUpdated: string;
    productName: string;
    sku: string;
}