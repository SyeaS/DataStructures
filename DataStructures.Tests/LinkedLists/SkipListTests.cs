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
    public class SkipListTests
    {
        [Theory]
        [InlineData(5, 20, 10, 70, 40, 90, 100, 25, 64, 95)]
        [InlineData(-100, -20, -50, 20, 30, 10, 40, 2, 8, 15, 60)]
        public void AddTest(params int[] values)
        {
            SkipList<int> randomizedSkipList = new SkipList<int>(values);

            foreach (int item in randomizedSkipList)
            {
                Assert.True(randomizedSkipList.Contains(item), $"Item: { item } wasn't added!");
            }

            /*foreach (int item in randomizedSkipList.GetLevels())
            {

            }*/
        }

        [Theory]
        [InlineData(5, 20, 10, 70, 40, 90, 100, 25, 64, 95)]
        [InlineData(-100, -20, -50, 20, 30, 10, 40, 2, 8, 15, 60)]
        public void RemoveTest(params int[] values)
        {
            SkipList<int> randomizedSkipList = new SkipList<int>(values);
            Random random = new Random();
            List<int> valuesList = new List<int>(values);

            for (int i = randomizedSkipList.Count - 1; i >= 0; i--)
            {
                int number = random.Next(i);

                randomizedSkipList.Remove(valuesList[number]);

                Assert.False(randomizedSkipList.Contains(valuesList[number]), $"Item { valuesList[number] } wasn't deleted!");
                valuesList.Remove(valuesList[number]);
            }
        }

        [Theory]
        [InlineData(5, 20, 10, 70, 40, 90, 100, 25, 64, 95)]
        [InlineData(-100, -20, -50, 20, 30, 10, 40, 2, 8, 15, 60)]
        public void SearchTest(params int[] values)
        {
            SkipList<int> randomizedSkipList = new SkipList<int>(values);
            int index = 0;
            Array.Sort(values);

            foreach (int item in randomizedSkipList)
            {
                Assert.True(item == values[index++], $"{ item }");
            }
        }

        [Fact]
        public void Add_TimerTest()
        {
            SkipList<int> randomizedSkipList = new SkipList<int>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\SkipListAddTest.txt";
            File.Delete(path);
            StringBuilder fileBuilder = new StringBuilder();
            DateTime startTime = DateTime.Now;

            int x = 10;

            for (int i = 0; i < 100000; i++)
            {
                randomizedSkipList.Add(i);
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

            try
            {
                StringBuilder builder = new StringBuilder();
                IEnumerable<int> lvls = randomizedSkipList.GetLevels();
                int[] levels = new int[lvls.Count()];
                string levelpath = @"D:\DefaultPrograms\Programs\C#\skiplistlevel.txt";

                if (File.Exists(levelpath))
                {
                    builder.AppendLine();
                    builder.AppendLine();
                }
                builder.Append($"Total levels: { levels.Length }\tTotal elements: { randomizedSkipList.Count }");

                int i = 0;
                foreach (int count in lvls)
                {
                    builder.Append($"\nLevel: { ++i }\t{ count }\t{ Math.Round(((double)count / randomizedSkipList.Count) * 100, 3) }%");
                }

                File.AppendAllText(levelpath, builder.ToString());
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                Debug.WriteLine(e.Message);
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
            SkipList<int> randomizedSkipList = new SkipList<int>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\SkipListSearchTest.txt";
            File.Delete(path);

            for (int i = 0; i < 100000; i++)
            {
                randomizedSkipList.Add(i);
            }
            DateTime startTime = DateTime.Now;
            StringBuilder file = new StringBuilder();
            int x = 10;


            for (int i = 0; i < 100000; i++)
            {
                randomizedSkipList.Contains(i);
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
            SkipList<int> randomizedSkipList = new SkipList<int>();
            string path = @"D:\DefaultPrograms\Programs\C#\DataStructures\SkipListRemoveTest.txt";
            File.Delete(path);

            for (int i = 0; i < 100000; i++)
            {
                randomizedSkipList.Add(i);
            }
            DateTime startTime = DateTime.Now;
            StringBuilder file = new StringBuilder();
            int x = 10;


            for (int i = 0; i < 100000; i++)
            {
                randomizedSkipList.Remove(i);
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