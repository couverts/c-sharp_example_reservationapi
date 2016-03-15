using System.Collections.Generic;
using Couverts.Example.Services.Results.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Couverts.Example.Services.Calls.GetInputFields {
    public class GetInputFieldsResult {
        public ContactDetailsField Gender { get; set; }
        public ContactDetailsField FirstName { get; set; }
        public ContactDetailsField LastName { get; set; }
        public ContactDetailsField Email { get; set; }
        public ContactDetailsField PhoneNumber { get; set; }
        public ContactDetailsField PostalCode { get; set; }
        public ContactDetailsField BirthDate { get; set; }
        public ContactDetailsField Comments { get; set; }
        public List<RestaurantSpecificField> RestaurantSpecificFields { get; set; }

        public class ContactDetailsField {
            public bool Show { get; set; }
            public bool Required { get; set; }
        }

        public class RestaurantSpecificField {
            public int Id { get; set; }
            public MultilingualTextField Title { get; set; }
            public MultilingualTextField Description { get; set; }

            [JsonConverter(typeof (StringEnumConverter))]
            public RestaurantFieldType Type { get; set; }

            public bool Required { get; set; }

            public enum RestaurantFieldType {
                Text,
                Number,
                Checkbox
            }
        }
    }
}