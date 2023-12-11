namespace Wonkysattva.Collections.Generic
{
    /// <summary>
    /// When implemented in a derived class, provides a binary heap implementation
    /// </summary>
    /// <typeparam name="TItem">The type of items to hold</typeparam>
    public interface IBinaryHeap<TItem>
    {
        /// <summary>
        /// The count of items in the heap
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Push an item onto the heap
        /// </summary>
        /// <param name="item">The item</param>
        void Push(TItem item);

        /// <summary>
        /// Pop the first element from the heap
        /// </summary>
        /// <returns>The first element from the heap</returns>
        TItem Pop();

        /// <summary>
        /// Push an element onto the heap and pop the first element from the heap
        /// </summary>
        /// <param name="item">The item to push onto the heap</param>
        /// <returns>The first element from the heap after pushing</returns>
        TItem PushPop(TItem item);
    }
}