using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Wonkysattva.Collections.Generic
{
    /// <summary>
    /// A binary heap
    /// </summary>
    /// <typeparam name="TItem">The type stored in the heap</typeparam>
    public class BinaryHeap<TItem> : IBinaryHeap<TItem>, IReadOnlyCollection<TItem>
    {
        private readonly List<TItem> _heap;
        private readonly IComparer<TItem> _comparer;

        /// <summary>
        /// Construct a <see cref="BinaryHeap{TItem}"/>
        /// </summary>
        public BinaryHeap()
            : this(0)
        {
        }

        /// <summary>
        /// Construct a <see cref="BinaryHeap{TItem}"/>
        /// </summary>
        /// <param name="comparer">The comparer to use to compare items</param>
        public BinaryHeap(IComparer<TItem> comparer)
            : this(0, comparer)
        {
        }

        /// <summary>
        /// Construct a <see cref="BinaryHeap{TItem}"/>
        /// </summary>
        /// <param name="capacity">The initial capacity of the heap</param>
        public BinaryHeap(int capacity)
            : this(capacity, Comparer<TItem>.Default)
        {
        }

        /// <summary>
        /// Construct a <see cref="BinaryHeap{TItem}"/>
        /// </summary>
        /// <param name="capacity">The initial capacity of the heap</param>
        /// <param name="comparer">The comparer to use to compare items</param>
        /// <exception cref="ArgumentNullException">Thrown when comparer is null</exception>
        public BinaryHeap(int capacity, IComparer<TItem> comparer)
        {
            _heap = new List<TItem>(capacity);
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        /// <summary>
        /// Construct a <see cref="BinaryHeap{TItem}"/>
        /// </summary>
        /// <param name="initial">The initial values to store in the heap</param>
        /// <param name="comparer">The comparer to use to compare items</param>
        /// <exception cref="ArgumentNullException">Thrown when initial or comparer is null</exception>
        public BinaryHeap(IEnumerable<TItem> initial, IComparer<TItem> comparer)
        {
            _heap = new List<TItem>(initial ?? throw new ArgumentNullException(nameof(initial)));
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));

            for (var i = _heap.Count / 2; i >= 0; ++i)
            {
                BubbleDown(i);
            }
        }

        /// <summary>
        /// Construct a <see cref="BinaryHeap{TItem}"/>
        /// </summary>
        /// <param name="other">The binary heap to copy</param>
        public BinaryHeap(BinaryHeap<TItem> other)
        {
            _heap = other._heap.ToList();
            _comparer = other._comparer;
        }

        /// <summary>
        /// The count of items in the heap
        /// </summary>
        public int Count => _heap.Count;

        /// <inheritdoc />
        public void Push(TItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _heap.Add(item);
            BubbleUp(_heap.Count - 1);
        }

        /// <inheritdoc />
        public TItem Pop()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException("Heap is empty");
            }

            var minItem = _heap[0];
            var lastIndex = _heap.Count - 1;

            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);
            BubbleDown(0);

            return minItem;
        }

        /// <summary>
        /// Push an element onto the heap and pop the first element from the heap
        /// </summary>
        /// <param name="item">The item to push onto the heap</param>
        /// <returns>The first element from the heap after pushing</returns>
        public TItem PushPop(TItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (_heap.Count == 0 || _comparer.Compare(item, _heap[0]) <= 0)
            {
                return item;
            }

            var result = _heap[0];

            _heap[0] = item;
            BubbleDown(0);

            return result;
        }

        /// <inheritdoc />
        public IEnumerator<TItem> GetEnumerator()
        {
            var copy = _heap.ToList();

            while (copy.Count > 0)
            {
                yield return copy[0];

                var lastIndex = copy.Count - 1;
                copy[0] = copy[lastIndex];
                copy.RemoveAt(lastIndex);
                BubbleDown(copy, _comparer, 0);
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void BubbleUp(int index)
        {
            while (index > 0)
            {
                var parentIndex = (index - 1) / 2;

                if (_comparer.Compare(_heap[index], _heap[parentIndex]) < 0)
                {
                    (_heap[index], _heap[parentIndex]) = (_heap[parentIndex], _heap[index]);
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
            BubbleDown(_heap, _comparer, index);
        }

        private static void BubbleDown(IList<TItem> heap, IComparer<TItem> comparer, int index)
        {
            while (true)
            {
                var leftChild = 2 * index + 1;
                var rightChild = 2 * index + 2;
                var smallest = index;

                if (leftChild < heap.Count && comparer.Compare(heap[leftChild], heap[smallest]) < 0)
                {
                    smallest = leftChild;
                }

                if (rightChild < heap.Count && comparer.Compare(heap[rightChild], heap[smallest]) < 0)
                {
                    smallest = rightChild;
                }

                if (smallest != index)
                {
                    (heap[index], heap[smallest]) = (heap[smallest], heap[index]);
                    index = smallest;

                    continue;
                }

                break;
            }
        }
    }
}
