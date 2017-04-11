using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Main
{
    [TestFixture]
    public class MainTests
    {
        [Test]
        public void BuyingOneBook_TotalIs_8()
        {
            var basket = new Basket();
            basket.BuyBook(0);

            var total = basket.Total();

            Assert.That(total, Is.EqualTo(8));

        }
        [Test]
        public void BuyingTwoBooks_TotalIs_16()
        {
            Basket basket = new Basket();
            basket.BuyBook(0);
            basket.BuyBook(0);
            var total = basket.Total();

            Assert.That(total, Is.EqualTo(16));
        }

        [Test]
        public void Buying_2_DifferentBooks_Total_Is_With_Discount1()
        {
            var basket = new Basket();
            basket.BuyBook(0);
            basket.BuyBook(1);
            decimal expected = 16 * 0.95m;
            Assert.That(expected, Is.EqualTo(basket.Total()));
        }

        [Test]
        public void Buying_3_DifferentBooks_Total_Is_With_Discount2()
        {
            var basket = new Basket();
            basket.BuyBook(0);
            basket.BuyBook(1);
            basket.BuyBook(2);
            decimal expected = 24 * 0.9m;
            Assert.That(expected, Is.EqualTo(basket.Total()));
        }
    }

    public class Basket
    {
        private Dictionary<int, decimal> Discounts = new Dictionary<int, decimal>()
        {
            {2, 0.95m },
            {3, 0.90m }
        };

        private readonly int[] _books;
        public Basket(int[] books)
        {
            _books = books;
        }

        public Basket()
        {
            _books = new[] {0, 0, 0, 0, 0};
        }


        public decimal Total()
        {
            var total = 0m;

            foreach (var i in _books)
            {
                total += i * 8;
            }

            total = ApplyDiscounts(total);

            return total;
        }

        private decimal ApplyDiscounts(decimal total)
        {
            var countDiff = _books.Count(x => x > 0);
            if (countDiff > 1)
            {
                return total * Discounts[countDiff];
            }
            return total;
        }

        public void BuyBook(int i)
        {
            _books[i]++;
        }
    }
}