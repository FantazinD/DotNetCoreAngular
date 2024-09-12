import { IContact } from './IContact';
import { IKeyValuePair } from './IKeyValuePair';

export interface IVehicle {
  id: number;
  model: IKeyValuePair;
  make: IKeyValuePair;
  isRegistered: boolean;
  features: IKeyValuePair[];
  contact: IContact;
  lastUpdate: string;
}
