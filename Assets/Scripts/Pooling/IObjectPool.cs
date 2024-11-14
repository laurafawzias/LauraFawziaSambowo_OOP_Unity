namespace Pooling
{
    public interface IObjectPool<T> where T : class
    {
        T Get();
        void Release(T element);
        int CountInactive { get; }
    }
}
