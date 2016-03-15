using Couverts.Example.Services.Interfaces;

namespace Couverts.Example.Services.Calls.CreateReservation {
    public class CreateReservationCall : ICommandWithResult<CreateReservationResult> {
        private readonly CreateReservationRequest _createReservationRequest;

        public CreateReservationCall(CreateReservationRequest createReservationRequest) {
            _createReservationRequest = createReservationRequest;
        }

        public void Execute(WebClient client) {
            Result = client.Post<CreateReservationResult, CreateReservationRequest>("/Reservation", _createReservationRequest);
        }

        public CreateReservationResult Result { get; set; }
    }
}