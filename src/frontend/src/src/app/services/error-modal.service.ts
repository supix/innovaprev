import {ApplicationRef, ComponentRef, createComponent, Injectable} from '@angular/core';
import {ErrorModalComponent} from "../components/error-modal/error-modal.component";

@Injectable({providedIn: 'root'})
export class ErrorModalService {

  constructor(private appRef: ApplicationRef) {
  }

  showErrorModal(message: string, restartMode: boolean = false): void {
    const componentRef: ComponentRef<ErrorModalComponent> = createComponent(ErrorModalComponent, {
      environmentInjector: this.appRef.injector,
    });

    componentRef.instance.message = message;
    componentRef.instance.restartMode = restartMode;
    componentRef.changeDetectorRef.detectChanges();

    const element = componentRef.location.nativeElement;
    document.body.appendChild(element);

    componentRef.instance.close = () => {
      document.body.removeChild(element);
      componentRef.destroy();
    };
  }
}
