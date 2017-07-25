using MySecondHand.Spider.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MySecondHand.Spider.Unit.Tests.Model
{
    public class CurrencyTest
    {
        [Fact]
        public void TryParseWithValidValueShouldReturnNewCurrency()
        {
            var currencyString = "100.01€";

            Currency outCurrency;

            Currency.TryParse(currencyString, out outCurrency);

            Assert.Equal(outCurrency.Value, 100, 01);
            Assert.Equal(outCurrency.Symbol, "€");
        }

        [Fact]
        public void TryParseWithValidValueShouldReturnNewCurrency2()
        {
            var currencyString = "100,01€";

            Currency outCurrency;

            Currency.TryParse(currencyString, out outCurrency);

            Assert.Equal(outCurrency.Value, 100, 01);
            Assert.Equal(outCurrency.Symbol, "€");
        }

        [Fact]
        public void TryParseWithValidValueShouldReturnNewCurrency3()
        {
            var currencyString = "1.100,01€";

            Currency outCurrency;

            Currency.TryParse(currencyString, out outCurrency);

            Assert.Equal(outCurrency.Value, 1100, 01);
            Assert.Equal(outCurrency.Symbol, "€");
        }

        [Fact]
        public void TryParseWithValidValueShouldReturnNewCurrency4()
        {
            var currencyString = "1100€";

            Currency outCurrency;

            Currency.TryParse(currencyString, out outCurrency);

            Assert.Equal(outCurrency.Value, 1100);
            Assert.Equal(outCurrency.Symbol, "€");
        }

        [Fact]
        public void TryParseWithWrogValueShouldReturnNull()
        {
            var currencyString = "adsfdsfasf";

            Currency outCurrency;

            Currency.TryParse(currencyString, out outCurrency);

            Assert.Equal(outCurrency.Value, default(decimal));
            Assert.True(string.IsNullOrEmpty(outCurrency.Symbol));
        }
    }
}
