using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiTower
{
    public class Program
    {
		public static void Main(String[] args)
		{
			Console.WriteLine("Enter number of disks: ");
			var numberOfDisks = Console.ReadLine();

			if (!int.TryParse(numberOfDisks, out var numberOfDisksInt))
			{
				Console.WriteLine("Please enter a valid number.");
				Main(args);
				return;
			}

			if(numberOfDisksInt > 11 || numberOfDisksInt <= 0)
			{
				Console.WriteLine("Please enter a number between 1 and 10 inclusively.");
				Main(args);
				return;
			}

			var obj = new HanoiTower(numberOfDisksInt, 'A', 'C', 'B');
			obj.Execute();
			Console.ReadKey();
		}
	}
}
