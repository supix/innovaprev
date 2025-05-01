import { Component, Input } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-preview-modal',
  imports: [CommonModule],
  templateUrl: './preview-modal.component.html',
  styleUrls: ['./preview-modal.component.scss'],
  standalone: true
})
export class PreviewModalComponent {
  @Input() imageBlob!: Blob;

  constructor(private sanitizer: DomSanitizer) {}

  close(): void {
    document.body.removeChild(document.getElementById('app-preview-modal')!);
  }

  get safeUrl(): SafeUrl {
    return this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(this.imageBlob));
  }
}
