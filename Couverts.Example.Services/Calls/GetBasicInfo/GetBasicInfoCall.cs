using Couverts.Example.Services.Interfaces;

namespace Couverts.Example.Services.Calls.GetBasicInfo {
    public class GetBasicInfoCall : ICommandWithResult<GetBasicInfoResult> {
        public void Execute(WebClient client) {
            Result = client.Get<GetBasicInfoResult>("/BasicInfo");
        }

        public GetBasicInfoResult Result { get; set; }
    }
}