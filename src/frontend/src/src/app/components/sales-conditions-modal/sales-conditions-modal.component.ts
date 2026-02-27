import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-sales-conditions-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sales-conditions-modal.component.html',
  styleUrl: './sales-conditions-modal.component.scss',
})
export class SalesConditionsModalComponent {
  @Input() initialValue: string = '';

  value: string = '';
  confirmCallback?: (value: string) => void;

  ngOnInit(): void {
    this.value = this.initialValue || '';
  }

  close(): void {
    document.body.removeChild(document.getElementById('app-sales-conditions-modal')!);
  }

  confirm(): void {
    this.confirmCallback?.(this.value);
  }
}
