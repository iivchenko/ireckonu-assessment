using Ireckonu.Application.Domain.ArticleAggregate;
using Ireckonu.Application.Domain.AudienceAggregate;
using Ireckonu.Application.Domain.Common;
using Ireckonu.Application.Domain.CurrencyAggregate;
using Ireckonu.Application.Domain.ProductAggregate;
using Ireckonu.Application.Services.Converters;
using Ireckonu.Application.Services.FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ireckonu.Infrastructure.Services.Converters
{
    public sealed class CvsToProductConverter : IConverter<IFileReader, IAsyncEnumerable<Product>>
    {
        private readonly IRepository<Article, Guid> _articleRepository;
        private readonly IRepository<Audience, Guid> _audienceRepository;
        private readonly IRepository<Currency, Guid> _currencyRepository;

        public CvsToProductConverter(
            IRepository<Article, Guid> articleRepository,
            IRepository<Audience, Guid> audienceRepository,
            IRepository<Currency, Guid> currencyRepository)
        {
            _articleRepository = articleRepository;
            _audienceRepository = audienceRepository;
            _currencyRepository = currencyRepository;
        }

        public IAsyncEnumerable<Product> Convert(IFileReader reader)
        {
            return
                reader
                    .ReadAsync()
                    .Skip(1) // Skip csv header
                    .Select(ToRaw)
                    .SelectAwait(ToProduct);
        }

        private async ValueTask<Product> ToProduct(Raw item)
        {
            var articles = await _articleRepository.FindAsync(x => x.Code == item.ArtikelCode);
            var audiences = await _audienceRepository.FindAsync(x => x.Name == item.Q1);
            var currency = await _currencyRepository.FindAsync(x => x.Name == "EUR");

            return new Product
            {
                Key = item.Key,
                ArticleId = articles.First().Id,
                AudienceId = audiences.First().Id,
                Description = item.Description,
                Color = ConvertColor(item.Color),
                Size = new Size(int.Parse(item.Size)),
                Price = new Price(currency.First().Id, int.Parse(item.Price)),
                DiscountPrice = new Price(currency.First().Id, int.Parse(item.DiscountPrice)),
                DeliveryPeriod = ConvertDelivery(item.DeliveredIn)
            };
        }

        private static Color ConvertColor(string color)
        {
            switch(color)
            {

                case "grijs":
                    return new Color(128, 128, 128);
                case "groen":
                    return new Color(0, 255, 0);
                case "wit":
                    return new Color(255, 255, 255);
                case "zwart":
                    return new Color(0, 0, 0);
                case "bruin":
                    return new Color(75, 0, 0);
                case "beige":
                    return new Color(200, 175, 160);
                case "rood":
                    return new Color(255, 0, 0);
                case "blauw":
                    return new Color(0, 0, 255);
                default:
                    throw new Exception($"Unknown color '{color}'!");
            }
        }

        private static DeliveryPeriod ConvertDelivery(string period)
        {
            var days =
                period
                    .Replace(" werkdagen", "")
                    .Split('-')
                    .Select(x => int.Parse(x))
                    .ToArray();

            return new DeliveryPeriod(days[0], days[1]);
        }

        private static Raw ToRaw(string line)
        {
            var items = line.Split(',');

            return new Raw
            {
                Key = items[0],
                ArtikelCode = items[1],
                ColorCode = items[2],
                Description = items[3],
                Price = items[4],
                DiscountPrice = items[5],
                DeliveredIn = items[6],
                Q1 = items[7],
                Size = items[8],
                Color = items[9]
            };
        }

        private sealed class Raw
        {
            public string Key { get; set; }
            public string ArtikelCode { get; set; }
            public string ColorCode { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public string DiscountPrice { get; set; }
            public string DeliveredIn { get; set; }
            public string Q1 { get; set; }
            public string Size { get; set; }
            public string Color { get; set; }
        }
    }
}
