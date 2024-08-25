import { FormsModule } from '@angular/forms';
import { MakeService } from './../../../../services/make.service';
import { Component, OnInit } from '@angular/core';
import { FeatureService } from '../../../../services/feature.service';

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
  features: any[] = [];
  vehicle: any = {
    make: '',
  };
  constructor(
    private makeService: MakeService,
    private featureService: FeatureService
  ) {}

  ngOnInit(): void {
    //this.makeService.getMakes().subscribe((makes: any) => (this.makes = makes));
    //this.featureService.getFeatures().subscribe((features: any) => (this.features = features));
    this.makes = this.makeService.getMakes();
    this.features = this.featureService.getFeatures();
  }

  onMakeChange = (): void => {
    let selectedMake = this.makes.find((make) => make.id == this.vehicle.make);
    this.models = selectedMake ? selectedMake.models : [];
  };
}
