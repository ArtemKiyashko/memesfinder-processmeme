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

        [FunctionName("ProcessMeme")]
        public async Task Run([ServiceBusTrigger("keywordmessages", "memeprocessor", Connection = "ServiceBusOptions")] TgMessageModel tgMessageModel)
        {
            try
            {
                //keyword search
                await _googleSearchEngineManager.SearchMemesAsync(tgMessageModel.Keyword);

                await Policy
                    .Handle<ApiRequestException>()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                    .ExecuteAsync(async () => {
                        //try send meme
                        await _telegramBotClient.SendPhotoAsync(
                            chatId: tgMessageModel.Message.Chat.Id,
                            replyToMessageId: tgMessageModel.Message.MessageId,
                            photo: InputFile.FromString(_googleSearchEngineManager.GetNextRandomMemeUrl()));
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

