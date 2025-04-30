import { CanvasRenderingContext2D, createCanvas } from 'canvas';
import { WindowInput } from '../interfaces/windows/window-input.interface';

export class DrawService {
    public drawWindow(input: WindowInput): Buffer {
        const { width, height, type } = input;

        const scale = 0.1; // 1mm = 0.1px (es. 1000mm = 100px)
        const canvasWidth = width * scale;
        const canvasHeight = height * scale;

        const canvas = createCanvas(canvasWidth, canvasHeight);
        const ctx = canvas.getContext('2d');

        // Background
        ctx.fillStyle = '#f0f0f0';
        ctx.fillRect(0, 0, canvasWidth, canvasHeight);

        // Border
        ctx.strokeStyle = '#000';
        ctx.lineWidth = 2;
        ctx.strokeRect(0, 0, canvasWidth, canvasHeight);

        ctx.lineWidth = 1;

        switch (type) {
            case 'double-leaf':
                this.drawDoubleLeaf(ctx, canvasWidth, canvasHeight);
                break;
            case 'double-fixed':
                this.drawDoubleFixed(ctx, canvasWidth, canvasHeight);
                break;
            case 'single-opening':
                this.drawSingleOpening(ctx, canvasWidth, canvasHeight);
                break;
        }

        return canvas.toBuffer('image/png');
    }

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
}