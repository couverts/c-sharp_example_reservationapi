namespace Couverts.Example.Services.Interfaces {
    public interface ICommandWithResult<T> : ICommand {
        T Result { get; set; }
    }
}