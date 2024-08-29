using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Vehicle;
using api.Models;

namespace api.Mappers
{
    public static class VehicleMapper
    {
        public static Vehicle ToVehicle (this VehicleDTO vehicleDTO){
            return new Vehicle {
                ModelId = vehicleDTO.ModelId,
                IsRegistered = vehicleDTO.IsRegistered,
                ContactName = vehicleDTO.Contact.Name,
                ContactEmail = vehicleDTO.Contact.Email,
                ContactPhone = vehicleDTO.Contact.Phone,
                Features = vehicleDTO.Features.Select(featureId => new VehicleFeature {
                    FeatureId = featureId
                }).ToList()
            };
        }

        public static Vehicle UpdateVehicle (this Vehicle vehicle, VehicleDTO vehicleDTO){
            var updatedVehicle = vehicle;
            updatedVehicle.IsRegistered = vehicleDTO.IsRegistered == null ? updatedVehicle.IsRegistered : vehicleDTO.IsRegistered;
            updatedVehicle.ContactName = vehicleDTO.Contact.Name ?? updatedVehicle.ContactName;
            updatedVehicle.ContactEmail = vehicleDTO.Contact.Email ?? updatedVehicle.ContactEmail;
            updatedVehicle.ContactPhone = vehicleDTO.Contact.Phone ?? updatedVehicle.ContactPhone;
            updatedVehicle.Features = vehicleDTO.Features == null ? updatedVehicle.Features : vehicleDTO.Features.Select(featureId => new VehicleFeature {
                    FeatureId = featureId
                }).ToList();

            return updatedVehicle;
        }
    }
}