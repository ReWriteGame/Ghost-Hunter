using System;

namespace Modules
{
    public interface IDestroyable<out T>
    {
        public event Action<T> OnDestroyObj;
    }
}
