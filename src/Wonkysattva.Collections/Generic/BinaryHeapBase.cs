using System;
using System.Collections.Generic;

namespace Wonkysattva.Collections.Generic
{
    public abstract class BinaryHeapBase<T> : IBinaryHeap<T>
    {
        private readonly List<T> _heap;
        private readonly IComparer<T> _comparer;

        protected BinaryHeapBase()
            : this(0)
        {
        }

        protected BinaryHeapBase(IComparer<T> comparer)
            : this(0, comparer)
        {
        }

        protected BinaryHeapBase(int capacity)
            : this(capacity, Comparer<T>.Default)
        {
        }

        protected BinaryHeapBase(int capacity, IComparer<T> comparer)
        {
            _ = comparer ?? throw new ArgumentNullException(nameof(comparer));

            _heap = new List<T>(capacity);
            _comparer = comparer;
        }

        protected BinaryHeapBase(IEnumerable<T> initial, IComparer<T> comparer)
        {
            _ = comparer ?? throw new ArgumentNullException(nameof(comparer));

            _heap = new List<T>(initial);
            _comparer = comparer;

            for (var i = _heap.Count / 2; i >= 0; ++i)
            {
                BubbleDown(i);
            }
        }

        public int Count => _heap.Count;

        public void Push(T item)
        {
            _heap.Add(item ?? throw new ArgumentNullException(nameof(item)));
            BubbleUp(_heap.Count - 1);
        }

        public T Pop()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException("Heap is empty");
            }

            var minItem = _heap[0];
            var lastIndex = _heap.Count - 1;

            // Move the last element to the root
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);

            // Restore the heap property
            BubbleDown(0);

            return minItem;
        }

        public T PushPop(T item)
        {
            _ = item ?? throw new ArgumentNullException(nameof(item));

            if (_heap.Count == 0 || _comparer.Compare(item, _heap[0]) <= 0)
            {
                return item;
            }

            var result = _heap[0];

            _heap[0] = item;
            BubbleDown(0);

            return result;
        }

        private void BubbleUp(int index)
        {
            while (index > 0)
            {
                var parentIndex = (index - 1) / 2;

                if (_comparer.Compare(_heap[index], _heap[parentIndex]) < 0)
                {
                    Swap(index, parentIndex);
                    index = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        private void BubbleDown(int index)
        {
            while (true)
            {
                var leftChild = 2 * index + 1;
                var rightChild = 2 * index + 2;
                var smallest = index;

                if (leftChild < _heap.Count && _comparer.Compare(_heap[leftChild], _heap[smallest]) < 0)
                {
                    smallest = leftChild;
                }

                if (rightChild < _heap.Count && _comparer.Compare(_heap[rightChild], _heap[smallest]) < 0)
                {
                    smallest = rightChild;
                }

                if (smallest != index)
                {
                    Swap(index, smallest);
                    index = smallest;

                    continue;
                }

                break;
            }
        }

        private void Swap(int i, int j)
        {
            (_heap[i], _heap[j]) = (_heap[j], _heap[i]);
        }
    }
}
