using System;
using System.Collections.Generic;

namespace Couverts.Example.Services.Calls.CreateReservation {
    public class CreateReservationRequest {
        public Date Date { get; set; }
        public Time Time { get;set; }
        public int NumPersons { get; set; }
        public LanguageType Language { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public GenderType? Gender { get; set; }
        public Date BirthDate { get; set; }
        public string Comments { get; set; }
        public IList<RestaurantField> RestaurantSpecificFields { get; set; }

        public CreateReservationRequest() {
            RestaurantSpecificFields = new List<RestaurantField>();
        }

        public class RestaurantField {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public enum LanguageType {
            Dutch = 0,
            English = 1
        }

        public enum GenderType {
            Male = 0,
            Female = 1
        }
    }

    public class Date
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public static Date FromDateTime(DateTime dateTime)
        {
            return new Date() {Year = dateTime.Year, Month = dateTime.Month, Day = dateTime.Day};
        }
    }

    public class Time
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }

        public static Time FromDateTime(DateTime dateTime)
        {
            return new Time() {Hours = dateTime.Hour, Minutes = dateTime.Minute};
        }
    }
}