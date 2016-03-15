using System;
using System.Collections.Generic;

namespace Couverts.Example.Web.Models {
    public class ReservationModel {
        public DateTime DateTime { get; set; }
        public int Persons { get; set; }

        public List<DynamicField> Fields { get; set; }

        public ReservationModel() {
            Fields = new List<DynamicField>();
        }

        public class DynamicField {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Value { get; set; }
            public FieldType Type { get; set; }
            public bool Required { get; set; }

            public enum FieldType {
                Text,
                Number,
                Checkbox
            }
        }
    }
}