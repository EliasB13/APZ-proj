using System;
using System.Threading.Tasks;

namespace APZ_Iot
{
	class Program
	{
		static void Main(string[] args)
		{
			Reader reader = new Reader();
			reader.LoadSettings("defaultConfig.json");
			reader.Start();

			Console.ReadLine();
		}
	}
}
