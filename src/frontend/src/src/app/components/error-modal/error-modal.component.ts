import {Component, Input} from '@angular/core';
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-error-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './error-modal.component.html',
  styleUrl: './error-modal.component.scss'
})
export class ErrorModalComponent {
  @Input() message!: string;

  close(): void {
    const modal = document.querySelector('app-error-modal');
    if (modal) {
      modal.remove();
    }
  }

}
