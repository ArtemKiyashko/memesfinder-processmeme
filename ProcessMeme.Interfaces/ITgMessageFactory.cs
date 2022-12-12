using System;
using Telegram.Bot.Types;

namespace ProcessMeme.Interfaces
{
	public interface ITgMessageFactory
	{
		public Message GetMessage(Update incomingUpdate);
	}
}

