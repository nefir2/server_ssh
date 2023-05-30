using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
namespace server_ssh.lib
{
	public static class MailLib
	{
		public static string? FileName { get; private set; }
		public static string? FilePath { get; private set; }
		public static string DirectoryName { get; private set; }
		public static string ProgramPath { get; private set; }
		public static string ProgramDirectory { get; private set; }
		internal static string SCP { get; private set; }
		static MailLib()
		{
			DirectoryName = "Maildir";
			ProgramPath = Assembly.GetExecutingAssembly().Location;
			ProgramDirectory = Path.GetDirectoryName(ProgramPath) ?? String.Empty;
			SCP = Path.Combine("C:", "Program Files", "Git", "usr", "bin", "scp.exe");
		}

		public static void MakeMailDirectory()
		{
			string dir = Path.Combine(ProgramDirectory, DirectoryName);
			string dcur = Path.Combine(dir, "cur");
			string dnew = Path.Combine(dir, "new");
			string dtmp = Path.Combine(dir, "tmp");
			Directory.CreateDirectory(dir);
			Directory.CreateDirectory(dcur);
			Directory.CreateDirectory(dnew);
			Directory.CreateDirectory(dtmp);
		}
		public static void MakeMailDirectory(string path)
		{
			string dir = Path.Combine(path, DirectoryName);
			string dcur = Path.Combine(dir, "cur");
			string dnew = Path.Combine(dir, "new");
			string dtmp = Path.Combine(dir, "tmp");
			Directory.CreateDirectory(dir);
			Directory.CreateDirectory(dcur);
			Directory.CreateDirectory(dnew);
			Directory.CreateDirectory(dtmp);
		}
		/// <summary>
		/// downloads directory with all files inside from server into programs folder with same name.
		/// </summary>
		// /// <remarks>
		// /// if directory not found, throws 
		// /// </remarks>
		/// <param name="MaildirPathOnServer">path to directory in the server.</param>
		/// <param name="isRecursive">
		/// if it's <see langword="true"/>, then it can be used to download directories with files.<br/>
		/// else downloads file by <see cref="SCPDownloadMail(string, string, string, int?)"/>.
		/// </param>
		/// <param name="login">login name for the server connection.</param>
		/// <param name="ip">ip to the server.</param>
		/// <param name="port">if <see langword="null"/> using default port.</param>
		/// <returns>path to downloaded directory.</returns>
		/// <exception cref="Exception"></exception>
		public static string SCPDownloadMail(string MaildirPathOnServer, bool isRecursive, string login = "root", string ip = "localhost", int? port = null) //scp -r -P 2222 buntafuji@localhost:~/Maildir ./
		{
			try
			{
				if (isRecursive)
				{
					if (port is null) Process.Start($"{SCP} -r {login}@{ip}:\"{MaildirPathOnServer}\" \"{ProgramDirectory}\\\"");
					else Process.Start($"{SCP} -r -P {port} {login}@{ip}:\"{MaildirPathOnServer}\" \"{ProgramDirectory}\\\"");
				}
				else SCPDownloadMail(MaildirPathOnServer);
				return Path.Combine(ProgramDirectory, MaildirPathOnServer);
			}
			catch (Win32Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		/// <summary>
		/// downloads file from server into programs folder with same name.
		/// </summary>
		/// <param name="FilePathOnServer">path to file in the server.</param>
		/// <param name="login">login name for the server.</param>
		/// <param name="ip">ip to the server.</param>
		/// <param name="port">if <see langword="null"/> using default port.</param>
		/// <returns>returns path to downloaded file.</returns>
		/// <exception cref="Exception"></exception>
		public static string SCPDownloadMail(string FilePathOnServer, string login = "root", string ip = "localhost", int? port = null) //scp -P 2222 buntafuji@localhost:/home/buntafuji/Maildir/new/* ../
		{
			try
			{
				if (port is null) Process.Start($"{SCP} {login}@{ip}:\"{FilePathOnServer}\" \"{ProgramDirectory}\\\"");
				else Process.Start($"{SCP} -P {port} {login}@{ip}:\"{FilePathOnServer}\" \"{ProgramDirectory}\\\"");
				return Path.Combine(ProgramDirectory, FilePathOnServer);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
		//public static string SSHDownloadMail() { }
	}
}