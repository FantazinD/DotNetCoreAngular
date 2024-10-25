import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  SimpleChanges,
} from '@angular/core';

@Component({
  selector: 'app-pagination',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.css',
})
export class PaginationComponent implements OnChanges {
  @Input('total-items') totalItems: any;
  @Input('is-disabled') isDisabled: boolean = false;
  @Input('page-size') pageSize: number = 10;
  @Output('page-changed') pageChanged = new EventEmitter();
  pages: any[] = [];
  currentPage = 1;

  ngOnChanges(changes: SimpleChanges): void {
    this.currentPage = 1;

    let pagesCount = Math.ceil(this.totalItems / this.pageSize);
    this.pages = [];
    for (let i = 1; i <= pagesCount; i++) this.pages.push(i);
  }

  onChangePage = (page: number) => {
    this.currentPage = page;

    this.pageChanged.emit(page);
  };

  onClickPrevious = () => {
    if (this.currentPage == 1) return;

    this.currentPage--;

    this.pageChanged.emit(this.currentPage);
  };

  onClickNext = () => {
    if (this.currentPage == this.pages.length) return;

    this.currentPage++;

    this.pageChanged.emit(this.currentPage);
  };
}
