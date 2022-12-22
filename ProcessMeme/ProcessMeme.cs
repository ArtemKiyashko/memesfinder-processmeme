using MemesFinderTextProcessor.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ProcessMeme.Interfaces.SearchEngine;
using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ProcessMeme
{
    public class ProcessMeme
    {
        private readonly ILogger<ProcessMeme> _logger;
        private readonly IGoogleSearchEngineManager _googleSearchEngineManager;
        private readonly ITelegramBotClient _telegramBotClient;

        public ProcessMeme(ILogger<ProcessMeme> log, IGoogleSearchEngineManager googleSearchEngineManager, ITelegramBotClient telegramBotClient)
        {
            _logger = log;
            _googleSearchEngineManager = googleSearchEngineManager;
            _telegramBotClient = telegramBotClient;
        }

        [FunctionName("ProcessMeme")]
        public async Task Run([ServiceBusTrigger("keywordmessages", "memeprocessor", Connection = "ServiceBusOptions")] TgMessageModel tgMessageModel)
        {
            //keyword search
            var memeLink = await _googleSearchEngineManager.GetMemeLinkAsync(tgMessageModel.Keyword);
            //check if meme link is not null or empty
            if (string.IsNullOrEmpty(memeLink))
            {
                _logger.LogError("Link is null or empty");
                return;
            }

            //try send meme
            try
            {
                await _telegramBotClient.SendPhotoAsync(
                    chatId: tgMessageModel.Message.Chat.Id,
                    replyToMessageId: tgMessageModel.Message.MessageId,
                    photo: memeLink);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cant send meme with URL: {memeLink}");
                return;
            }
            Console.WriteLine(memeLink);
        }
    }
}

