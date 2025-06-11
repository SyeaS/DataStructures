using DataStructures.Trees.BinaryTrees;
using DataStructures.Wrappers.Numerics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataStructures.Tests.Trees
{
    public class MinTreapTreeTests
    {
        private static Random random = new Random();

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void Add_ShouldWork(params double[] values)
        {
            MinTreapTree<double, DoubleWrapper, double> treapTree = new MinTreapTree<double, DoubleWrapper, double>(values, GetRandom, new DoubleWrapper(double.MaxValue));

            for (int i = 0; i < values.Length; i++)
            {
                Assert.True(treapTree.Contains(values[i]), "Add didn't work! (Element not found)");
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
                new MinTreapTree<double, DoubleWrapper, double>(values, GetRandom, new DoubleWrapper(double.MaxValue)).Add(vals[0]);
            });
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void Remove_ShouldWork(params double[] values)
        {
            MinTreapTree<double, DoubleWrapper, double> treapTree = new MinTreapTree<double, DoubleWrapper, double>(values, GetRandom, new DoubleWrapper(double.MaxValue));

            for (int i = 0; i < values.Length; i++)
            {
                treapTree.Remove(values[i]);

                if (treapTree.Contains(values[i]))
                {
                    Assert.True(false, "Tree contains the 'removed' element!");
                }
            }

            Assert.True(treapTree.Count == 0);
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void Remove_ShouldThrowException(params double[] values)
        {
            MinTreapTree<double, DoubleWrapper, double> treapTree = new MinTreapTree<double, DoubleWrapper, double>(values, GetRandom, new DoubleWrapper(double.MaxValue));

            for (int i = 0; i < values.Length; i++)
            {
                Assert.Throws<TreeElementNotFoundException>(() =>
                {
                    treapTree.Remove(values[i] + 5d);
                });
            }
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void InOrderTest(params double[] values)
        {
            MinTreapTree<double, DoubleWrapper, double> treapTree = new MinTreapTree<double, DoubleWrapper, double>(values, GetRandom, new DoubleWrapper(double.MaxValue));
            List<double> inorder = new List<double>();
            treapTree.InOrder((item) => { inorder.Add(item); });

            inorder = new List<double>();

            foreach (double item in treapTree.InOrder())
            {
                inorder.Add(item);
            }
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void PreOrderTest(params double[] values)
        {
            MinTreapTree<double, DoubleWrapper, double> treapTree = new MinTreapTree<double, DoubleWrapper, double>(values, GetRandom, new DoubleWrapper(double.MaxValue));
            List<double> preorder = new List<double>();
            treapTree.PreOrder((item) => { preorder.Add(item); });

            preorder = new List<double>();

            foreach (double item in treapTree.PreOrder())
            {
                preorder.Add(item);
            }
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void PostOrderTest(params double[] values)
        {
            MinTreapTree<double, DoubleWrapper, double> treapTree = new MinTreapTree<double, DoubleWrapper, double>(values, GetRandom, new DoubleWrapper(double.MaxValue));
            List<double> postorder = new List<double>();
            treapTree.PostOrder((item) => { postorder.Add(item); });

            postorder = new List<double>();

            foreach (double item in treapTree.PostOrder())
            {
                postorder.Add(item);
            }
        }

        [Theory]
        [InlineData(55.52d, 24.562d, 1.63d, 0.006d, 7.3421d, 89.221d, 62.5d, 16.73d, 78.61d, 81.701d)]
        public void BreadthFirstTest(params double[] values)
        {
            MinTreapTree<double, DoubleWrapper, double> treapTree = new MinTreapTree<double, DoubleWrapper, double>(values, GetRandom, new DoubleWrapper(double.MaxValue));
            List<double> breadthFirst = new List<double>();
            treapTree.BreadthFirst((item) => { breadthFirst.Add(item); });

            breadthFirst = new List<double>();

            foreach (double item in treapTree.BreadthFirst())
            {
                breadthFirst.Add(item);
            }
        }

        [Fact]
        public void Add_TimerTest()
        {
            MinTreapTree<double, DoubleWrapper, double> treapTree = new MinTreapTree<double, DoubleWrapper, double>(GetRandom, new DoubleWrapper(double.MaxValue));
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\MinTreapTreeAddTest.txt";
            File.Delete(path);
            StringBuilder fileBuilder = new StringBuilder();
            DateTime startTime = DateTime.Now;

            int x = 10;

            for (int i = 0; i < 100000; i++)
            {
                treapTree.Add(i);
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
            MinTreapTree<double, DoubleWrapper, double> treapTree = new MinTreapTree<double, DoubleWrapper, double>(GetRandom, new DoubleWrapper(double.MaxValue));
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\MinTreapTreeSearchTest.txt";
            File.Delete(path);

            for (int i = 0; i < 100000; i++)
            {
                treapTree.Add(i);
            }
            DateTime startTime = DateTime.Now;
            StringBuilder file = new StringBuilder();
            int x = 10;


            for (int i = 0; i < 100000; i++)
            {
                treapTree.Contains(i);
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
            MinTreapTree<double, DoubleWrapper, double> treapTree = new MinTreapTree<double, DoubleWrapper, double>(GetRandom, new DoubleWrapper(double.MaxValue));
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\MinTreapTreeRemoveTest.txt";
            File.Delete(path);

            for (int i = 0; i < 100000; i++)
            {
                treapTree.Add(i);
            }
            DateTime startTime = DateTime.Now;
            StringBuilder file = new StringBuilder();
            int x = 10;


            for (int i = 0; i < 100000; i++)
            {
                treapTree.Remove(i);
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

        public DoubleWrapper GetRandom()
        {
            return new DoubleWrapper(random.NextDouble() + random.Next(int.MaxValue));
        }
    }
}