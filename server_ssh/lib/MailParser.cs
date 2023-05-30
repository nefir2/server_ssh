using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace server_ssh.lib
{
	static public class MailParser
	{
		/// <summary>
		/// <see cref="string"/> 1 is name of file.<br/>
		/// <see cref="string"/> 2 is object in this file.
		/// </summary>
		public static Dictionary<string, string> Objects { get; private set; }
		static MailParser() => Objects = new Dictionary<string, string>();
		public static string Parse(string pathOfMaildir)
		{
			return pathOfMaildir;
		}
		public static string GetFileAsString(string pathToFile)
		{
			string file = "";
			try
			{
				using (FileStream sf = new FileStream(pathToFile, FileMode.Open))
				{
					byte[] buffer = new byte[sf.Length];
					sf.Read(buffer);
					file = Encoding.Default.GetString(buffer);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
			if (file == "") throw new Exception("file isn't parsed.");
			return file;
		}
		private static void SetObjects()
		{

		}
	}
}