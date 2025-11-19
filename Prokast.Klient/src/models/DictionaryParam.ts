import { DictionaryRegion } from "./DictionaryRegion";

export interface DictionaryParam {
  id: number;
  name: string;
  type: string;
  value: string;
  optionID: number;
  regionID: number;
  regions: DictionaryRegion;
}