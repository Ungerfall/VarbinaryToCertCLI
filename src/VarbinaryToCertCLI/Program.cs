using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VarbinaryToCertCLI
{
	class Program
	{
		public static void Main(string[] args)
		{
			var helpLiterals = new[] {"?", "/?", "-H", "--help"};
			if (args.Length == 0 || string.IsNullOrWhiteSpace(args[0]) || helpLiterals.Contains(args[0]))
			{
				Console.WriteLine(
					$"Usage: *.exe varbinary sequence{Environment.NewLine}Example: varbinary-to-cert.exe 0x123F43E");
				return;
			}

			var arg = args[0];
			var byteList = new List<byte>();
			try
			{
				var hexPart = arg.Substring(2);
				for (int i = 0; i < hexPart.Length / 2; i++)
				{
					var hexNumber = hexPart.Substring(i * 2, 2);
					byteList.Add((byte) Convert.ToInt32(hexNumber, 16));
				}

				using (var ms = new MemoryStream(byteList.ToArray()))
				using (var fileStream = File.Create("certificate.pfx"))
				{
					ms.WriteTo(fileStream);
				}

				Console.WriteLine("Varbinary certificate has been successfully imported to certificate.pfx file"
					+ $"{Environment.NewLine}Press any key to exit...");
				Console.ReadKey();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Import failed: {ex.Message}");
			}
		}
	}
}
