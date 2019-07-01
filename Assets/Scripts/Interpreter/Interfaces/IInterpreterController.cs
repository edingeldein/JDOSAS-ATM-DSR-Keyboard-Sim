using DSR.Objects;

namespace DSR.Interpreter.Interfaces
{
    public interface IInterpreterController
    {
        KeyData Interpret(string key);
    }
}
