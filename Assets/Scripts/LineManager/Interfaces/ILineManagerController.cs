using DSR.Objects;

namespace DSR.LineManager.Interfaces
{
    public interface ILineManagerController
    {
        void KeyInput(KeyData keyValue);
        Line CommandInput(KeyData keyValue);
        void ActionInput(KeyData keyAction);
        void ValueInput(KeyData keyValue);
    }
}
