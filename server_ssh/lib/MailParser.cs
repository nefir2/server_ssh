using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server_ssh.lib
{
	internal class MailParser
	{
		public void Parse(string pathOfMailFile)
		{
			using (FileStream sf = new FileStream(pathOfMailFile, FileMode.Open))
			{

			}
		}
	}
}
