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

            Assert.Equal(outCurrency.Value, 100,01);
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
