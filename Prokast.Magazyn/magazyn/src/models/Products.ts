export interface storedProduct {
  id: number;
  warehouseID: number;
    productID: number;
    quantity: number;
    minQuantity: number;
    maxQuantity: number;
    productName: string;
}

export interface storageResponse {
  model: storedProduct[];
  id: number;
  clientID: number;
  createdDate: string;
}