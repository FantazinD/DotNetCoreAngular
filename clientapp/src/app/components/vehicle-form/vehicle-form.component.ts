import { MakeService } from './../../../../services/make.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-form',
  standalone: true,
  imports: [],
  templateUrl: './vehicle-form.component.html',
  styleUrl: './vehicle-form.component.css',
})
export class VehicleFormComponent implements OnInit {
  makes: any;
  constructor(private makeService: MakeService) {}

  ngOnInit(): void {
    this.makeService.getMakes().subscribe((makes: any) => {
      this.makes = makes;
    });
  }
}
