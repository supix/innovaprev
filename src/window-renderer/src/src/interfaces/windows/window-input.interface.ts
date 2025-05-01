import { WindowMaterialType } from './windows.type';

export interface WindowInput {
  height: number;                // altezza in mm
  width: number;                 // larghezza in mm
  materialType: WindowMaterialType;
  wireCover?: boolean;           // presenza coprifilo (opzionale)
  openingType?: 'OT_DX' | 'OT_SX'; // direzione apertura vetro (destra/sinistra)
  glassType?: 'GT_TRASPARENTE' | 'GT_OPACO'; // tipo vetro
}
