import { CanvasRenderingContext2D, createCanvas } from 'canvas';
import { WindowInput } from '../interfaces/windows/window-input.interface';
import { WindowMaterialType } from '../interfaces/windows/windows.type';

export class DrawService {
    public drawWindow(input: WindowInput): Buffer {
        const { width, height, materialType } = input;
        const scale = 0.1;
        const canvasWidth = width * scale;
        const canvasHeight = height * scale;

        const canvas = createCanvas(canvasWidth, canvasHeight);
        const ctx = canvas.getContext('2d');

        ctx.fillStyle = '#fff';
        ctx.fillRect(0, 0, canvasWidth, canvasHeight);
        ctx.strokeStyle = '#000';
        ctx.lineWidth = 2;
        ctx.strokeRect(0, 0, canvasWidth, canvasHeight);

        // Dispatch by material type
        const renderer = this.materialRenderMap[materialType];
        if (renderer) {
            renderer.call(this, ctx, canvasWidth, canvasHeight);
        } else {
            this.drawUnknown(ctx, canvasWidth, canvasHeight);
        }

        return canvas.toBuffer('image/png');
    }

    private materialRenderMap: Record<WindowMaterialType, (ctx: CanvasRenderingContext2D, w: number, h: number) => void> = {
        CAS: this.drawCassonetto,
        F1A: this.drawSingleOpening,
        F2A: this.drawDoubleLeaf,
        FIX: this.drawFixed,
        FLD: this.drawFixed,
        FLS: this.drawFixed,
        FRO: this.drawFrontalino,
        PF1A: this.drawSingleOpening,
        PF2A: this.drawDoubleLeaf,
        PRT1A: this.drawSingleOpening,
        PRT2A: this.drawDoubleLeaf,
        SIL: this.drawSliding,
        SLA: this.drawTopOpening,
        SLF: this.drawFixedTop,
        SRAF: this.drawSlidingWithFixed,
        SRLA: this.drawSlidingWithOpening,
        VAS: this.drawVasistas,
    };


    private drawDoubleLeaf(ctx: CanvasRenderingContext2D, w: number, h: number) {
        const midX = w / 2;

        // Divide in due ante
        ctx.beginPath();
        ctx.moveTo(midX, 0);
        ctx.lineTo(midX, h);
        ctx.stroke();

        // Diagonali ante sx
        ctx.beginPath();
        ctx.moveTo(0, 0);
        ctx.lineTo(midX, h);
        ctx.stroke();

        // Diagonali ante dx
        ctx.beginPath();
        ctx.moveTo(midX, 0);
        ctx.lineTo(w, h);
        ctx.stroke();
    }

    private drawDoubleFixed(ctx: CanvasRenderingContext2D, w: number, h: number) {
        const midX = w / 2;

        ctx.beginPath();
        ctx.moveTo(midX, 0);
        ctx.lineTo(midX, h);
        ctx.stroke();

        ctx.font = 'bold 10px Sans';
        ctx.fillStyle = '#666';
        ctx.fillText('FIXED', midX / 2 - 15, h / 2);
        ctx.fillText('FIXED', (3 * midX) / 2 - 15, h / 2);
    }

    private drawSingleOpening(ctx: CanvasRenderingContext2D, w: number, h: number) {
        ctx.beginPath();
        ctx.moveTo(0, 0);
        ctx.lineTo(w, h);
        ctx.stroke();

        ctx.font = 'bold 10px Sans';
        ctx.fillStyle = '#666';
        ctx.fillText('OPEN', w / 2 - 20, h / 2);
    }

    private drawSliding(ctx: CanvasRenderingContext2D, w: number, h: number) {
        const midX = w / 2;
        ctx.strokeStyle = '#00f';

        // Linea centrale (binario)
        ctx.beginPath();
        ctx.moveTo(midX, 0);
        ctx.lineTo(midX, h);
        ctx.stroke();

        // Frecce di scorrimento
        ctx.beginPath();
        ctx.moveTo(midX - 10, h / 2 - 10);
        ctx.lineTo(midX + 10, h / 2);
        ctx.lineTo(midX - 10, h / 2 + 10);
        ctx.stroke();
    }

