namespace Wonkysattva.Collections.Generic
{
    public interface IBinaryHeap<T>
    {
        int Count { get; }
        void Push(T item);
        T Pop();
        T PushPop(T item);
    }
}