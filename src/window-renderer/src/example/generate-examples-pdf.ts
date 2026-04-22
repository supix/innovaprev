import fs from 'fs';
import path from 'path';
import { CanvasRenderingContext2D, createCanvas, loadImage } from 'canvas';
import { DrawService } from '../src/services/draw.service';
import { WindowInput } from '../src/interfaces/windows/window-input.interface';

type Example = {
  title: string;
  note: string;
  input: WindowInput;
};

type CollectionItemMap = Record<string, string>;

const PAGE_WIDTH = 595;
const PAGE_HEIGHT = 842;
const MARGIN = 40;
const GAP = 22;
const EXAMPLE_HEIGHT = 340;
const IMAGE_SIZE = 180;

const outputDir = path.resolve(__dirname, 'output');
const outputPath = path.join(outputDir, 'window-generation-examples.pdf');
const drawService = new DrawService();

const materialDescriptions: CollectionItemMap = {
  F1A: 'Finestra 1 Anta',
  F2A: 'Finestra 2 Ante',
  FLD: 'Fisso laterale dx',
  FLS: 'Fisso laterale sx',
  PF1A: 'Portafinestra 1 Anta',
  PF2A: 'Portafinestra 2 Ante',
  PRT2A: 'Portoncino 2 ante',
  SIL: 'Scorrevole in linea',
  SRLA: 'Scorrevole ribalta con laterale apribile',
};

const openingTypeDescriptions: CollectionItemMap = {
  OT_DX: 'SX',
  OT_SX: 'DX',
};

const glassTypeDescriptions: CollectionItemMap = {
  GT_TRASPARENTE: 'Trasparente',
  GT_OPACO: 'Opaco',
};

const examples: Example[] = [
  {
    title: 'FLS - fisso laterale sinistro, anta destra apribile',
    note: 'Caso simile allo schema bug: la maniglia e sul montante centrale dell anta destra.',
    input: { width: 1600, height: 1400, materialType: 'FLS', openingType: 'OT_DX', glassType: 'GT_TRASPARENTE' },
  },
  {
    title: 'FLD - fisso laterale destro, anta sinistra apribile',
    note: 'Speculare al caso precedente: il punto maniglia segue il verso di apertura.',
    input: { width: 1600, height: 1400, materialType: 'FLD', openingType: 'OT_SX', glassType: 'GT_TRASPARENTE' },
  },
  {
    title: 'F2A - finestra due ante, anta destra principale',
    note: 'Due ante con apertura a battente e ribalta sull anta principale.',
    input: { width: 1600, height: 1400, materialType: 'F2A', openingType: 'OT_DX', glassType: 'GT_TRASPARENTE' },
  },
  {
    title: 'F2A - finestra due ante, anta sinistra principale',
    note: 'Stessi dati dimensionali, ma verso apertura invertito.',
    input: { width: 1600, height: 1400, materialType: 'F2A', openingType: 'OT_SX', glassType: 'GT_TRASPARENTE' },
  },
  {
    title: 'PF2A - portafinestra due ante',
    note: 'Altezza maggiore: il renderer mantiene il proporzionamento nel canvas 400x400.',
    input: { width: 1800, height: 2400, materialType: 'PF2A', openingType: 'OT_DX', glassType: 'GT_TRASPARENTE' },
  },
  {
    title: 'PF1A - portafinestra una anta',
    note: 'Anta singola con maniglia sul lato indicato dal verso apertura.',
    input: { width: 1000, height: 2400, materialType: 'PF1A', openingType: 'OT_SX', glassType: 'GT_TRASPARENTE' },
  },
  {
    title: 'F1A - finestra una anta con vetro opaco',
    note: 'Il tipo vetro GT_OPACO aggiunge il tratteggio incrociato sul vetro.',
    input: { width: 1200, height: 1400, materialType: 'F1A', openingType: 'OT_DX', glassType: 'GT_OPACO' },
  },
  {
    title: 'PRT2A - portoncino due ante con wire cover',
    note: 'wireCover true aggiunge il riquadro tecnico esterno.',
    input: { width: 1800, height: 2300, materialType: 'PRT2A', openingType: 'OT_SX', glassType: 'GT_TRASPARENTE', wireCover: true },
  },
  {
    title: 'SIL - scorrevole in linea',
    note: 'Gli scorrevoli usano frecce verdi invece dei tratteggi di apertura a battente.',
    input: { width: 2200, height: 1400, materialType: 'SIL', glassType: 'GT_TRASPARENTE' },
  },
  {
    title: 'SRLA - scorrevole ribalta con laterale apribile',
    note: 'Combina freccia di scorrimento e apertura laterale a battente.',
    input: { width: 2200, height: 1500, materialType: 'SRLA', openingType: 'OT_DX', glassType: 'GT_TRASPARENTE' },
  },
];

