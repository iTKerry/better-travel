using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BetterTravel.DataAccess.Redis.Base;
using BetterTravel.Domain.Cache;
using BetterTravel.Domain.Enums;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using static System.Net.SecurityProtocolType;
using static System.Net.ServicePointManager;

namespace BetterTravel.CurrencyFetcher.Function
{
    public sealed class ExchangeProvider : IExchangeProvider 
    {
        private static readonly CurrencyType[] SupportedCurrencies =
        {
            CurrencyType.USD, 
            CurrencyType.EUR
        };
        
        private const string ExchangeKey = "exchange";
        private const string DateFormatString = "dd.MM.yyyy";
        private const string ServiceUrl = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";

        private readonly CacheProvider<List<CurrencyRate>> _cacheProvider;

        public ExchangeProvider(CacheProvider<List<CurrencyRate>> cacheProvider) => 
            _cacheProvider = cacheProvider;

        public async Task<Result<List<CurrencyRate>>> GetExchangeAsync(bool cached = false)
        {
            if (cached)
            {
                var (success, _, val, err) = 
                    await _cacheProvider.GetValueAsync(ExchangeKey);
                
                if (success && val.Any())
                    return Result.Success(val);
                    
                Console.WriteLine(err);
            }
            
            SecurityProtocol = Tls | Tls11 | Tls12;
            
            var json = await new HttpClient().GetStringAsync(ServiceUrl);
            var settings = new JsonSerializerSettings {DateFormatString = DateFormatString};
            var currencies = JsonConvert.DeserializeObject<List<CurrencyResponse>>(json, settings)?
                .Where(response => SupportedCurrencies.Contains((CurrencyType) response.CurrencyNumber))
                .Select(response => new CurrencyRate
                { 
                    Type = (CurrencyType) response.CurrencyNumber,
                    Rate = response.Rate,
                    ExchangeDate = DateTime.Today
                })
                .ToList();

            var setResult =
                await _cacheProvider.SetValueAsync(ExchangeKey, currencies, TimeSpan.FromMinutes(1));

            return Result
                .Combine(setResult)
                .Map(() => currencies);
        }
    }

    public interface IExchangeProvider
    {
        public Task<Result<List<CurrencyRate>>> GetExchangeAsync(bool cached);
    }
}