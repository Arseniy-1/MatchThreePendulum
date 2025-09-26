using Code.Services.Pool;

namespace Code.Services
{
    public interface IFactory<T> where T : IDestoyable<T>
    {
        T Create();
    }
}