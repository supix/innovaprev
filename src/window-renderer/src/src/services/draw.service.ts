import { CanvasRenderingContext2D, createCanvas } from 'canvas';
import { WindowInput } from '../interfaces/windows/window-input.interface';
import { WindowMaterialType } from '../interfaces/windows/windows.type';

type DrawFn = (ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) => void;

export class DrawService {
    private readonly scale = 0.1;

    private materialRenderMap: Record<WindowMaterialType, DrawFn> = {
        F1A: this.drawSingleOpeningWithVasistas,
        F2A: this.drawDoubleLeafWithVasistas,
        FIX: this.drawFixed,
        FLD: this.drawDoubleWithLeftOpening,
        FLS: this.drawDoubleWithRightOpening,
        PF1A: this.drawSingleOpening,
        PF2A: this.drawDoubleLeaf,
        PRT1A: this.drawSingleOpening,
        PRT2A: this.drawDoubleLeaf,
        SIL: this.drawSliding,
        SRAF: this.drawSlidingWithFixedVasistas,
        SRLA: this.drawSlidingWithRightOpening,
        SLA: this.drawSopraluceWithDoubleLeafVasistas,
        SLF: this.drawSopraluceWithDoubleLeafFixed,
        VASC: this.drawVasistasOnly,
        VASM: this.drawVasistasOnly,
        VAST: this.drawVasistasOnly,
    };

    public drawWindow(input: WindowInput): Buffer {
        const { width, height, materialType, openingType } = input;

        if (width <= 0 || height <= 0) {
            throw new Error(`Invalid dimensions: width=${width}, height=${height}`);
        }

        // Dimensioni telaio "fuori" dal vetro
        const borderSize = 0.08 * width * this.scale; // deve essere uguale a outerFrame!
        const canvasWidth = width * this.scale + 2 * borderSize;
        const canvasHeight = height * this.scale + 2 * borderSize;

        const canvas = createCanvas(canvasWidth, canvasHeight);
        const ctx = canvas.getContext('2d');

        // Sposta tutti i disegni verso il centro, lasciando spazio alla cornice fuori
        ctx.translate(borderSize, borderSize);

        // Vetro (sfondo)
        ctx.fillStyle = input.glassType === 'GT_OPACO' ? '#ccc' : '#cfefff';
        ctx.fillRect(0, 0, width * this.scale, height * this.scale);

        // Telaio tecnico
        this.drawTechnicalFrame(ctx, width * this.scale, height * this.scale);

        // Maniglie
        if (['F1A', 'PF1A', 'PRT1A'].includes(materialType)) {
            this.drawHandle(ctx, width * this.scale, height * this.scale, openingType === 'OT_SX' ? 'right' : 'left');
        }
        if (['F2A', 'PF2A', 'PRT2A'].includes(materialType)) {
            this.drawHandle(ctx, width * this.scale, height * this.scale, 'center');
        }

        // Wire cover
        const wireCoverPadding = borderSize - 5;
        if (input.wireCover) {
            ctx.save();
            ctx.strokeStyle = '#444';
            ctx.lineWidth = 1.2;
            ctx.strokeRect(
              -wireCoverPadding,
              -wireCoverPadding,
              width * this.scale + 2 * wireCoverPadding,
              height * this.scale + 2 * wireCoverPadding
            );
            ctx.restore();
        }

        // Tratteggi/diagrammi di apertura
        const renderer = this.materialRenderMap[materialType];
        if (renderer) {
            renderer.call(this, ctx, width * this.scale, height * this.scale, input);
        } else {
            this.drawUnknown(ctx, width * this.scale, height * this.scale, input);
        }

        return canvas.toBuffer('image/png');
    }

