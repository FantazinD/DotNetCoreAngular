import { Component } from '@angular/core';
import { NgbCollapse } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-navmenu',
  standalone: true,
  imports: [NgbCollapse],
  templateUrl: './navmenu.component.html',
  styleUrl: './navmenu.component.css',
})
export class NavmenuComponent {}
