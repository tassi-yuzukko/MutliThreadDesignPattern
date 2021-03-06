﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _05_Producer_Consumer
{
	/// <summary>
	/// Producer
	/// </summary>
    class MakerThread
    {
		readonly Random _random;
		readonly Table _table;

		static object _lock = new object();
		static int _id = 0;

		public MakerThread(string name, Table table, int seed)
		{
			Thread.CurrentThread.Name = name;
			_table = table;
			_random = new Random(seed);
		}

		public void Run(CancellationToken token)
		{
			try
			{
				while (true)
				{
					Thread.Sleep(_random.Next(1000));

					string cake = "[ Cake No. " + NextId() + " by " + Thread.CurrentThread.Name + " ]";
					_table.Put(cake, token);
				}
			}
			catch (OperationCanceledException)
			{
				Console.WriteLine("---- PROCESS ABORT ---- {0}", DateTime.Now);
				return;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		static int NextId()
		{
			lock (_lock)
			{
				return _id++;
			}
		}
    }
}
