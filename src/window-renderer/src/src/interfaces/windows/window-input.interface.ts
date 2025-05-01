import { WindowMaterialType } from './windows.type';

export interface WindowInput {
  height: number; // mm
  width: number;  // mm
  materialType: WindowMaterialType;
}