    private drawFixed(ctx: CanvasRenderingContext2D, w: number, h: number) {
        ctx.font = 'bold 12px Sans';
        ctx.fillStyle = '#888';
        ctx.fillText('FIXED', w / 2 - 25, h / 2);
    }

    private drawCassonetto(ctx: CanvasRenderingContext2D, w: number, h: number) {
        ctx.fillStyle = '#ddd';
        ctx.fillRect(0, 0, w, h);
        ctx.strokeRect(0, 0, w, h);
        ctx.font = 'bold 12px Sans';
        ctx.fillStyle = '#333';
        ctx.fillText('CASSONETTO', w / 2 - 40, h / 2);
    }

    private drawFrontalino(ctx: CanvasRenderingContext2D, w: number, h: number) {
        // TODO: implement rendering for FRONTALINO
        ctx.font = 'bold 12px Sans';
        ctx.fillStyle = '#888';
        ctx.fillText('FRONTALINO', w / 2 - 40, h / 2);
        ctx.strokeStyle = '#aaa';
        ctx.strokeRect(0, 0, w, h);
    }

    private drawTopOpening(ctx: CanvasRenderingContext2D, w: number, h: number) {
        // TODO: implement rendering for TOPOPENING
        ctx.font = 'bold 12px Sans';
        ctx.fillStyle = '#888';
        ctx.fillText('TOPOPENING', w / 2 - 40, h / 2);
        ctx.strokeStyle = '#aaa';
        ctx.strokeRect(0, 0, w, h);
    }

    private drawFixedTop(ctx: CanvasRenderingContext2D, w: number, h: number) {
        // TODO: implement rendering for FIXEDTOP
        ctx.font = 'bold 12px Sans';
        ctx.fillStyle = '#888';
        ctx.fillText('FIXEDTOP', w / 2 - 40, h / 2);
        ctx.strokeStyle = '#aaa';
        ctx.strokeRect(0, 0, w, h);
    }

    private drawSlidingWithFixed(ctx: CanvasRenderingContext2D, w: number, h: number) {
        // TODO: implement rendering for SLIDINGWITHFIXED
        ctx.font = 'bold 12px Sans';
        ctx.fillStyle = '#888';
        ctx.fillText('SLIDINGWITHFIXED', w / 2 - 40, h / 2);
        ctx.strokeStyle = '#aaa';
        ctx.strokeRect(0, 0, w, h);
    }

    private drawSlidingWithOpening(ctx: CanvasRenderingContext2D, w: number, h: number) {
        // TODO: implement rendering for SLIDINGWITHOPENING
        ctx.font = 'bold 12px Sans';
        ctx.fillStyle = '#888';
        ctx.fillText('SLIDINGWITHOPENING', w / 2 - 40, h / 2);
        ctx.strokeStyle = '#aaa';
        ctx.strokeRect(0, 0, w, h);
    }

    private drawVasistas(ctx: CanvasRenderingContext2D, w: number, h: number) {
        // TODO: implement rendering for VASISTAS
        ctx.font = 'bold 12px Sans';
        ctx.fillStyle = '#888';
        ctx.fillText('VASISTAS', w / 2 - 40, h / 2);
        ctx.strokeStyle = '#aaa';
        ctx.strokeRect(0, 0, w, h);
    }

    private drawUnknown(ctx: CanvasRenderingContext2D, w: number, h: number) {
        // TODO: implement rendering for UNKNOWN
        ctx.font = 'bold 12px Sans';
        ctx.fillStyle = '#888';
        ctx.fillText('UNKNOWN', w / 2 - 40, h / 2);
        ctx.strokeStyle = '#aaa';
        ctx.strokeRect(0, 0, w, h);
    }



}