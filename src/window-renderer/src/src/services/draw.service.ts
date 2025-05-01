import { CanvasRenderingContext2D, createCanvas } from 'canvas';
import { WindowInput } from '../interfaces/windows/window-input.interface';
import { WindowMaterialType } from '../interfaces/windows/windows.type';

type DrawFn = (ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) => void;

export class DrawService {
    private readonly scale = 0.1;

    public drawWindow(input: WindowInput): Buffer {
        const { width, height, materialType } = input;

        if (width <= 0 || height <= 0) {
            throw new Error(`Invalid dimensions: width=${width}, height=${height}`);
        }

        const canvasWidth = width * this.scale;
        const canvasHeight = height * this.scale;

        const canvas = createCanvas(canvasWidth, canvasHeight);
        const ctx = canvas.getContext('2d');

        // Fill background based on the opening type
        if (input.glassType === 'GT_OPACO') {
            ctx.fillStyle = '#ccc';
        } else {
            ctx.fillStyle = '#eef';
        }
        ctx.fillRect(0, 0, canvasWidth, canvasHeight);

        // Outline frame
        ctx.strokeStyle = '#000';
        ctx.lineWidth = 2;
        ctx.strokeRect(0, 0, canvasWidth, canvasHeight);

        // Wire cover
        if (input.wireCover) {
            ctx.strokeStyle = '#444';
            ctx.strokeRect(5, 5, canvasWidth - 10, canvasHeight - 10);
        }

        // Dispatch by material type
        const renderer = this.materialRenderMap[materialType];
        if (renderer) {
            renderer.call(this, ctx, canvasWidth, canvasHeight, input);
        } else {
            this.drawUnknown(ctx, canvasWidth, canvasHeight, input);
        }

        return canvas.toBuffer('image/png');
    }

    private materialRenderMap: Record<WindowMaterialType, DrawFn> = {
        // Finestra e portafinestra
        F1A: this.drawSingleOpening,
        F2A: this.drawDoubleLeaf,
        PF1A: this.drawSingleOpening,
        PF2A: this.drawDoubleLeaf,

        // Portoncini
        PRT1A: this.drawSingleOpening,
        PRT2A: this.drawDoubleLeaf,

        // Fissi
        FIX: this.drawFixed,
        FLD: this.drawFixed,
        FLS: this.drawFixed,
        SLF: this.drawFixedTop,

        // Scorrevoli
        SIL: this.drawSliding,
        SRAF: this.drawSlidingWithFixed,
        SRLA: this.drawSlidingWithOpening,

        // Sopraluce e altri
        SLA: this.drawTopOpening,
        VAS: this.drawVasistas,
        FRO: this.drawFrontalino,
        CAS: this.drawCassonetto,
    };

    private drawLabelBox(ctx: CanvasRenderingContext2D, w: number, h: number, label: string) {
        ctx.font = 'bold 12px Sans';
        ctx.fillStyle = '#888';
        ctx.fillText(label, w / 2 - ctx.measureText(label).width / 2, h / 2);
        ctx.strokeStyle = '#aaa';
        ctx.strokeRect(0, 0, w, h);
    }

    private drawDoubleLeaf(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // currently unused: input.wireCover, input.glassType, input.openingType
        void input?.wireCover;
        void input?.glassType;
        void input?.openingType;

        const midX = w / 2;
        ctx.beginPath();
        ctx.moveTo(midX, 0);
        ctx.lineTo(midX, h);
        ctx.stroke();

        ctx.beginPath();
        ctx.moveTo(0, 0);
        ctx.lineTo(midX, h);
        ctx.stroke();

        ctx.beginPath();
        ctx.moveTo(midX, 0);
        ctx.lineTo(w, h);
        ctx.stroke();
    }

    private drawSingleOpening(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // currently unused: input.wireCover, input.glassType, input.openingType
        void input?.wireCover;
        void input?.glassType;
        void input?.openingType;

        ctx.beginPath();
        ctx.moveTo(0, 0);
        ctx.lineTo(w, h);
        ctx.stroke();

        ctx.font = 'bold 10px Sans';
        ctx.fillStyle = '#666';
        ctx.fillText('OPEN', w / 2 - 20, h / 2);
    }

    private drawSliding(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // currently unused: input.wireCover, input.glassType, input.openingType
        void input?.wireCover;
        void input?.glassType;
        void input?.openingType;

        const midX = w / 2;
        ctx.strokeStyle = '#00f';
        ctx.beginPath();
        ctx.moveTo(midX, 0);
        ctx.lineTo(midX, h);
        ctx.stroke();

        ctx.beginPath();
        ctx.moveTo(midX - 10, h / 2 - 10);
        ctx.lineTo(midX + 10, h / 2);
        ctx.lineTo(midX - 10, h / 2 + 10);
        ctx.stroke();
    }

    private drawFixed(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // currently unused: input.wireCover, input.glassType, input.openingType
        void input?.wireCover;
        void input?.glassType;
        void input?.openingType;

        this.drawLabelBox(ctx, w, h, 'FIXED');
    }

    private drawCassonetto(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // currently unused: input.wireCover, input.glassType, input.openingType
        void input?.wireCover;
        void input?.glassType;
        void input?.openingType;

        this.drawLabelBox(ctx, w, h, 'CASSONETTO');
    }

    private drawFrontalino(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // currently unused: input.wireCover, input.glassType, input.openingType
        void input?.wireCover;
        void input?.glassType;
        void input?.openingType;

        this.drawLabelBox(ctx, w, h, 'FRONTALINO');
    }

    private drawTopOpening(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // currently unused: input.wireCover, input.glassType, input.openingType
        void input?.wireCover;
        void input?.glassType;
        void input?.openingType;

        this.drawLabelBox(ctx, w, h, 'TOPOPENING');
    }

    private drawFixedTop(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // currently unused: input.wireCover, input.glassType, input.openingType
        void input?.wireCover;
        void input?.glassType;
        void input?.openingType;

        this.drawLabelBox(ctx, w, h, 'FIXEDTOP');
    }

    private drawSlidingWithFixed(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // currently unused: input.wireCover, input.glassType, input.openingType
        void input?.wireCover;
        void input?.glassType;
        void input?.openingType;

        this.drawLabelBox(ctx, w, h, 'SLIDING+FIXED');
    }

    private drawSlidingWithOpening(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // currently unused: input.wireCover, input.glassType, input.openingType
        void input?.wireCover;
        void input?.glassType;
        void input?.openingType;

        this.drawLabelBox(ctx, w, h, 'SLIDING+OPENING');
    }

    private drawVasistas(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // currently unused: input.wireCover, input.glassType, input.openingType
        void input?.wireCover;
        void input?.glassType;
        void input?.openingType;

        this.drawLabelBox(ctx, w, h, 'VASISTAS');
    }

    private drawUnknown(ctx: CanvasRenderingContext2D, w: number, h: number, input: WindowInput) {
        // currently unused: input.wireCover, input.glassType, input.openingType
        void input?.wireCover;
        void input?.glassType;
        void input?.openingType;

        this.drawLabelBox(ctx, w, h, 'UNKNOWN');
    }
}
