using System;
using Renci.SshNet;
namespace server_ssh
{
	internal static class Program
	{
		private static void Main()
		{
			using SshClient ssh = new(new("localhost", 2222, "buntafuji", new PasswordAuthenticationMethod("buntafuji", "12345678")));
			ssh.Connect();
			while (true)
			{
				string cd = Console.ReadLine() ?? "";
				if (cd == "" || cd == string.Empty) continue;
				if (cd.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;
				if (cd.ToLower() == "clear")
				{
					Console.Clear();
					continue;
				}
				SshCommand command = ssh.RunCommand(cd);
				if (command.ExitStatus == 0) Console.WriteLine($"{command.Result}");
				else Console.WriteLine($"error:\n\t{command.Error}");
			}
			ssh.Disconnect();
		}
	}
}