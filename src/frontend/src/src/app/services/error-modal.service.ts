import {Injectable, Injector, ComponentRef, createComponent} from '@angular/core';
import {ErrorModalComponent} from "../components/error-modal/error-modal.component";
import {ApplicationRef} from '@angular/core';

@Injectable({providedIn: 'root'})
export class ErrorModalService {

  constructor(private injector: Injector, private appRef: ApplicationRef) {
  }

  showErrorModal(message: string): void {
    const componentRef: ComponentRef<ErrorModalComponent> = createComponent(ErrorModalComponent, {
      environmentInjector: this.appRef.injector,
    });

    componentRef.instance.message = message;
    componentRef.changeDetectorRef.detectChanges();

    const element = componentRef.location.nativeElement;
    document.body.appendChild(element);

    componentRef.instance.close = () => {
      document.body.removeChild(element);
      componentRef.destroy();
    };
  }
}
