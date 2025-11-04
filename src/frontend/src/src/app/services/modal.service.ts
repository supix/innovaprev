import { ApplicationRef, ComponentRef, createComponent, Injectable } from '@angular/core';
import { ErrorModalComponent } from "../components/error-modal/error-modal.component";
import { PreviewModalComponent } from '../components/preview-modal/preview-modal.component';
import { ArchiveModalComponent } from '../components/archive-modal/archive-modal.component';
import { LogoManagerModalComponent } from '../components/logo-manager-modal/logo-manager-modal.component';

@Injectable({providedIn: 'root'})
export class ModalService {

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

  showPreviewModal(title: string, image: Blob): void {
    const componentRef: ComponentRef<PreviewModalComponent> = createComponent(PreviewModalComponent, {
      environmentInjector: this.appRef.injector,
    });

    componentRef.instance.imageBlob = image;
    componentRef.instance.title = title;
    componentRef.changeDetectorRef.detectChanges();

    const element = componentRef.location.nativeElement;
    document.body.appendChild(element);

    componentRef.instance.close = () => {
      document.body.removeChild(element);
      componentRef.destroy();
    };
  }

  showArchiveModal(): Promise<string | null> {
    return new Promise<string | null>((resolve) => {
      const componentRef: ComponentRef<ArchiveModalComponent> = createComponent(ArchiveModalComponent, {
        environmentInjector: this.appRef.injector,
      });

      componentRef.changeDetectorRef.detectChanges();

      const element = componentRef.location.nativeElement;
      document.body.appendChild(element);

      componentRef.instance.confirmCallback = (id: string) => {
        document.body.removeChild(element);
        componentRef.destroy();
        resolve(id);
      };

      componentRef.instance.close = () => {
        document.body.removeChild(element);
        componentRef.destroy();
        resolve(null);
      };
    });
  }

  showLogoManagerModal(): void {
    const componentRef: ComponentRef<LogoManagerModalComponent> = createComponent(LogoManagerModalComponent, {
      environmentInjector: this.appRef.injector,
    });

    componentRef.changeDetectorRef.detectChanges();

    const element = componentRef.location.nativeElement;
    document.body.appendChild(element);

    componentRef.instance.close = () => {
      document.body.removeChild(element);
      componentRef.destroy();
    };
  }

}
