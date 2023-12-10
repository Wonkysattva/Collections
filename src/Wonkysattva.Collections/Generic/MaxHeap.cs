using System.Collections.Generic;

namespace Wonkysattva.Collections.Generic
{
    public class MaxHeap<T> : BinaryHeapBase<T>, IMaxHeap<T>
    {
        public MaxHeap()
            : this(0)
        {
        }

        public MaxHeap(IComparer<T> comparer)
            : this(0, comparer)
        {
        }

        public MaxHeap(int capacity)
            : this(capacity, Comparer<T>.Default)
        {
        }

        public MaxHeap(int capacity, IComparer<T> comparer)
            : base(capacity, Comparer<T>.Create((a, b) => comparer.Compare(b, a)))
        {
        }

        public MaxHeap(IEnumerable<T> initial, IComparer<T> comparer)
            : base(initial, Comparer<T>.Create((a, b) => comparer.Compare(b, a)))
        {
        }
    }
}