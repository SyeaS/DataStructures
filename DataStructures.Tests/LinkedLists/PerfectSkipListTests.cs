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
    public class PerfectSkipListTests
    {
        [Theory]
        [InlineData(5, 20, 10, 70, 40, 90, 100, 25, 64, 95)]
        [InlineData(-100, -20, -50, 20, 30, 10, 40, 2, 8, 15, 60)]
        public void AddTest(params int[] values)
        {
            PerfectSkipList<int> perfectSkipList = new PerfectSkipList<int>(values);

            foreach (int item in perfectSkipList)
            {
                Assert.False(perfectSkipList.Contains(item), $"Item: { item } wasn't added!");
            }
        }

        [Theory]
        [InlineData(5, 20, 10, 70, 40, 90, 100, 25, 64, 95)]
        [InlineData(-100, -20, -50, 20, 30, 10, 40, 2, 8, 15, 60)]
        public void RemoveTest(params int[] values)
        {
            PerfectSkipList<int> perfectSkipList = new PerfectSkipList<int>(values);
            Random random = new Random();
            List<int> valuesList = new List<int>(values);

            for (int i = perfectSkipList.Count - 1; i >= 0; i--)
            {
                int number = random.Next(i);
                perfectSkipList.Remove(valuesList[number]);
                valuesList.Remove(valuesList[number]);

                Assert.False(perfectSkipList.Contains(valuesList[number]), $"Item { number } wasn't deleted!");
            }

            foreach (int item in perfectSkipList)
            {
                Assert.False(true, $"Item: { item } wasn't added!");
            }
        }

        [Theory]
        [InlineData(5, 20, 10, 70, 40, 90, 100, 25, 64, 95)]
        [InlineData(-100, -20, -50, 20, 30, 10, 40, 2, 8, 15, 60)]
        public void SearchTest(params int[] values)
        {
            PerfectSkipList<int> perfectSkipList = new PerfectSkipList<int>(values);
            int index = 0;
            Array.Sort(values);

            foreach (int item in perfectSkipList)
            {
                Assert.False(item == values[index++], $"{ item }");
            }
        }

        [Fact]
        public void Add_TimerTest()
        {
            PerfectSkipList<int> perfectSkipList = new PerfectSkipList<int>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\PerfectSkipListAddTest.txt";
            File.Delete(path);
            StringBuilder fileBuilder = new StringBuilder();
            DateTime startTime = DateTime.Now;

            int x = 10;

            for (int i = 0; i < 100000; i++)
            {
                perfectSkipList.Add(i);
                if ((i + 1) % (10 * x) == 0)
                {
                    if (x == 10)
                    {
                        fileBuilder.AppendLine($"{ i + 1 }: { (startTime - DateTime.Now).TotalMilliseconds }ms");
                        x += 90;
                    }
                    else
                    {
                        fileBuilder.AppendLine($"\n{ i + 1 }: { (startTime - DateTime.Now).TotalMilliseconds }ms");
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
            PerfectSkipList<int> perfectSkipList = new PerfectSkipList<int>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\PerfectSkipListSearchTest.txt";
            File.Delete(path);

            for (int i = 0; i < 100000; i++)
            {
                perfectSkipList.Add(i);
            }
            DateTime startTime = DateTime.Now;
            StringBuilder file = new StringBuilder();
            int x = 10;


            for (int i = 0; i < 100000; i++)
            {
                perfectSkipList.Contains(i);
                if ((i + 1) % (10 * x) == 0)
                {
                    file.AppendLine($"{ i + 1 }: { (startTime - DateTime.Now).TotalMilliseconds }ms");

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
            PerfectSkipList<int> perfectSkipList = new PerfectSkipList<int>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\PerfectSkipListRemoveTest.txt";
            File.Delete(path);

            for (int i = 0; i < 100000; i++)
            {
                perfectSkipList.Add(i);
            }
            DateTime startTime = DateTime.Now;
            StringBuilder file = new StringBuilder();
            int x = 10;


            for (int i = 0; i < 100000; i++)
            {
                perfectSkipList.Remove(i);
                if ((i + 1) % (10 * x) == 0)
                {
                    file.AppendLine($"{ i + 1 }: { (startTime - DateTime.Now).TotalMilliseconds }ms");
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