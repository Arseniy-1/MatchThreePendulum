using System;

namespace Code.Services
{
    public interface IInputService
    {
        event Action OnClick;
        void Enable();
        void Disable();
        void Update();
    }
}