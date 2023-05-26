using System;
using Renci.SshNet;  

namespace server_ssh
{
	internal static class Program
	{
		private static void Main()
		{
			ConnectionInfo conInfo = new ConnectionInfo(
				"localhost",
				2222,
				"buntafuji",
				new PasswordAuthenticationMethod("buntafuji", "12345678")
			);
			SshClient ssh = new SshClient(conInfo);
			ssh.Connect();
			SshCommand command = ssh.RunCommand("pwd; ls -lsa"); //вывод текущего пути к текущей папке;
																 //вывод содержимого папки

			//если в Linux команда возвращает 0 - команда успешно выполнена.
			if (command.ExitStatus == 0) Console.WriteLine($"result:\n{command.Result}"); 
			else Console.WriteLine($"error:\n\t{command.Error}");
			ssh.Disconnect();
			ssh.Dispose();
		}
	}
}