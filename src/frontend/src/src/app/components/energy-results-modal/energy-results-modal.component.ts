import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnergyCalculationResult } from '../../models';

@Component({
  selector: 'app-energy-results-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './energy-results-modal.component.html',
  styleUrl: './energy-results-modal.component.scss'
})
export class EnergyResultsModalComponent {
  @Input() result!: EnergyCalculationResult;

  close: () => void = () => {};
}