    private drawTechnicalFrame(ctx: CanvasRenderingContext2D, w: number, h: number) {
        // Spessore cornice rispetto all’area utile
        const outerFrame = 0.08 * w; // cornice esterna (8% larghezza)
        const innerFrame = 0.035 * w; // cornice interna (3.5%)

        ctx.save();

        // --- Cornice esterna (fuori dall'area utile) ---
        ctx.strokeStyle = '#aaa';
        ctx.lineWidth = 2;
        ctx.strokeRect(-outerFrame, -outerFrame, w + 2 * outerFrame, h + 2 * outerFrame);

        // --- Cornice interna (fuori dall'area utile) ---
        ctx.strokeStyle = '#888';
        ctx.lineWidth = 2;
        ctx.strokeRect(-outerFrame + innerFrame, -outerFrame + innerFrame, w + 2 * (outerFrame - innerFrame), h + 2 * (outerFrame - innerFrame));

        // --- Cornice vetro (finestra vera e propria, dentro all’area utile) ---
        ctx.strokeStyle = '#bbb';
        ctx.lineWidth = 1;
        ctx.strokeRect(0, 0, w, h);

        // --- Effetto smusso angoli (fuori area utile) ---
        ctx.strokeStyle = '#888';
        ctx.lineWidth = 1;
        const bevel = 0.025 * w;
        // Alto sx
        ctx.beginPath();
        ctx.moveTo(-outerFrame, -outerFrame + bevel);
        ctx.lineTo(-outerFrame + bevel, -outerFrame);
        ctx.stroke();
        // Alto dx
        ctx.beginPath();
        ctx.moveTo(w + outerFrame - bevel, -outerFrame);
        ctx.lineTo(w + outerFrame, -outerFrame + bevel);
        ctx.stroke();
        // Basso dx
        ctx.beginPath();
        ctx.moveTo(w + outerFrame, h + outerFrame - bevel);
        ctx.lineTo(w + outerFrame - bevel, h + outerFrame);
        ctx.stroke();
        // Basso sx
        ctx.beginPath();
        ctx.moveTo(-outerFrame, h + outerFrame - bevel);
        ctx.lineTo(-outerFrame + bevel, h + outerFrame);
        ctx.stroke();

        ctx.restore();
    }

    private drawFixed(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // Solo telaio/vetro
    }

    private drawDoubleLeafWithVasistas(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // X centrale su entrambe le ante
        this.drawDoubleLeaf(ctx, w, h, input);
        // Triangolo vasistas SOLO a sinistra
        this.drawVasistasOnHalf(ctx, w, h, 'left');
    }

    private drawHandle(
      ctx: CanvasRenderingContext2D,
      w: number,
      h: number,
      side: 'left' | 'right' | 'center'
    ) {
        const handleWidth = 0.07 * w;
        const handleHeight = 0.16 * h;
        const y = h / 2 - handleHeight / 2;

        let x: number;
        if (side === 'left') {
            x = 0.04 * w;
        } else if (side === 'right') {
            x = w - handleWidth - 0.04 * w;
        } else {
            // center: esattamente sulla linea centrale, leggermente decentrato verso sinistra
            x = w / 2 - handleWidth / 2;
        }

        ctx.save();
        ctx.fillStyle = '#222';
        ctx.fillRect(x, y, handleWidth, handleHeight);
        ctx.restore();
    }


    private drawUnknown(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // Solo telaio/vetro
    }

    private drawSingleOpening(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        ctx.save();
        ctx.setLineDash([5, 3]);
        ctx.strokeStyle = '#b89617';
        ctx.lineWidth = 2;
        if (input.openingType === 'OT_DX') {
            ctx.beginPath();
            ctx.moveTo(w, h); ctx.lineTo(0, h/2);
            ctx.moveTo(w, 0); ctx.lineTo(0, h/2);
            ctx.stroke();
        } else {
            ctx.beginPath();
            ctx.moveTo(0, h); ctx.lineTo(w, h/2);
            ctx.moveTo(0, 0); ctx.lineTo(w, h/2);
            ctx.stroke();
        }
        ctx.restore();
    }

    private drawDoubleLeaf(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        const halfW = w / 2;
        ctx.save();

        // Divisione centrale tra le due ante
        ctx.setLineDash([]);
        ctx.strokeStyle = '#888';
        ctx.lineWidth = 2;
        ctx.beginPath();
        ctx.moveTo(halfW, 0);
        ctx.lineTo(halfW, h);
        ctx.stroke();

        // Diagonale sinistra (tratteggiata)
        ctx.setLineDash([5, 3]);
        ctx.strokeStyle = '#b89617';
        ctx.lineWidth = 2;
        ctx.beginPath();
        ctx.moveTo(0, h);         // angolo basso sinistro
        ctx.lineTo(halfW, h/2);   // centro tra ante (verticale)
        ctx.moveTo(0, 0);         // angolo alto sinistro
        ctx.lineTo(halfW, h/2);
        ctx.stroke();

        // Diagonale destra (tratteggiata)
        ctx.beginPath();
        ctx.moveTo(w, h);         // angolo basso destro
        ctx.lineTo(halfW, h/2);   // centro tra ante
        ctx.moveTo(w, 0);         // angolo alto destro
        ctx.lineTo(halfW, h/2);
        ctx.stroke();

        ctx.restore();
    }

