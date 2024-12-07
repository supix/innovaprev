import { Routes } from '@angular/router';
import { PrevFormComponent } from './prev-form/prev-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'form', pathMatch: 'full' },
  { path: 'form', component: PrevFormComponent },
  { path: '**', redirectTo: 'form', pathMatch: 'full' }
];
