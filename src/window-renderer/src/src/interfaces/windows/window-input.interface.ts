import { WindowMaterialType } from './windows.type';

export interface WindowInput {
  height: number;
  width: number;
  materialType: WindowMaterialType;
  wireCover?: boolean;
  openingType?: 'OT_DX' | 'OT_SX';
  glassType?: 'GT_TRASPARENTE' | 'GT_OPACO';
}

export interface WindowInputBatch extends WindowInput {
  position: string;
}
