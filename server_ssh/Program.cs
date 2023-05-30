using System;
using System.Diagnostics;

using Renci.SshNet;
using server_ssh.lib;
namespace server_ssh
{
	internal static class Program
	{
		private static void Main()
		{
			string file = MailLib.DownloadMail("~/Maildir", true, "buntafuji", port: 2222);
			Process.Start($"explorer {file}");
			Console.WriteLine();
		}
		private static void ConsoleSSH()
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