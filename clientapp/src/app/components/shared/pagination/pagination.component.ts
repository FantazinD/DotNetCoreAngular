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
  imports: [],
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.css',
})
export class PaginationComponent implements OnChanges {
  @Input('total-items') totalItems: any;
  @Input('page-size') pageSize: number = 10;
  @Output('page-changed') pageChanged = new EventEmitter();
  pages: any[] = [];
  currentPage = 1;

  ngOnChanges(changes: SimpleChanges): void {
    this.currentPage = 1;

    var pagesCount = Math.ceil(this.totalItems / this.pageSize);
    this.pages = [];
    for (var i = 1; i <= pagesCount; i++) this.pages.push(i);

    console.log(this);
  }

  changePage = (page: number) => {
    this.currentPage = page;
    this.pageChanged.emit(page);
  };

  previous = () => {
    if (this.currentPage == 1) return;

    this.currentPage--;
    this.pageChanged.emit(this.currentPage);
  };

  next = () => {
    if (this.currentPage == this.pages.length) return;

    this.currentPage++;
    console.log('next', this);
    this.pageChanged.emit(this.currentPage);
  };
}
