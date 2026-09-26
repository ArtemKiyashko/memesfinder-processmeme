using MemesFinderTextProcessor.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Polly;
using ProcessMeme.Interfaces.SearchEngine;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace ProcessMeme
{
    public class ProcessMeme
    {
        private readonly ILogger<ProcessMeme> _logger;
        private readonly IGoogleSearchEngineManager _googleSearchEngineManager;
        private readonly ITelegramBotClient _telegramBotClient;

        public ProcessMeme(
            ILogger<ProcessMeme> log,
            IGoogleSearchEngineManager googleSearchEngineManager,
            ITelegramBotClient telegramBotClient)
        {
            _logger = log;
            _googleSearchEngineManager = googleSearchEngineManager;
            _telegramBotClient = telegramBotClient;
        }

        [Function("ProcessMeme")]
        public async Task Run([ServiceBusTrigger("keywordmessages", "memeprocessor", Connection = "ServiceBusOptions")] TgMessageModel tgMessageModel)
        {
            try
            {
                //keyword search
                await _googleSearchEngineManager.SearchMemesAsync(tgMessageModel.Keyword);
                var memeUrl = _googleSearchEngineManager.GetNextRandomMemeUrl();
                if (string.IsNullOrWhiteSpace(memeUrl))
                {
                    _logger.LogWarning("No usable meme image was found for query {Query}; skipping Telegram reply.", tgMessageModel.Keyword);
                    return;
                }

                await Policy
                    .Handle<ApiRequestException>()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                    .ExecuteAsync(async () => {
                        //try send meme
                        await _telegramBotClient.SendPhotoAsync(
                            chatId: tgMessageModel.Message.Chat.Id,
                            replyToMessageId: tgMessageModel.Message.MessageId,
                            photo: InputFile.FromString(memeUrl));
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cant send any meme by keyword: {tgMessageModel.Keyword}");
                return;
            }
        }
    }
}

