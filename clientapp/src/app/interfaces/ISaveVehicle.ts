import { IContact } from './IContact';

export interface ISaveVehicle {
  id: number;
  modelId?: number;
  makeId: number;
  isRegistered: boolean;
  features: number[];
  contact: IContact;
}
