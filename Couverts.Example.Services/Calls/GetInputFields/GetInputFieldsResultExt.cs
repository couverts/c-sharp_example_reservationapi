namespace Couverts.Example.Services.Calls.GetInputFields {
    public static class GetInputFieldsResultExt {
        public static string[] DefaultFields(this GetInputFieldsResult self) {
            return new[] {
                nameof(self.Gender),
                nameof(self.FirstName),
                nameof(self.LastName),
                nameof(self.BirthDate),
                nameof(self.Email),
                nameof(self.PhoneNumber),
                nameof(self.PostalCode),
                nameof(self.Comments)
            };
        }
    }
}
