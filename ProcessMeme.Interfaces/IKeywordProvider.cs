using System;
namespace ProcessMeme.Interfaces
{
	public interface IKeywordProvider
	{
		public string GetKeyword(string message);
	}
}

