using System;
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
		static MailLib()
		{
			DirectoryName = "Maildir";
			ProgramPath = Assembly.GetExecutingAssembly().Location;
			ProgramDirectory = Path.GetDirectoryName(ProgramPath) ?? String.Empty;
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
		public static void GetMail(string MaildirPath, bool isRecursive, string login = "root", string ip = "localhost", int? port = null)
		{
			if (isRecursive)
			{
				if (port is null) Process.Start($"scp -r {login}@{ip}:{MaildirPath} {ProgramDirectory}");
				else Process.Start($"scp -r -P {port} {login}@{ip}:{MaildirPath} {ProgramDirectory}");
			}
			else GetMail(MaildirPath);
		}
		public static void GetMail(string FilePath, string login = "root", string ip = "localhost", int? port = null)
		{
			if (port is null) Process.Start($"scp {login}@{ip}:{FilePath} {ProgramDirectory}");
			else Process.Start($"scp -P {port} {login}@{ip}:{FilePath} {ProgramDirectory}");
		}
		public static void ParseNamesInDir()
		{

		}
	}
}