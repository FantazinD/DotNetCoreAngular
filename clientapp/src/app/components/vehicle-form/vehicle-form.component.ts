import { FormsModule } from '@angular/forms';
import { MakeService } from './../../../../services/make.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './vehicle-form.component.html',
  styleUrl: './vehicle-form.component.css',
})
export class VehicleFormComponent implements OnInit {
  makes: any[] = [];
  models: any[] = [];
  vehicle: any = {
    make: '',
  };
  constructor(private makeService: MakeService) {}

  ngOnInit(): void {
    //this.makeService.getMakes().subscribe((makes: any) => (this.makes = makes));
    this.makes = this.makeService.getMakes();
  }

  onMakeChange = (): void => {
    let selectedMake = this.makes.find((make) => make.id == this.vehicle.make);
    this.models = selectedMake ? selectedMake.models : [];
  };
}
