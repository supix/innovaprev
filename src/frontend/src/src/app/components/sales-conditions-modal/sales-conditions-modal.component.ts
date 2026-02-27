import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, Input, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sales-conditions-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sales-conditions-modal.component.html',
  styleUrl: './sales-conditions-modal.component.scss',
})
export class SalesConditionsModalComponent implements AfterViewInit {
  constructor(private readonly cdr: ChangeDetectorRef) {}

  @Input() initialValue: string = '';
  @ViewChild('editor') editor?: ElementRef<HTMLDivElement>;

  value: string = '';
  activeFormats: Record<string, boolean> = {
    bold: false,
    italic: false,
    underline: false,
    insertUnorderedList: false,
    insertOrderedList: false,
  };
  private savedRange: Range | null = null;
  confirmCallback?: (value: string) => void;

  ngAfterViewInit(): void {
    this.value = this.initialValue || '';
    if (this.editor) {
      this.editor.nativeElement.innerHTML = this.value;
    }
  }

  onEditorInteraction(): void {
    this.saveSelection();
  }

  onEnterTooltip(event: MouseEvent, tooltipId: string): void {
    const tooltip = document.getElementById(tooltipId);
    const target = event.currentTarget as HTMLElement;

    if (tooltip && target) {
      const rect = target.getBoundingClientRect();
      tooltip.style.top = `${rect.bottom + 5}px`;
      tooltip.style.left = `${rect.left + rect.width / 2}px`;
      tooltip.style.transform = 'translateX(-50%)';
      tooltip.style.opacity = '1';
    }
  }

  onLeaveTooltip(tooltipId: string): void {
    const tooltip = document.getElementById(tooltipId);

    if (tooltip) {
      tooltip.style.opacity = '0';
    }
  }

  onToolbarAction(event: MouseEvent, command: string): void {
    event.preventDefault();
    this.restoreSelection();
    document.execCommand(command);
    this.saveSelection();
    this.syncValue();

    if (command !== 'removeFormat') {
      this.activeFormats = {
        ...this.activeFormats,
        [command]: !this.activeFormats[command],
      };
    } else {
      this.resetActiveFormats();
    }

    this.cdr.detectChanges();
  }

  syncValue(): void {
    this.value = this.editor?.nativeElement.innerHTML || '';
  }

  private isSelectionInsideEditor(): boolean {
    const selection = window.getSelection();
    const editor = this.editor?.nativeElement;

    if (!selection || !editor || selection.rangeCount === 0) {
      return false;
    }

    const anchorNode = selection.anchorNode;
    return !!anchorNode && editor.contains(anchorNode);
  }

  private saveSelection(): void {
    const selection = window.getSelection();

    if (!selection || selection.rangeCount === 0 || !this.isSelectionInsideEditor()) {
      return;
    }

    this.savedRange = selection.getRangeAt(0).cloneRange();
  }

  private restoreSelection(): void {
    const selection = window.getSelection();

    if (!selection || !this.savedRange) {
      return;
    }

    this.editor?.nativeElement.focus();
    selection.removeAllRanges();
    selection.addRange(this.savedRange);
  }

  private resetActiveFormats(): void {
    this.activeFormats = {
      bold: false,
      italic: false,
      underline: false,
      insertUnorderedList: false,
      insertOrderedList: false,
    };
  }

  close(): void {
    document.body.removeChild(document.getElementById('app-sales-conditions-modal')!);
  }

  confirm(): void {
    this.confirmCallback?.(this.value);
  }
}