    private drawSingleOpeningWithVasistas(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        this.drawSingleOpening(ctx, w, h, input);
        this.drawVasistasOnly(ctx, w, h, input);
    }

    private drawVasistasOnHalf(ctx: CanvasRenderingContext2D, w: number, h: number, side: 'left'|'right') {
        ctx.save();
        ctx.setLineDash([5, 3]);
        ctx.strokeStyle = '#b89617';
        ctx.lineWidth = 2;

        // Coordinate della metà selezionata
        const halfW = w / 2;
        const x0 = side === 'left' ? 0 : halfW;
        const xCenter = x0 + halfW / 2;
        const yTop = 0;
        const xBaseLeft = x0;
        const xBaseRight = x0 + halfW;
        const yBase = h;

        ctx.beginPath();
        // Da base sinistra a punta alta
        ctx.moveTo(xBaseLeft, yBase);
        ctx.lineTo(xCenter, yTop);
        // Da punta alta a base destra
        ctx.lineTo(xBaseRight, yBase);
        ctx.stroke();

        ctx.restore();
    }

    private drawDoubleWithLeftOpening(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        const wHalf = w / 2;
        ctx.save();

        // Divisione centrale tra le due ante
        ctx.setLineDash([]);
        ctx.strokeStyle = '#888';
        ctx.lineWidth = 2;
        ctx.beginPath();
        ctx.moveTo(wHalf, 0);
        ctx.lineTo(wHalf, h);
        ctx.stroke();

        // Sinistra: diagonali tratteggiate
        ctx.setLineDash([5, 3]);
        ctx.strokeStyle = '#b89617';
        ctx.lineWidth = 2;
        ctx.beginPath();
        if (input.openingType === 'OT_DX') {
            ctx.moveTo(wHalf, h); ctx.lineTo(0, h / 2);
            ctx.moveTo(wHalf, 0); ctx.lineTo(0, h / 2);
        } else {
            ctx.moveTo(0, h); ctx.lineTo(wHalf, h / 2);
            ctx.moveTo(0, 0); ctx.lineTo(wHalf, h / 2);
        }
        ctx.stroke();

        ctx.restore();
        // Destra: nessuna diagonale (fisso)
    }

    private drawDoubleWithRightOpening(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        const wHalf = w / 2;
        ctx.save();

        // Divisione centrale tra le due ante
        ctx.setLineDash([]);
        ctx.strokeStyle = '#888';
        ctx.lineWidth = 2;
        ctx.beginPath();
        ctx.moveTo(wHalf, 0);
        ctx.lineTo(wHalf, h);
        ctx.stroke();

        // Destra: diagonali tratteggiate
        ctx.setLineDash([5, 3]);
        ctx.strokeStyle = '#b89617';
        ctx.lineWidth = 2;
        ctx.beginPath();
        if (input.openingType === 'OT_DX') {
            ctx.moveTo(w, h); ctx.lineTo(wHalf, h / 2);
            ctx.moveTo(w, 0); ctx.lineTo(wHalf, h / 2);
        } else {
            ctx.moveTo(wHalf, h); ctx.lineTo(w, h / 2);
            ctx.moveTo(wHalf, 0); ctx.lineTo(w, h / 2);
        }
        ctx.stroke();

        ctx.restore();
        // Sinistra: nessuna diagonale (fisso)
    }

    private drawSliding(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // currently unused: input.wireCover, input.glassType, input.openingType
        void input?.wireCover;
        void input?.glassType;
        void input?.openingType;

        // Linea centrale
        ctx.save();
        ctx.strokeStyle = '#888';
        ctx.lineWidth = 2;
        ctx.beginPath();
        ctx.moveTo(w / 2, 0);
        ctx.lineTo(w / 2, h);
        ctx.stroke();
        ctx.restore();
        // Freccia a destra (<-) e a sinistra (->)
        this.drawArrow(ctx, w*0.25, h*0.5, 'right', w, h);
        this.drawArrow(ctx, w*0.75, h*0.5, 'left', w, h);
    }

    private drawArrow(ctx: CanvasRenderingContext2D, x: number, y: number, direction: 'left' | 'right', w: number, h: number) {
        ctx.save();
        ctx.strokeStyle = '#3dc784';
        ctx.lineWidth = 2;
        const len = Math.min(w, h) * 0.14;
        const head = len * 0.4;
        ctx.beginPath();
        if (direction === 'right') {
            ctx.moveTo(x - len / 2, y);
            ctx.lineTo(x + len / 2, y);
            ctx.moveTo(x + len / 2 - head, y - head);
            ctx.lineTo(x + len / 2, y);
            ctx.lineTo(x + len / 2 - head, y + head);
        } else {
            ctx.moveTo(x + len / 2, y);
            ctx.lineTo(x - len / 2, y);
            ctx.moveTo(x - len / 2 + head, y - head);
            ctx.lineTo(x - len / 2, y);
            ctx.lineTo(x - len / 2 + head, y + head);
        }
        ctx.stroke();
        ctx.restore();
    }

