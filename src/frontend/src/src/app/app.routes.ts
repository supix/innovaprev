import { Routes } from '@angular/router';
import { InnovaFormComponent } from './innova-form/innova-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'form', pathMatch: 'full' },
  { path: 'form', component: InnovaFormComponent },
  { path: '**', redirectTo: 'form', pathMatch: 'full' }
];
