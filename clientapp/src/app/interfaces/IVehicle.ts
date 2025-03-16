import { IKeyValuePair } from './IKeyValuePair';
import { IContact } from './IContact';

export interface IVehicle {
  id: number;
  model: IKeyValuePair;
  make: IKeyValuePair;
  isRegistered: boolean;
  features: IKeyValuePair[];
  contact: IContact;
  lastUpdate: string;
}
