using DataStructures.Heaps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataStructures.Tests.Trees.Heaps
{
    public class MinHeapTests
    {
        [Theory]
        [InlineData(0, 1, 2, 3, 4, 5, 6, 7, 8, 9)]
        [InlineData(9, 8, 7, 6, 5, 4, 3, 2, 1, 0)]
        public void AddTest(params int[] values)
        {
            BinaryMinHeap<HeapData> minHeap = new BinaryMinHeap<HeapData>(HeapData.CreateFromValues(values));
            Assert.True(minHeap.Count == values.Length);
        }

        [Theory]
        [InlineData(0, 1, 2, 3, 4, 5, 6, 7, 8, 9)]
        [InlineData(9, 8, 7, 6, 5, 4, 3, 2, 1, 0)]
        public void RemoveTest(params int[] values)
        {
            BinaryMinHeap<HeapData> minHeap = new BinaryMinHeap<HeapData>(HeapData.CreateFromValues(values));
            int[] sortedArray = new int[values.Length];
            values.CopyTo(sortedArray, 0);
            Array.Sort(sortedArray);

            for (int i = 0; i < values.Length; i++)
            {
                if (sortedArray[i] != minHeap.Extract().Number)
                {
                    Assert.True(false, "Extraction didn't work!");
                }
            }

            Assert.True(minHeap.Count == 0);
        }

        [Fact]
        public void Add_TimerTest()
        {
            BinaryMinHeap<HeapData> minHeap = new BinaryMinHeap<HeapData>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\BinaryMinHeapAddTest.txt";
            File.Delete(path);
            StringBuilder fileBuilder = new StringBuilder();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            int x = 10;

            for (int i = 0; i < 100000; i++)
            {
                minHeap.Add(new HeapData(i));
                if ((i + 1) % (10 * x) == 0)
                {
                    if (x == 10)
                    {
                        fileBuilder.AppendLine($"{ i + 1 }: { stopWatch.Elapsed.TotalMilliseconds }ms");
                    }
                    else
                    {
                        fileBuilder.AppendLine($"\n{ i + 1 }: { stopWatch.Elapsed.TotalMilliseconds }ms");
                    }
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

            stopWatch.Stop();
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
            BinaryMinHeap<HeapData> minHeap = new BinaryMinHeap<HeapData>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\BinaryMinHeapSearchTest.txt";
            File.Delete(path);

            for (int i = 0; i < 100000; i++)
            {
                minHeap.Add(new HeapData(i));
            }
            Stopwatch stopWatch = new Stopwatch();
            StringBuilder file = new StringBuilder();
            int x = 10;
            stopWatch.Start();

            for (int i = 0; i < 100000; i++)
            {
                minHeap.Contains(new HeapData(i));
                if ((i + 1) % (10 * x) == 0)
                {
                    file.AppendLine($"{ i + 1 }: { stopWatch.Elapsed.TotalMilliseconds }ms");

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

            stopWatch.Stop();
            File.WriteAllText(path, file.ToString());
        }

        [Fact]
        public void Remove_TimerTest()
        {
            BinaryMinHeap<HeapData> minHeap = new BinaryMinHeap<HeapData>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\BinaryMinHeapRemoveTest.txt";
            File.Delete(path);

            for (int i = 0; i < 100000; i++)
            {
                minHeap.Add(new HeapData(i));
            }
            Stopwatch stopWatch = new Stopwatch();
            StringBuilder file = new StringBuilder();
            int x = 10;
            stopWatch.Start();

            for (int i = 0; i < 100000; i++)
            {
                minHeap.Extract();
                if ((i + 1) % (10 * x) == 0)
                {
                    file.AppendLine($"{ i + 1 }: { stopWatch.Elapsed.TotalMilliseconds }ms");
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

            stopWatch.Stop();
            File.WriteAllText(path, file.ToString());
        }
    }
}