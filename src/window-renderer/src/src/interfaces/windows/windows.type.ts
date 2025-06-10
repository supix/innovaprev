export const WINDOW_MATERIAL_TYPES = [
  'F1A',     // Finestra 1 Anta
  'F2A',     // Finestra 2 Ante
  'FIX',     // Fisso con fermavetro
  'FLD',     // Fisso laterale dx
  'FLS',     // Fisso laterale sx
  'PF1A',    // Portafinestra 1 Anta
  'PF2A',    // Portafinestra 2 Ante
  'PRT1A',   // Portoncino 1 Anta
  'PRT2A',   // Portoncino 2 Ante
  'SIL',     // Scorrevole in linea
  'SLA',     // Sopraluce apribile
  'SLF',     // Sopraluce fisso
  'SRAF',    // Scorrevole ribalta con anta fissa
  'SRLA',    // Scorrevole ribalta con laterale apribile
  'VASC',    // Vasistas (apertura a cricchetto)
  'VASM',    // Vasistas (apertura a motore)
  'VAST'     // Vasistas (apertura a martellina)
] as const;

export type WindowMaterialType = typeof WINDOW_MATERIAL_TYPES[number];