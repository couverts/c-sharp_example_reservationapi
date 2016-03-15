using System;
using Couverts.Example.Services.Interfaces;

namespace Couverts.Example.Services.Calls.GetConfigForDay {
    public class GetConfigForDayCall : ICommandWithResult<GetConfigForDayResult> {
        private readonly DateTime _date;

        public GetConfigForDayCall(DateTime date) {
            _date = date;
        }

        public void Execute(WebClient client) {
            Result = client.Get<GetConfigForDayResult>("/ConfigForDay", new { _date.Year, _date.Month, _date.Day });
        }

        public GetConfigForDayResult Result { get; set; }
    }
}