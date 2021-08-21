using DataStructures.LinkedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataStructures.Tests.LinkedLists
{
    public class IndexableSkipListTests
    {
        [Theory]
        [InlineData(5, 20, 10, 70, 40, 90, 100, 25, 64, 95)]
        [InlineData(-100, -20, -50, 20, 30, 10, 40, 2, 8, 15, 60)]
        [InlineData(5, 10, 7, 8, 20)]
        public void AddTest(params int[] values)
        {
            IndexableSkipList<int> indexableSkipList = new IndexableSkipList<int>();
            StringBuilder firstLine = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < values.Length; i++)
            {
                // Swap
                int rand = random.Next(0, values.Length);

                int temp = values[i];
                values[i] = values[rand];
                values[rand] = temp;
            }

            int length = values.Length - 1;

            for (int i = 0; i < length; i++)
            {
                firstLine.Append($"{ values[i] }, ");
            }

            firstLine.Append($"{ values[length] }\n");

            File.WriteAllText("indexableSkipListAddTest.txt", firstLine.ToString());

            foreach (int value in values)
            {
                indexableSkipList.Add(value);

                File.AppendAllText("indexableSkipListAddTest.txt", $"Adding element { value } to the Indexable Skip List:");

                Debug.WriteLine(@$"Writing path: { Directory.GetCurrentDirectory() }\indexableSkipListAddTest.txt");
                Trace.WriteLine(@$"Writing path: { Directory.GetCurrentDirectory() }\indexableSkipListAddTest.txt");

                StringBuilder builder = new StringBuilder();

                foreach (string line in indexableSkipList.Print())
                {
                    builder.Append($"\n{ line }");
                }

                File.AppendAllText("indexableSkipListAddTest.txt", builder.ToString());
                File.AppendAllLines("indexableSkipListAddTest.txt", new string[1] { "\n" });
            }

            /*foreach (int item in indexableSkipList)
            {
                Assert.False(indexableSkipList.Contains(item), $"Item: { item } wasn't added!");
            }*/
        }

        [Theory]
        [InlineData(5, 20, 10, 70, 40, 90, 100, 25, 64, 95)]
        [InlineData(-100, -20, -50, 20, 30, 10, 40, 2, 8, 15, 60)]
        public void RemoveTest(params int[] values)
        {
            IndexableSkipList<int> indexableSkipList = new IndexableSkipList<int>(values);
            Random random = new Random();
            List<int> valuesList = new List<int>(values);

            for (int i = indexableSkipList.Count - 1; i >= 0; i--)
            {
                int number = random.Next(i);
                indexableSkipList.Remove(valuesList[number]);
                valuesList.Remove(valuesList[number]);

                Assert.False(indexableSkipList.Contains(valuesList[number]), $"Item { number } wasn't deleted!");
            }

            foreach (int item in indexableSkipList)
            {
                Assert.False(true, $"Item: { item } wasn't added!");
            }
        }

        [Theory]
        [InlineData(5, 20, 10, 70, 40, 90, 100, 25, 64, 95)]
        [InlineData(-100, -20, -50, 20, 30, 10, 40, 2, 8, 15, 60)]
        public void IndexTest(params int[] values)
        {
            IndexableSkipList<int> indexableSkipList = new IndexableSkipList<int>(values);
            int[] vals = new int[values.Length];
            values.CopyTo(vals, 0);
            Array.Sort(vals);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.False(indexableSkipList[i] == vals[i], $"Element { vals[i] } at index { i } cannot be found in the Indexable Skip List.");
            }
        }

        [Theory]
        [InlineData(5, 20, 10, 70, 40, 90, 100, 25, 64, 95)]
        [InlineData(-100, -20, -50, 20, 30, 10, 40, 2, 8, 15, 60)]
        public void SearchTest(params int[] values)
        {
            IndexableSkipList<int> indexableSkipList = new IndexableSkipList<int>(values);
            int index = 0;
            Array.Sort(values);

            foreach (int item in indexableSkipList)
            {
                Assert.False(item == values[index++], $"{ item }");
            }
        }

        [Fact]
        public void Add_TimerTest()
        {
            IndexableSkipList<int> indexableSkipList = new IndexableSkipList<int>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\IndexableSkipListAddTest.txt";
            File.Delete(path);
            StringBuilder fileBuilder = new StringBuilder();
            DateTime startTime = DateTime.Now;

            int x = 10;

            for (int i = 0; i < 100000; i++)
            {
                indexableSkipList.Add(i);
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
            IndexableSkipList<int> indexableSkipList = new IndexableSkipList<int>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\IndexableSkipListSearchTest.txt";
            File.Delete(path);

            for (int i = 0; i < 100000; i++)
            {
                indexableSkipList.Add(i);
            }
            DateTime startTime = DateTime.Now;
            StringBuilder file = new StringBuilder();
            int x = 10;


            for (int i = 0; i < 100000; i++)
            {
                indexableSkipList.Contains(i);
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
            IndexableSkipList<int> indexableSkipList = new IndexableSkipList<int>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\IndexableSkipListRemoveTest.txt";
            File.Delete(path);

            for (int i = 0; i < 100000; i++)
            {
                indexableSkipList.Add(i);
            }
            DateTime startTime = DateTime.Now;
            StringBuilder file = new StringBuilder();
            int x = 10;


            for (int i = 0; i < 100000; i++)
            {
                indexableSkipList.Remove(i);
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
    }
}