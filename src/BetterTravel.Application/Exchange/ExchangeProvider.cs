using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BetterTravel.Application.Exchange.Abstractions;
using BetterTravel.Application.Exchange.Responses;
using BetterTravel.DataAccess.Cache;
using BetterTravel.DataAccess.Enums;
using BetterTravel.DataAccess.Redis.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static System.Net.SecurityProtocolType;

namespace BetterTravel.Application.Exchange
{
    public sealed class ExchangeProvider : IExchangeProvider 
    {
        private static readonly CurrencyType[] SupportedCurrencies =
        {
            CurrencyType.USD, 
            CurrencyType.EUR
        };
        
        private const string DateFormatString = "dd.MM.yyyy";
        private const string ServiceUrl = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";

        private readonly ICurrencyRateRepository _cacheRepository;
        private readonly ILogger<ExchangeProvider> _logger;

        public ExchangeProvider(
            ICurrencyRateRepository cacheRepository, 
            ILogger<ExchangeProvider> logger) =>
            (_cacheRepository, _logger) = (cacheRepository, logger);

        public async Task<Result<List<CurrencyRate>>> GetExchangeAsync(bool cached)
        {
            if (cached)
            {
                var (success, _, val, err) = 
                    await _cacheRepository.GetValuesAsync();
                
                if (success && val.Any())
                    return Result.Success(val);
                    
                _logger.LogError(err);
            }
            
            SecurityProtocol = Tls | Tls11 | Tls12;
            
            var json = await new HttpClient().GetStringAsync(ServiceUrl);
            var settings = new JsonSerializerSettings {DateFormatString = DateFormatString};
            var currencies = JsonConvert.DeserializeObject<List<ExchangeResponse>>(json, settings)?
                .Where(response => SupportedCurrencies.Contains((CurrencyType) response.CurrencyNumber))
                .Select(response => new CurrencyRate
                { 
                    Type = (CurrencyType) response.CurrencyNumber,
                    Rate = response.Rate,
                    ExchangeDate = DateTime.Today
                })
                .ToList();

            var setResult = await _cacheRepository.SetValuesAsync(currencies);

            return Result
                .Combine(setResult)
                .Map(() => currencies);
        }

        public object SecurityProtocol { get; set; }
    }
}