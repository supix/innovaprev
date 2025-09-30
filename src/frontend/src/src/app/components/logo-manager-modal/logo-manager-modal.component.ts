import { Component, ElementRef, ViewChild, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogoStorageService, SavedLogo } from '../../services/logo-storage.service';

@Component({
  selector: 'app-logo-manager-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './logo-manager-modal.component.html',
  styleUrl: './logo-manager-modal.component.scss',
})
export class LogoManagerModalComponent {
  @ViewChild('canvas', { static: false }) canvasRef!: ElementRef<HTMLCanvasElement>;
  @ViewChild('fileInput', { static: false }) fileInputRef!: ElementRef<HTMLInputElement>;

  // Crop state
  image?: HTMLImageElement;
  imageLoaded = false;
  canvasSize = 300; // square canvas
  scale = 1;
  minScale = 1;
  maxScale = 5;
  offsetX = 0;
  offsetY = 0;
  isDragging = false;
  dragStartX = 0;
  dragStartY = 0;

  logos: SavedLogo[] = [];

  constructor(private logoStorage: LogoStorageService, private cdr: ChangeDetectorRef) {
    this.loadLogos();
  }

  ngAfterViewInit() {
    const canvas = this.canvasRef?.nativeElement;
    if (canvas) {
      canvas.width = this.canvasSize;
      canvas.height = this.canvasSize;
      this.draw();
    }
  }

  close(): void {
    document.body.removeChild(document.getElementById('app-logo-manager-modal')!);
  }

  openFileDialog(): void {
    this.fileInputRef?.nativeElement.click();
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files || !input.files[0]) return;
    const file = input.files[0];
    const url = URL.createObjectURL(file);
    const img = new Image();
    img.onload = () => {
      this.image = img;
      this.imageLoaded = true;

      // compute initial scale to cover square
      const canvasSize = this.canvasSize;
      const scaleX = canvasSize / img.width;
      const scaleY = canvasSize / img.height;
      this.scale = Math.max(scaleX, scaleY);
      this.minScale = this.scale; // prevent empty borders
      this.maxScale = this.minScale * 4;

      // center image
      this.offsetX = (canvasSize - img.width * this.scale) / 2;
      this.offsetY = (canvasSize - img.height * this.scale) / 2;

      this.draw();
      URL.revokeObjectURL(url);
      // Ensure template updates in zone-less or detached callback contexts
      this.cdr.detectChanges();
    };
    img.src = url;
  }

  onWheel(event: WheelEvent): void {
    if (!this.image) return;
    event.preventDefault();
    const delta = -event.deltaY;
    const zoomFactor = 1 + (delta > 0 ? 0.05 : -0.05);

    const rect = this.canvasRef.nativeElement.getBoundingClientRect();
    const mouseX = event.clientX - rect.left;
    const mouseY = event.clientY - rect.top;

    this.zoomAt(mouseX, mouseY, zoomFactor);
  }

  zoomAt(cx: number, cy: number, zoomFactor: number) {
    const prevScale = this.scale;
    let nextScale = prevScale * zoomFactor;
    nextScale = Math.min(Math.max(nextScale, this.minScale), this.maxScale);
    if (!this.image || nextScale === prevScale) return;

    // adjust offset so the zoom focuses at cursor
    const imgX = (cx - this.offsetX) / prevScale;
    const imgY = (cy - this.offsetY) / prevScale;

    this.offsetX = cx - imgX * nextScale;
    this.offsetY = cy - imgY * nextScale;

    this.scale = nextScale;
    this.draw();
  }

  onMouseDown(event: MouseEvent): void {
    if (!this.image) return;
    this.isDragging = true;
    this.dragStartX = event.clientX;
    this.dragStartY = event.clientY;
  }

  onMouseMove(event: MouseEvent): void {
    if (!this.image || !this.isDragging) return;
    const dx = event.clientX - this.dragStartX;
    const dy = event.clientY - this.dragStartY;
    this.dragStartX = event.clientX;
    this.dragStartY = event.clientY;
    this.offsetX += dx;
    this.offsetY += dy;
    this.draw();
  }

  onMouseUp(): void {
    this.isDragging = false;
  }

  onMouseLeave(): void {
    this.isDragging = false;
  }

  onScaleInput(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    const desired = Number(value);
    if (!this.image) return;

    const center = this.canvasSize / 2;
    const zoomFactor = desired / this.scale;
    this.zoomAt(center, center, zoomFactor);
  }

  private draw(): void {
    const canvas = this.canvasRef?.nativeElement;
    const ctx = canvas?.getContext('2d');
    if (!canvas || !ctx) return;

    // Clear background
    ctx.fillStyle = '#f3f3f3';
    ctx.fillRect(0, 0, canvas.width, canvas.height);

    if (this.image) {
      ctx.drawImage(
        this.image,
        this.offsetX,
        this.offsetY,
        this.image.width * this.scale,
        this.image.height * this.scale
      );

      // border
      ctx.strokeStyle = '#888';
      ctx.lineWidth = 1;
      ctx.strokeRect(0, 0, canvas.width, canvas.height);
    } else {
      // hint text
      ctx.fillStyle = '#666';
      ctx.font = '14px sans-serif';
      ctx.textAlign = 'center';
      ctx.fillText('Carica un logo per ritagliare', canvas.width / 2, canvas.height / 2);
    }
  }

  save(): void {
    if (!this.canvasRef) return;
    const dataUrl = this.canvasRef.nativeElement.toDataURL('image/png');
    this.logoStorage.upsert(null, dataUrl, 'Logo');
    this.loadLogos();
    // Trigger view update after saving a logo
    this.cdr.detectChanges();
    alert('Logo salvato!');
  }

  setActive(item: SavedLogo): void {
    this.logoStorage.setActive(item.id);
    this.loadLogos();
    this.cdr.detectChanges();
  }

  loadLogos(): void {
    // Only refresh the in-memory list here. Avoid triggering change detection during
    // component creation to prevent Angular assertion errors.
    this.logos = this.logoStorage.list();
  }

  deleteLogo(item: SavedLogo): void {
    if (confirm('Eliminare questo logo?')) {
      this.logoStorage.delete(item.id);
      if (this.logoStorage.count()) {
        this.loadLogos();
        // Trigger view update after deletion when the modal stays open
        this.cdr.detectChanges();
      } else {
        this.close();
      }
    }
  }
}
