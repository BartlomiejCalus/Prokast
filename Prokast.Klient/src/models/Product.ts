import { AdditionalField } from "./AdditionalField";
import { CustomParam } from "./CustomParam";
import { DictionaryParam } from "./DictionaryParam";
import { Photo } from "./Photo";
import { PriceList } from "./PriceList";

export interface ProductModel {
  name: string;
  sku: string;
  ean: string;
  description: string;
  additionalDescriptions: AdditionalField[];
  additionalNames: AdditionalField[];
  dictionaryParams: DictionaryParam[];
  customParams: CustomParam[];
  photos: Photo[];
  priceList: PriceList;
}

export interface ProductResponse {
  model: ProductModel;
  id: number;
  clientID: number;
  createdDate: string;
}