    private drawSlidingWithFixedVasistas(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // Divisione centrale
        ctx.save();
        ctx.strokeStyle = '#888';
        ctx.lineWidth = 2;
        ctx.beginPath();
        ctx.moveTo(w / 2, 0);
        ctx.lineTo(w / 2, h);
        ctx.stroke();
        ctx.restore();
        // Freccia solo su sinistra
        this.drawArrow(ctx, w*0.25, h*0.5, 'right', w/2, h);
        // Vasistas solo su destra
        this.drawVasistasOnHalf(ctx, w, h, 'right');
    }

    private drawSlidingWithRightOpening(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // Divisione centrale
        ctx.save();
        ctx.strokeStyle = '#888';
        ctx.lineWidth = 2;
        ctx.beginPath();
        ctx.moveTo(w / 2, 0);
        ctx.lineTo(w / 2, h);
        ctx.stroke();
        ctx.restore();
        // Freccia solo su sinistra
        this.drawArrow(ctx, w*0.25, h*0.5, 'right', w/2, h);
        // 2 diagonali tratteggiate su destra
        const wHalf = w / 2;
        ctx.save();
        ctx.setLineDash([5, 3]);
        ctx.strokeStyle = '#b89617';
        ctx.lineWidth = 2;
        if (input.openingType === 'OT_DX') {
            ctx.beginPath();
            ctx.moveTo(w, h); ctx.lineTo(wHalf, h/2);
            ctx.moveTo(w, 0); ctx.lineTo(wHalf, h/2);
            ctx.stroke();
        } else {
            ctx.beginPath();
            ctx.moveTo(wHalf, h); ctx.lineTo(w, h/2);
            ctx.moveTo(wHalf, 0); ctx.lineTo(w, h/2);
            ctx.stroke();
        }
        ctx.restore();
    }

    private drawVasistasOnly(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        
        ctx.save();
        ctx.setLineDash([5, 3]);
        ctx.strokeStyle = '#b89617';
        ctx.lineWidth = 2;
        ctx.beginPath();
        ctx.moveTo(0, h);      // base sinistra
        ctx.lineTo(w / 2, 0);  // punta in alto al centro
        ctx.lineTo(w, h);      // base destra
        ctx.stroke();
        ctx.restore();
    }

    private drawSopraluceWithDoubleLeafVasistas(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // Calcolo proporzioni (30% sopra, 70% sotto)
        const hSup = h * 0.3;
        const hInf = h - hSup;

        // Sopraluce (rettangolo sopra)
        ctx.save();
        ctx.strokeStyle = '#888';
        ctx.lineWidth = 2;
        ctx.strokeRect(0, 0, w, hSup);

        // Triangolo vasistas nel sopraluce
        ctx.setLineDash([5, 3]);
        ctx.strokeStyle = '#b89617';
        ctx.lineWidth = 2;
        ctx.beginPath();
        ctx.moveTo(0, hSup);        // angolo basso sx della fascia sopra
        ctx.lineTo(w / 2, 0);       // punta al centro in alto
        ctx.lineTo(w, hSup);        // angolo basso dx della fascia sopra
        ctx.stroke();

        ctx.restore();

        // Due ante sotto (stesso schema F2A, ma Y spostata)
        ctx.save();
        ctx.translate(0, hSup);
        this.drawDoubleLeaf(ctx, w, hInf, input);
        ctx.restore();
    }

    private drawSopraluceWithDoubleLeafFixed(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // Calcolo proporzioni (30% sopra, 70% sotto)
        const hSup = h * 0.3;
        const hInf = h - hSup;

        // Sopraluce (rettangolo sopra)
        ctx.save();
        ctx.strokeStyle = '#888';
        ctx.lineWidth = 2;
        ctx.strokeRect(0, 0, w, hSup);
        ctx.restore();

        // Due ante sotto (stesso schema F2A, ma Y spostata)
        ctx.save();
        ctx.translate(0, hSup);
        this.drawDoubleLeaf(ctx, w, hInf, input);
        ctx.restore();
    }

}
