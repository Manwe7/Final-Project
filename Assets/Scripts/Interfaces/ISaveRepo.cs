namespace Interfaces
{
    public interface ISaveRepo<T>
    {
        void Save(T value);
    }
}
