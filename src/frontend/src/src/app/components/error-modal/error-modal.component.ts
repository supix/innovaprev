import {ApplicationRef, Component, Input} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AppComponent} from '../../app.component';

@Component({
  selector: 'app-error-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './error-modal.component.html',
  styleUrl: './error-modal.component.scss'
})
export class ErrorModalComponent {
  @Input() message!: string;
  @Input() restartMode: boolean = false;

  constructor(private appRef: ApplicationRef) {}

  close(): void {
    const modal = document.querySelector('app-error-modal');
    if (modal) {
      modal.remove();
    }
  }

  restart(): void {
    this.appRef.bootstrap(AppComponent);
    this.close();
  }

}
