using System;
using System.Diagnostics;
using System.IO;

using Renci.SshNet;
using server_ssh.lib;
namespace server_ssh
{
	internal static class Program
	{
		private static string login = "buntafuji";
		private static string passwd = "12345678";
		private static string ip = "localhost";
		private static int port = 2222;
		private static void Main()
		{
			//string file = MailLib.SCPDownloadMail("~/Maildir", true, "buntafuji", port: 2222);
			//Process.Start($"explorer {file}");
			//Console.WriteLine($"file exists ({MailLib.SCP}): {File.Exists(MailLib.SCP)}");
			//Process.Start($"{MailLib.SCP} -r -P 2222 buntafuji@localhost:\"~/Maildir\" \"./\"");
			//Console.WriteLine();

			ScpClient scp = new ScpClient(ip, login, passwd);
			scp.Download();//!!!!!
		}
		private static void ConsoleSSH()
		{
			using SshClient ssh = new(new(ip, 2222, login, new PasswordAuthenticationMethod(login, passwd)));
			ssh.Connect();
			while (true)
			{
				string cd = Console.ReadLine() ?? "";
				if (cd == "") continue;
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