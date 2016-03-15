using System;
using System.ComponentModel.DataAnnotations;

namespace Couverts.Example.Web.Models {
    public class IndexModel {
        public string RestaurantName { get; set; }
        public string IntroText { get; set; }

        [Display(Name = "Datum")]
        public DateTime Date { get; set; }

        [Display(Name = "Tijd")]
        public string Time { get; set; }

        [Display(Name = "Personen")]
        public int Persons { get; set; }
    }
}