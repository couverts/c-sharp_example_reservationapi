namespace Couverts.Example.Services.Calls.CreateReservation {
    public static class CreateReservationRequestExt {
        public static string[] DefaultFields(this CreateReservationRequest self) {
            return new[] {
                nameof(self.Date),
                nameof(self.Time),
                nameof(self.NumPersons),
                nameof(self.Gender),
                nameof(self.FirstName),
                nameof(self.LastName),
                nameof(self.BirthDate),
                nameof(self.Email),
                nameof(self.PhoneNumber),
                nameof(self.Language),
                nameof(self.PostalCode),
                nameof(self.Comments)
            };
        }
    }
}