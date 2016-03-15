using Couverts.Example.Services.Results.Common;

namespace Couverts.Example.Services.Calls.GetBasicInfo {
    public class GetBasicInfoResult {
        public string RestaurantName { get; set; }
        public string RestaurantCity { get; set; }
        public string RestaurantPhoneNumber { get; set; }
        public bool DisableEnglishIFrame { get; set; }
        public MultilingualTextField IntroText { get; set; }
    }
}