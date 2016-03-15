using System.Collections.Generic;

namespace Couverts.Example.Web.Models {
    public class AvailableTimesModel {
        public IEnumerable<string> Times { get; set; }
        public string Message { get; set; }
    }
}