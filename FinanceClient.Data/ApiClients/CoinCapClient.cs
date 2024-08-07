﻿using System.Globalization;
using FinanceClient.Data.Contracts;
using FinanceClient.Data.Models;

namespace FinanceClient.Data.Services
{
    public class CoinCapClient : IApiClient
    {
        private readonly IFetchService _fetchService;
        private readonly string _baseUrl;

        public CoinCapClient(IFetchService fetchService, string baseUrl = "https://api.coincap.io/v2/assets/")
        {
            _fetchService = fetchService;
            _baseUrl = baseUrl;
        }

        public async Task<CurrencyModel[]> GetFullCurrenciesInfoAsync()
        {
            var currencies = await GetTopCurrenciesAsync();
            var tasks = currencies.Select(GetModel);
            return await Task.WhenAll(tasks);
        }

        public async Task<Dictionary<DateTime, double>> GetHistoryAsync(string name)
        {
            var response = await _fetchService.FetchDataAsync<HistoryResponse>($"{_baseUrl}{name}/history?interval=d1");
            return FormatHistoryData(response.Data);
        }

        public async Task<Dictionary<string, double>> GetMarketsAsync(string name)
        {
            var response = await _fetchService.FetchDataAsync<MarketListResponse>($"{_baseUrl}{name}/markets");
            return FormatMarketsData(response.Data);
        }

        public async Task<CurrencyModel> SearchAsync(string name)
        {
            var response = await _fetchService.FetchDataAsync<CurrencyResponse>($"{_baseUrl}{name}/");
            return await GetModel(response.Data);
        }

        public async Task<CurrencyDTO[]> GetTopCurrenciesAsync()
        {
            var response = await _fetchService.FetchDataAsync<CurrencyListResponse>(_baseUrl);
            return response != null ? 
                FormatCurrenciesData(response.Data) : 
                Array.Empty<CurrencyDTO>();
        }

        public async Task<Dictionary<string, double>> GetCurrenciesCoastsAsync()
        {
            var response = await _fetchService.FetchDataAsync<CurrencyListResponse>(_baseUrl);
            return FormatCurrenciesCoast(response.Data);
        }

        private static Dictionary<DateTime, double> FormatHistoryData(DayDTO[] history)
        {
            return history.ToDictionary(
                data => DateTimeOffset.FromUnixTimeMilliseconds(data.Time).DateTime,
                data => Math.Round(double.Parse(data.PriceUsd, CultureInfo.InvariantCulture), 2)
            );
        }

        private static Dictionary<string, double> FormatMarketsData(MarketDTO[] markets)
        {
            return markets
                .GroupBy(market => market.ExchangeId)
                .Select(group => group.First()) // Take the first market for each unique ExchangeId
                .OrderByDescending(market => double.Parse(market.PriceUsd, CultureInfo.InvariantCulture))
                .Take(5)
                .ToDictionary(
                    market => market.ExchangeId,
                    market => Math.Round(double.Parse(market.PriceUsd, CultureInfo.InvariantCulture), 2)
                );
        }

        private static CurrencyDTO[] FormatCurrenciesData(CurrencyDTO[] currencies)
        {
            return currencies
                .OrderByDescending(currency => double.Parse(currency.PriceUsd, CultureInfo.InvariantCulture))
                .Take(10)
                .ToArray();
        }

        private static Dictionary<string, double> FormatCurrenciesCoast(CurrencyDTO[] currencies)
        {
            return currencies
                .OrderByDescending(currency => double.Parse(currency.PriceUsd, CultureInfo.InvariantCulture))
                .ToDictionary(
                    currency => currency.Id,
                    currency => Math.Round(double.Parse(currency.PriceUsd, CultureInfo.InvariantCulture), 2)
                );
        }

        private async Task<CurrencyModel> GetModel(CurrencyDTO currencyDto)
        {
            return currencyDto != null ? new CurrencyModel()
            {
                Id = currencyDto.Id,
                Symbol = currencyDto.Symbol,
                Name = currencyDto.Name,
                Price = Math.Round(double.Parse(currencyDto.PriceUsd, CultureInfo.InvariantCulture), 2),
                ChangePercent = Math.Round(double.Parse(currencyDto.ChangePercent24Hr, CultureInfo.InvariantCulture), 2),
                Link = currencyDto.Explorer,
                History = await GetHistoryAsync(currencyDto.Id),
                Markets = await GetMarketsAsync(currencyDto.Id),
            } : new CurrencyModel();
        }
    }
}
