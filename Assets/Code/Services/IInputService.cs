using System;

namespace Code
{
    public interface IInputService
    {
        event Action OnClick;
        void Enable();
        void Disable();
        void Update();
    }
}