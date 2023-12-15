using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Wonkysattva.Collections.Generic;
using Xunit;

namespace Wonkysattva.Collections.Tests.Generic
{
    [TestSubject(typeof(BinaryHeap<>))]
    public class BinaryHeapTest
    {

        [Fact]
        public void TestBinaryHeap()
        {
            var rng = new Random();
            var numbers = Enumerable.Range(0, 1000).OrderBy(_ => rng.Next()).ToList();
            var binaryHeap = new BinaryHeap<int>(numbers[..10], Comparer<int>.Create((a, b) => Comparer<int>.Default.Compare(b, a)));

            foreach (var number in numbers[10..])
            {
                binaryHeap.PushPop(number);
            }

            var result = binaryHeap.Reverse().ToList();
            
            Assert.Equal(0, result[0]);
            Assert.Equal(1, result[1]);
            Assert.Equal(2, result[2]);
            Assert.Equal(3, result[3]);
            Assert.Equal(4, result[4]);
            Assert.Equal(5, result[5]);
            Assert.Equal(6, result[6]);
            Assert.Equal(7, result[7]);
            Assert.Equal(8, result[8]);
            Assert.Equal(9, result[9]);
        }
    }
}