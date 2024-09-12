import { IContact } from './IContact';

export interface IVehicle {
  id: number;
  modelId: number;
  makeId: number;
  isRegistered: boolean;
  features: number[];
  contact: IContact;
}
