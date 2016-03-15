using System;
using System.Linq;
using Couverts.Example.Services.Calls.CreateReservation;
using Couverts.Example.Services.Calls.GetInputFields;
using Couverts.Example.Web.Models;

namespace Couverts.Example.Web.Helpers
{
    public class ModelMapper
    {
        public static CreateReservationRequest CreateReservationRequestFromViewModel(ReservationModel viewModel) {
            var reservationRequest = new CreateReservationRequest {
                Date = Date.FromDateTime(viewModel.DateTime),
                Time = Time.FromDateTime(viewModel.DateTime),
                NumPersons = viewModel.Persons,
                Language = CreateReservationRequest.LanguageType.Dutch,
                Gender = (CreateReservationRequest.GenderType?) Enum.Parse(typeof(CreateReservationRequest.GenderType), viewModel.Fields.SingleOrDefault(f => f.Id == "Gender").Value, true),
                BirthDate = !String.IsNullOrWhiteSpace(viewModel.Fields.SingleOrDefault(f => f.Id == "BirthDate")?.Value) ? Date.FromDateTime(DateTime.Parse(viewModel.Fields.SingleOrDefault(f => f.Id == "BirthDate").Value)) : null,
                FirstName = viewModel.Fields.SingleOrDefault(f => f.Id == "FirstName")?.Value,
                LastName = viewModel.Fields.SingleOrDefault(f => f.Id == "LastName")?.Value,
                Email = viewModel.Fields.SingleOrDefault(f => f.Id == "Email")?.Value,
                PhoneNumber = viewModel.Fields.SingleOrDefault(f => f.Id == "PhoneNumber")?.Value,
                PostalCode = viewModel.Fields.SingleOrDefault(f => f.Id == "PostalCode")?.Value,
                Comments = viewModel.Fields.SingleOrDefault(f => f.Id == "Comments")?.Value
            };

            var defaultFields = reservationRequest.DefaultFields();
            foreach (var dynamicField in viewModel.Fields) {
                if (defaultFields.Contains(dynamicField.Id)) {
                    continue;
                }

                int id;
                if (int.TryParse(dynamicField.Id, out id)) {
                    reservationRequest.RestaurantSpecificFields.Add(new CreateReservationRequest.RestaurantField {
                        Id = id,
                        Value = dynamicField.Value
                    });
                }
            }
            return reservationRequest;
        }

        public static void MapFieldsToViewModel(GetInputFieldsResult fieldsConfiguration, ReservationModel viewModel) {
            var defaultFields = fieldsConfiguration.DefaultFields();
            var properties = fieldsConfiguration
                .GetType()
                .GetProperties()
                .Where(p => defaultFields.Contains(p.Name))
                .Select(pv => new {
                    Id = pv.Name,
                    Title = TranslationHelper.GetTranslationFor(pv.Name),
                    Show = ((GetInputFieldsResult.ContactDetailsField) pv.GetValue(fieldsConfiguration)).Show,
                    Required = ((GetInputFieldsResult.ContactDetailsField) pv.GetValue(fieldsConfiguration)).Required
                });

            foreach (var propertyInfo in properties.Where(propertyInfo => propertyInfo.Show)) {
                viewModel.Fields.Add(new ReservationModel.DynamicField {
                    Id = propertyInfo.Id,
                    Title = propertyInfo.Title,
                    Required = propertyInfo.Required
                });
            }

            foreach (var restaurantSpecificField in fieldsConfiguration.RestaurantSpecificFields) {
                viewModel.Fields.Add(new ReservationModel.DynamicField {
                    Id = restaurantSpecificField.Id.ToString(),
                    Title = restaurantSpecificField.Title.Dutch,
                    Required = restaurantSpecificField.Required,
                    Type = (ReservationModel.DynamicField.FieldType) (int) restaurantSpecificField.Type
                });
            }
        }
    }
}