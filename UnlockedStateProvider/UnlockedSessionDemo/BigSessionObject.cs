using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace UnlockedSessionDemo
{
	[Serializable]
	public class BigSessionObject
	{
		//private const int MAX_VALUE = 1000000;

		public BigSessionObject()
		{
			Bytes = BigBytez.Bytez;
			Content = ContentText.LoremIpsum;
		}

		public byte[] Bytes { get; set; }

		public string Content { get; set; }


	}
}