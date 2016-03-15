using System;
using Couverts.Example.Services.Interfaces;

namespace Couverts.Example.Services.Calls.GetAvailableTimes {
    public class GetAvailableTimesCall : ICommandWithResult<GetAvailableTimesResult> {
        private readonly DateTime _date;
        private readonly int _persons;

        public GetAvailableTimesCall(DateTime date, int persons) {
            _date = date;
            _persons = persons;
        }

        public void Execute(WebClient client)
        {
            Result = client.Get<GetAvailableTimesResult>("/AvailableTimes",
                new {Year = _date.Year, Month = _date.Month, Day = _date.Day, NumPersons = _persons});
        }

        public GetAvailableTimesResult Result { get; set; }
    }
}