using Couverts.Example.Services.Interfaces;

namespace Couverts.Example.Services {
    public static class ApiServiceClient {
        public static void Execute(ICommand command) {
            using (var apiClient = new WebClient()) {
                command.Execute(apiClient);
            }
        }

        public static T Execute<T>(ICommandWithResult<T> command) {
            using (var apiClient = new WebClient()) {
                command.Execute(apiClient);
                return command.Result;
            }
        }
    }
}