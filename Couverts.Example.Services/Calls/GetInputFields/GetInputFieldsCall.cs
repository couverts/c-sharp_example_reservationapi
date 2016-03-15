using System;
using Couverts.Example.Services.Interfaces;

namespace Couverts.Example.Services.Calls.GetInputFields {
    public class GetInputFieldsCall : ICommandWithResult<GetInputFieldsResult> {
        private readonly DateTime _date;

        public GetInputFieldsCall(DateTime date) {
            _date = date;
        }

        public void Execute(WebClient client)
        {
            Result = client.Get<GetInputFieldsResult>("/InputFields",
                new
                {
                    Year = _date.Year,
                    Month = _date.Month,
                    Day = _date.Day,
                    Hours = _date.Hour,
                    Minutes = _date.Minute
                });
        }

        public GetInputFieldsResult Result { get; set; }
    }
}