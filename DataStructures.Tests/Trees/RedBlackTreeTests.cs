using DataStructures.Trees;
using DataStructures.Trees.BinaryTrees;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataStructures.Tests
{
    public class RedBlackTreeTests
    {
        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        [InlineData(25d, 0d, 50d, -5d, 5d, 75d, 80d, 70d)]
        [InlineData(1d, 2d, 3d, 4d, 5d, 6d, 7d)]
        [InlineData(3d, 21d, 32d, 17d)]
        public void Add_ShouldWork(params double[] values)
        {
            RedBlackTree<double> RBTree = new RedBlackTree<double>(values.AsEnumerable());

            for (int i = 0; i < values.Length; i++)
            {
                Assert.True(RBTree.Contains(values[i]), "Add didn't work! (Element not found)");
            }
        }

        [Fact]
        public void Add_TimerTest()
        {
            RedBlackTree<int> RBTree = new RedBlackTree<int>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\RedBlackTreeAddTest.txt";
            File.Delete(path);
            StringBuilder fileBuilder = new StringBuilder();
            DateTime startTime = DateTime.Now;
            
            int x = 10;

            for (int i = 0; i < 100000; i++)
            {
                RBTree.Add(i);
                if ((i + 1) % (10 * x) == 0)
                {
                    if (x == 10)
                    {
                        fileBuilder.AppendLine($"{ i + 1 }: { (DateTime.Now - startTime).TotalMilliseconds }ms");
                        x += 90;
                    }
                    else
                    {
                        fileBuilder.AppendLine($"\n{ i + 1 }: { (DateTime.Now - startTime).TotalMilliseconds }ms");
                        x += 100;
                    }
                }
            }

            
            File.WriteAllText(path, fileBuilder.ToString());
            string[] file = File.ReadAllLines(path);
            List<string> newFile = new List<string>();
            double prev = 0d;
            if (string.IsNullOrWhiteSpace(file[0]))
            {
                prev = Convert.ToDouble(file[1].Split(' ')[1].Remove(file[1].Split(' ')[1].Length - 2));
            }
            else
            {
                prev = Convert.ToDouble(file[0].Split(' ')[1].Remove(file[0].Split(' ')[1].Length - 2));
            }

            foreach (string line in file)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] parsedLine = line.Split(' ');
                    double act = Convert.ToDouble(parsedLine[1].Remove(parsedLine[1].Length - 2));
                    parsedLine[1] += $" + {act / prev}x";
                    StringBuilder newLine = new StringBuilder();
                    foreach (string item in parsedLine)
                    {
                        newLine.Append(item);
                    }

                    newFile.Add(newLine.ToString());
                    prev = act;
                }
            }

            File.WriteAllLines(path, newFile);
        }

        [Fact]
        public void Search_TimerTest()
        {
            RedBlackTree<int> RBTree = new RedBlackTree<int>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\RedBlackTreeSearchTest.txt";
            File.Delete(path);

            for (int i = 0; i < 100000; i++)
            {
                RBTree.Add(i);
            }
            DateTime startTime = DateTime.Now;
            StringBuilder file = new StringBuilder();
            int x = 10;
            

            for (int i = 0; i < 100000; i++)
            {
                RBTree.Contains(i);
                if ((i + 1) % (10 * x) == 0)
                {
                    file.AppendLine($"{ i + 1 }: { (DateTime.Now - startTime).TotalMilliseconds }ms");
                    if (x == 10)
                    {
                        x += 90;
                    }
                    else
                    {
                        x += 100;
                    }
                }
            }

            
            File.WriteAllText(path, file.ToString());
        }

        [Fact]
        public void Remove_TimerTest()
        {
            RedBlackTree<int> RBTree = new RedBlackTree<int>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\RedBlackTreeRemoveTest.txt";
            File.Delete(path);

            for (int i = 0; i < 100000; i++)
            {
                RBTree.Add(i);
            }
            DateTime startTime = DateTime.Now;
            StringBuilder file = new StringBuilder();
            int x = 10;
            

            for (int i = 0; i < 100000; i++)
            {
                RBTree.Remove(i);
                if ((i + 1) % (10 * x) == 0)
                {
                    file.AppendLine($"{ i + 1 }: { (DateTime.Now - startTime).TotalMilliseconds }ms");
                    if (x == 10)
                    {
                        x += 90;
                    }
                    else
                    {
                        x += 100;
                    }
                }
            }

            
            File.WriteAllText(path, file.ToString());
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
                new RedBlackTree<double>(vals.AsEnumerable());
            });
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        [InlineData(50d, 60d, 40d, 45d)]
        public void Remove_ShouldWork(params double[] values)
        {
            RedBlackTree<double> RBTree = new RedBlackTree<double>(values.AsEnumerable());

            for (int i = 0; i < values.Length; i++)
            {
                RBTree.Remove(values[i]);
            }

            Assert.True(RBTree.Count == 0);
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void Remove_ShouldThrowException(params double[] values)
        {
            RedBlackTree<double> RBTree = new RedBlackTree<double>(values.AsEnumerable());

            for (int i = 0; i < values.Length; i++)
            {
                Assert.Throws<TreeElementNotFoundException>(() =>
                {
                    RBTree.Remove(values[i] + 5d);
                });
            }
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void InOrderTest(params double[] values)
        {
            RedBlackTree<double> RBTree = new RedBlackTree<double>(values);
            List<double> inorder = new List<double>();
            RBTree.InOrder((item) => { inorder.Add(item); });

            inorder = new List<double>();

            foreach (double item in RBTree.InOrder())
            {
                inorder.Add(item);
            }
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void PreOrderTest(params double[] values)
        {
            RedBlackTree<double> RBTree = new RedBlackTree<double>(values);
            List<double> preorder = new List<double>();
            RBTree.PreOrder((item) => { preorder.Add(item); });

            preorder = new List<double>();

            foreach (double item in RBTree.PreOrder())
            {
                preorder.Add(item);
            }
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void PostOrderTest(params double[] values)
        {
            RedBlackTree<double> RBTree = new RedBlackTree<double>(values);
            List<double> postorder = new List<double>();
            RBTree.PostOrder((item) => { postorder.Add(item); });

            postorder = new List<double>();

            foreach (double item in RBTree.PostOrder())
            {
                postorder.Add(item);
            }
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void BreadthFirstTest(params double[] values)
        {
            RedBlackTree<double> RBTree = new RedBlackTree<double>(values);
            List<double> breadthFirst = new List<double>();
            RBTree.BreadthFirst((item) => { breadthFirst.Add(item); });

            breadthFirst = new List<double>();

            foreach (double item in RBTree.BreadthFirst())
            {
                breadthFirst.Add(item);
            }
        }
    }
}