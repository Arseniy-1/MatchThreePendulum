using System;

namespace Code.Services.Pool
{
    public interface IDestoyable<T>
    {
        public event Action<T> Destroyed;

        void Die();
    }
}