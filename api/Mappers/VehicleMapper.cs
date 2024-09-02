using api.DTOs.Contact;
using api.DTOs.Vehicle;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Mappers
{
    public static class VehicleMapper
    {
        public static Vehicle ToVehicle (this SaveVehicleDTO vehicleDTO){
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

        public static SaveVehicleDTO ToVehicleDTO (this Vehicle vehicle){
            var fer = vehicle.Features.Select(feature => feature.FeatureId).ToList();
            return new SaveVehicleDTO {
                Id = vehicle.Id,
                ModelId = vehicle.ModelId,
                IsRegistered = vehicle.IsRegistered,
                Contact = new ContactDTO {
                    Name = vehicle.ContactName,
                    Email = vehicle.ContactEmail,
                    Phone = vehicle.ContactPhone
                },
                Features = vehicle.Features.Select(feature => feature.FeatureId).ToArray()
            };
        }

        public static Vehicle UpdateVehicle (this Vehicle vehicle, SaveVehicleDTO vehicleDTO){
            var updatedVehicle = vehicle;
            updatedVehicle.IsRegistered = vehicleDTO.IsRegistered == null ? vehicle.IsRegistered : vehicleDTO.IsRegistered;
            updatedVehicle.ContactName = vehicleDTO.Contact.Name ?? vehicle.ContactName;
            updatedVehicle.ContactEmail = vehicleDTO.Contact.Email ?? vehicle.ContactEmail;
            updatedVehicle.ContactPhone = vehicleDTO.Contact.Phone ?? vehicle.ContactPhone;
            updatedVehicle.Features = vehicleDTO.Features.IsNullOrEmpty() ? 
                vehicle.Features : 
                vehicleDTO.Features.SequenceEqual(updatedVehicle.Features.Select(feature => feature.FeatureId).ToList()) ? 
                    vehicle.Features : 
                    vehicleDTO.Features.Select(featureId => new VehicleFeature 
                    {
                        FeatureId = featureId
                    }).ToList();

            return updatedVehicle;
        }
    }
}