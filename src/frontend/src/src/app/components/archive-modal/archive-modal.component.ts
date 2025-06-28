import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SavedQuote, SavedQuotesService } from '../../services/saved-quotes.service';

@Component({
  selector: 'app-archive-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './archive-modal.component.html',
  styleUrl: './archive-modal.component.scss'
})
export class ArchiveModalComponent implements OnInit {
  @Input() confirmCallback!: (result: string) => void;

  quotes: SavedQuote[] = [];

  constructor(private savedQuotesService: SavedQuotesService, private cdr: ChangeDetectorRef) {
  }

  ngOnInit(): void {
    this.loadQuotes();
  }

  loadQuotes(): void {
    this.quotes = this.savedQuotesService.list();
    this.cdr.detectChanges();
  }

  close(): void {
    document.body.removeChild(document.getElementById('app-archive-modal')!);
  }

  openItem(item: SavedQuote): void {
    if (this.confirmCallback) {
      this.confirmCallback(item.id);
    } else {
      this.close();
    }
  }

  deleteItem(item: SavedQuote): void {
    if (confirm(`Vuoi eliminare il preventivo di ${item.billingPayload.customerData.companyName}?`)) {
      this.savedQuotesService.delete(item.id);
      if (this.savedQuotesService.count()) {
        this.loadQuotes();
      } else {
        this.close();
      }
    }
  }

  deleteAll(): void {
    if (confirm('Vuoi eliminare tutti i preventivi salvati?')) {
      this.savedQuotesService.clear();
      this.close();
    }
  }
}
