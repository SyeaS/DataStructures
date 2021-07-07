using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SortedPlayerQueue.Tests
{
    public class BinarySearchTreeTests
    {
        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void Add_ShouldWork(params double[] values)
        {
            BinarySearchTree<double> binarySearchTree = new BinarySearchTree<double>(values.AsEnumerable());

            for (int i = 0; i < values.Length; i++)
            {
                Assert.True(binarySearchTree.Contains(values[i]), "Add didn't work! (Element not found)");
            }
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void Add_ShouldThrowException(params double[] values)
        {
            double[] vals = new double[values.Length * 2];
            values.CopyTo(vals, 0);
            values.CopyTo(vals, values.Length);

            Assert.Throws<DuplicateTreeElementException>(() =>
            {
                new BinarySearchTree<double>(vals.AsEnumerable());
            });
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void Remove_ShouldWork(params double[] values)
        {
            BinarySearchTree<double> binarySearchTree = new BinarySearchTree<double>(values.AsEnumerable());

            for (int i = 0; i < values.Length; i++)
            {
                binarySearchTree.Remove(values[i]);
            }

            Assert.True(binarySearchTree.Count == 0);
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void Remove_ShouldThrowException(params double[] values)
        {
            BinarySearchTree<double> binarySearchTree = new BinarySearchTree<double>(values.AsEnumerable());

            for (int i = 0; i < values.Length; i++)
            {
                Assert.Throws<TreeElementNotFoundException>(() =>
                {
                    binarySearchTree.Remove(values[i] + 5d);
                });
            }
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void InOrderTest(params double[] values)
        {
            BinarySearchTree<double> binarySearchTree = new BinarySearchTree<double>(values);
            List<double> inorder = new List<double>();
            binarySearchTree.InOrder((item) => { inorder.Add(item); });

            inorder = new List<double>();

            foreach (double item in binarySearchTree.InOrder())
            {
                inorder.Add(item);
            }
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void PreOrderTest(params double[] values)
        {
            BinarySearchTree<double> binarySearchTree = new BinarySearchTree<double>(values);
            List<double> preorder = new List<double>();
            binarySearchTree.PreOrder((item) => { preorder.Add(item); });

            preorder = new List<double>();

            foreach (double item in binarySearchTree.PreOrder())
            {
                preorder.Add(item);
            }
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void PostOrderTest(params double[] values)
        {
            BinarySearchTree<double> binarySearchTree = new BinarySearchTree<double>(values);
            List<double> postorder = new List<double>();
            binarySearchTree.PostOrder((item) => { postorder.Add(item); });

            postorder = new List<double>();

            foreach (double item in binarySearchTree.PostOrder())
            {
                postorder.Add(item);
            }
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void BreadthFirstTest(params double[] values)
        {
            BinarySearchTree<double> binarySearchTree = new BinarySearchTree<double>(values);
            List<double> breadthFirst = new List<double>();
            binarySearchTree.BreadthFirst((item) => { breadthFirst.Add(item); });

            breadthFirst = new List<double>();

            foreach (double item in binarySearchTree.BreadthFirst())
            {
                breadthFirst.Add(item);
            }
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void DepthFirstTest(params double[] values)
        {
            BinarySearchTree<double> binarySearchTree = new BinarySearchTree<double>(values);
            List<double> depthFirst = new List<double>();
            binarySearchTree.DepthFirst((item) => { depthFirst.Add(item); });

            depthFirst = new List<double>();

            foreach (double item in binarySearchTree.DepthFirst())
            {
                depthFirst.Add(item);
            }
        }
    }
}