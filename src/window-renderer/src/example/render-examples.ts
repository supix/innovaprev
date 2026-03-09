import fs from 'fs';
import path from 'path';
import { DrawService } from '../src/services/draw.service';
import { WindowInput } from '../src/interfaces/windows/window-input.interface';
import { WINDOW_MATERIAL_TYPES, WindowMaterialType } from '../src/interfaces/windows/windows.type';

const outputDir = path.resolve(__dirname, 'output');
const drawService = new DrawService();

const defaultsByType: Record<WindowMaterialType, WindowInput> = {
  F1A: { width: 1200, height: 1400, materialType: 'F1A', openingType: 'OT_DX', glassType: 'GT_TRASPARENTE' },
  F2A: { width: 1600, height: 1400, materialType: 'F2A', openingType: 'OT_DX', glassType: 'GT_TRASPARENTE' },
  FIX: { width: 1200, height: 1400, materialType: 'FIX', glassType: 'GT_TRASPARENTE' },
  FLD: { width: 1600, height: 1400, materialType: 'FLD', openingType: 'OT_DX', glassType: 'GT_TRASPARENTE' },
  FLS: { width: 1600, height: 1400, materialType: 'FLS', openingType: 'OT_DX', glassType: 'GT_TRASPARENTE' },
  PF1A: { width: 1200, height: 2400, materialType: 'PF1A', openingType: 'OT_DX', glassType: 'GT_TRASPARENTE' },
  PF2A: { width: 1800, height: 2400, materialType: 'PF2A', openingType: 'OT_DX', glassType: 'GT_TRASPARENTE' },
  PRT1A: { width: 1100, height: 2300, materialType: 'PRT1A', openingType: 'OT_DX', glassType: 'GT_TRASPARENTE' },
  PRT2A: { width: 1800, height: 2300, materialType: 'PRT2A', openingType: 'OT_DX', glassType: 'GT_TRASPARENTE' },
  SIL: { width: 2000, height: 1400, materialType: 'SIL', glassType: 'GT_TRASPARENTE' },
  SLA: { width: 1600, height: 1800, materialType: 'SLA', openingType: 'OT_DX', glassType: 'GT_TRASPARENTE' },
  SLF: { width: 1600, height: 1800, materialType: 'SLF', glassType: 'GT_TRASPARENTE' },
  SRAF: { width: 2000, height: 1400, materialType: 'SRAF', glassType: 'GT_TRASPARENTE' },
  SRLA: { width: 2000, height: 1400, materialType: 'SRLA', openingType: 'OT_DX', glassType: 'GT_TRASPARENTE' },
  VASC: { width: 1200, height: 700, materialType: 'VASC', glassType: 'GT_TRASPARENTE' },
  VASM: { width: 1200, height: 700, materialType: 'VASM', glassType: 'GT_TRASPARENTE' },
  VAST: { width: 1200, height: 700, materialType: 'VAST', glassType: 'GT_TRASPARENTE' },
};

fs.mkdirSync(outputDir, { recursive: true });

for (const materialType of WINDOW_MATERIAL_TYPES) {
  const input = defaultsByType[materialType];
  const buffer = drawService.drawWindow(input);
  const filename = `${materialType}.png`;
  fs.writeFileSync(path.join(outputDir, filename), buffer);
}

console.log(`Generated ${WINDOW_MATERIAL_TYPES.length} example renders in ${outputDir}`);
