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
                LastUpdate = DateTime.Now,
                Features = vehicleDTO.Features.Select(featureId => new VehicleFeature {
                    FeatureId = featureId
                }).ToList()
            };
        } 
    }
}