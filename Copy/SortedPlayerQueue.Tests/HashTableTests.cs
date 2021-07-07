using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SortedPlayerQueue.Tests
{
    public class HashTableTests
    {
        static HashTable<PlayerData, float> hashTable = CreateQueue();

        private static int Hash(float key, int size)
        {
            int value = (int)key / 10;

            if (value > 9)
            {
                value = 9;
            }
            else if (value < 0)
            {
                value = 0;
            }

            return value;
        }

        private static HashTable<PlayerData, float> CreateQueue()
        {
            return new HashTable<PlayerData, float>(10,
                new HashTable<PlayerData, float>.HashingAlgorithm(Hash));
        }

        [Theory]
        [InlineData(55.01252f, 35.012f, 00.11f, 11.00f, 9.99f, 99.99f, 100.00f, 50.00f, 40.00f, 11.11f, 10.11f, 25.623542523f, 71.42f, 72.567f, 21.667f, 74.0543f)]
        public void AddShouldWork(params float[] keys)
        {
            List<PlayerData> players = new List<PlayerData>();
            int number = keys.Length;

            for (int i = 0; i < number; i++)
            {
                players.Add(GenerateRandomPlayerData());
            }

            for (int i = 0; i < number; i++)
            {
                hashTable.Add(players[i], keys[i]);
            }
        }

        [Theory]
        [InlineData(55.01252f, 35.012f, 00.11f, 11.00f, 9.99f, 99.99f, 100.00f, 50.00f, 40.00f, 11.11f, 10.11f, 25.623542523f, 71.42f, 72.567f, 21.667f, 74.0543f)]
        public void AddShouldntWork(params float[] keys)
        {
            List<PlayerData> playerDatas = new List<PlayerData>();
            HashTable<PlayerData, float> table = CreateQueue();

            for (int i = 0; i < keys.Length; i++)
            {
                playerDatas.Add(GenerateRandomPlayerData());
                table.Add(playerDatas[i], keys[i]);

                Assert.Throws<DuplicateListElementException>(() =>
                table.Add(playerDatas[i], keys[i]));
            }
        }

        private PlayerData GenerateRandomPlayerData()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[(short.MaxValue / 8) + 1];
            rng.GetBytes(bytes);
            long ID = 0;
            float priority = 0;

            foreach (byte item in bytes)
            {
                ID += item;
            }

            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            bytes = new byte[1024];
            double bytesMaxValue = bytes.Length * 255;
            randomNumberGenerator.GetBytes(bytes);

            foreach (byte item in bytes)
            {
                priority += item;
            }

            priority = (priority / (float)(bytesMaxValue)) * 100.0f;

            return new PlayerData(ID, priority);
        }
    }

    public record PlayerData(long ID, float Priority) : IComparable<PlayerData>
    {
        public int CompareTo(PlayerData other)
        {
            int count = 0;

            if (this.ID == other.ID && this.Priority == other.Priority)
            {
                return count;
            }

            if (this.ID < other.ID)
            {
                count++;
            }
            else
            {
                count--;
            }

            if (this.Priority < other.Priority)
            {
                count++;
            }
            else
            {
                count--;
            }

            return count;
        }
    }
}