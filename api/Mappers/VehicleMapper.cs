using api.DTOs.Contact;
using api.DTOs.Vehicle;
using api.Models;

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

        public static VehicleDTO ToVehicleDTO (this Vehicle vehicle){
            var fer = vehicle.Features.Select(feature => feature.FeatureId).ToList();
            return new VehicleDTO {
                Id = vehicle.Id,
                Model = vehicle.Model?.ToIdNameObjectDTO(),
                Make = vehicle.Model?.Make?.ToIdNameObjectDTO(),
                IsRegistered = vehicle.IsRegistered,
                Contact = new ContactDTO {
                    Name = vehicle.ContactName,
                    Email = vehicle.ContactEmail,
                    Phone = vehicle.ContactPhone
                },
                Features = vehicle.Features.Select(feature => feature.Feature.ToIdNameObjectDTO()).ToArray()
            };
        }

        public static Vehicle ToUpdatedVehicle (this Vehicle vehicle, SaveVehicleDTO vehicleDTO){
            var updatedVehicle = vehicle;
            updatedVehicle.IsRegistered = vehicleDTO.IsRegistered == null ? vehicle.IsRegistered : vehicleDTO.IsRegistered;
            updatedVehicle.ContactName = vehicleDTO.Contact.Name ?? vehicle.ContactName;
            updatedVehicle.ContactEmail = vehicleDTO.Contact.Email ?? vehicle.ContactEmail;
            updatedVehicle.ContactPhone = vehicleDTO.Contact.Phone ?? vehicle.ContactPhone;
            updatedVehicle.Features = vehicleDTO.Features == null ? 
                vehicle.Features : 
                vehicleDTO.Features.SequenceEqual(updatedVehicle.Features.Select(feature => feature.FeatureId).ToList()) ? 
                    vehicle.Features : 
                    vehicleDTO.Features.Distinct().Select(featureId => new VehicleFeature 
                    {
                        FeatureId = featureId
                    }).ToList();

            return updatedVehicle;
        }

        public static VehicleQuery ToVehicleQuery(this VehicleQueryDTO filterDTO){
            return new VehicleQuery{
                MakeId = filterDTO.MakeId,
                ModelId = filterDTO.ModelId,
                SortBy = filterDTO.SortBy,
                IsSortAscending = filterDTO.IsSortAscending,
                Page = filterDTO.Page,
                PageSize = filterDTO.PageSize
            };
        }
    }
}