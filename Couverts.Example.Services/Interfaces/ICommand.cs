namespace Couverts.Example.Services.Interfaces {
    public interface ICommand {
        void Execute(WebClient client);
    }
}