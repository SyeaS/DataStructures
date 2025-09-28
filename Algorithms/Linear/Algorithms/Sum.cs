using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.Linear.Algorithms
{
	public static partial class LinearAlgorithms
	{
		public static int Sum(int[] array)
		{
			// This is the implementation of the sum algorithm
			// Its an example algorithm, the rest of the
			// algorithms should be written in this order
			// (i.e. create Algorithms folder, then put the Algorithm name as the
			// file name and have a partial class outside the Algorithms folder!)

			int sum = 0;
			for (int i = 0; i < array.Length; i++)
			{
				sum += array[i];
			}
			return sum;
		}
	}
}