function drawText(ctx: CanvasRenderingContext2D, text: string, x: number, y: number, maxWidth: number, lineHeight: number): number {
  const words = text.split(/\s+/);
  let line = '';
  let cursorY = y;

  for (const word of words) {
    const testLine = line ? `${line} ${word}` : word;
    if (ctx.measureText(testLine).width > maxWidth && line) {
      ctx.fillText(line, x, cursorY);
      line = word;
      cursorY += lineHeight;
    } else {
      line = testLine;
    }
  }

  if (line) {
    ctx.fillText(line, x, cursorY);
    cursorY += lineHeight;
  }

  return cursorY;
}

function drawInputBlock(ctx: CanvasRenderingContext2D, input: WindowInput, x: number, y: number, maxWidth: number): void {
  const materialDescription = materialDescriptions[input.materialType] ?? input.materialType;
  const openingDescription = input.openingType ? openingTypeDescriptions[input.openingType] ?? input.openingType : '-';
  const glassDescription = input.glassType ? glassTypeDescriptions[input.glassType] ?? input.glassType : '-';
  const rows = [
    ['Tipologia', `${materialDescription} (${input.materialType})`],
    ['Larghezza', `${input.width} mm`],
    ['Altezza', `${input.height} mm`],
    ['Apertura', input.openingType ? `${openingDescription} (${input.openingType})` : '-'],
    ['Vetro', input.glassType ? `${glassDescription} (${input.glassType})` : '-'],
    ['Copriprofilo', input.wireCover ? 'Si' : 'No'],
  ];

  ctx.font = '10px Helvetica';
  ctx.fillStyle = '#111';
  ctx.fillText('Dati di origine:', x, y);

  let cursorY = y + 16;
  for (const [label, value] of rows) {
    ctx.fillStyle = '#555';
    ctx.fillText(label, x, cursorY);
    ctx.fillStyle = '#111';
    const nextValueY = drawText(ctx, value, x + 86, cursorY, maxWidth - 86, 12);
    cursorY = Math.max(cursorY + 14, nextValueY + 2);
  }

  ctx.strokeStyle = '#d0d0d0';
  ctx.lineWidth = 0.6;
  ctx.strokeRect(x - 8, y - 14, maxWidth + 16, cursorY - y + 6);
}

async function drawExample(ctx: CanvasRenderingContext2D, example: Example, index: number, y: number): Promise<void> {
  const imageBuffer = drawService.drawWindow(example.input);
  const image = await loadImage(imageBuffer);
  const imageX = MARGIN;
  const textX = MARGIN + IMAGE_SIZE + 28;
  const textWidth = PAGE_WIDTH - textX - MARGIN;

  ctx.strokeStyle = '#d8d8d8';
  ctx.lineWidth = 0.7;
  ctx.strokeRect(MARGIN, y, PAGE_WIDTH - 2 * MARGIN, EXAMPLE_HEIGHT);

  ctx.font = '12px Helvetica-Bold';
  ctx.fillStyle = '#111';
  ctx.fillText(`${index + 1}. ${example.title}`, textX, y + 30);

  ctx.font = '10px Helvetica';
  ctx.fillStyle = '#333';
  const afterNoteY = drawText(ctx, example.note, textX, y + 50, textWidth, 14);
  drawInputBlock(ctx, example.input, textX, afterNoteY + 18, textWidth);

  ctx.drawImage(image, imageX, y + 64, IMAGE_SIZE, IMAGE_SIZE);

  ctx.font = '9px Helvetica';
  ctx.fillStyle = '#555';
  ctx.fillText('Output PNG prodotto da DrawService.drawWindow(input)', imageX, y + 260);
}

async function main(): Promise<void> {
  fs.mkdirSync(outputDir, { recursive: true });

  const canvas = createCanvas(PAGE_WIDTH, PAGE_HEIGHT, 'pdf');
  const ctx = canvas.getContext('2d');

  for (let pageStart = 0; pageStart < examples.length; pageStart += 2) {
    if (pageStart > 0) {
      ctx.addPage(PAGE_WIDTH, PAGE_HEIGHT);
    }

    ctx.fillStyle = '#fff';
    ctx.fillRect(0, 0, PAGE_WIDTH, PAGE_HEIGHT);

    ctx.font = '18px Helvetica-Bold';
    ctx.fillStyle = '#111';
    ctx.fillText('Esempi generazione serramenti', MARGIN, 34);

    ctx.font = '10px Helvetica';
    ctx.fillStyle = '#555';
    ctx.fillText('Ogni esempio mostra immagine renderizzata e dati input da cui proviene.', MARGIN, 52);

    await drawExample(ctx, examples[pageStart], pageStart, 76);

    if (examples[pageStart + 1]) {
      await drawExample(ctx, examples[pageStart + 1], pageStart + 1, 76 + EXAMPLE_HEIGHT + GAP);
    }

    ctx.font = '9px Helvetica';
    ctx.fillStyle = '#777';
    ctx.fillText(`Pagina ${pageStart / 2 + 1}`, PAGE_WIDTH - MARGIN - 44, PAGE_HEIGHT - 24);
  }

  fs.writeFileSync(outputPath, canvas.toBuffer('application/pdf'));
  console.log(`Generated PDF: ${outputPath}`);
}

main().catch((error) => {
  console.error(error);
  process.exit(1);
});
