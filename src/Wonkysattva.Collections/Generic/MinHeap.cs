using System.Collections.Generic;

namespace Wonkysattva.Collections.Generic
{
    public class MinHeap<T> : BinaryHeapBase<T>, IMinHeap<T>
    {
        public MinHeap()
        {
        }

        public MinHeap(IComparer<T> comparer)
            : base(comparer)
        {
        }

        public MinHeap(int capacity)
            : base(capacity)
        {
        }

        public MinHeap(int capacity, IComparer<T> comparer)
            : base(capacity, comparer)
        {
        }

        public MinHeap(IEnumerable<T> initial, IComparer<T> comparer)
            : base(initial, comparer)
        {
        }
    }
}