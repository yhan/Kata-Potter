namespace PotterKata
{
    using System.Collections.Generic;
    using System.Linq;

    using NFluent;

    using NUnit.Framework;

    public class PotterShould
    {
        [Test]
        public void Return_16_when_buy_2_copy_of_the_same_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, 2);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(16);
        }

        [Test]
        public void Return_16_when_buy_2_different_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, numberOfCopies: 1);
            shoppingBasket.Add(2, numberOfCopies: 1);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(16 * 0.95);
        }

        [Test]
        public void Return_16_when_buy_2_same_book_and_a_different_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, numberOfCopies: 1);
            shoppingBasket.Add(2, numberOfCopies: 2);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(16 * 0.95 + 8);
        }

        [Test]
        public void Return_21_6_when_buy_3_different_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, numberOfCopies: 1);
            shoppingBasket.Add(2, numberOfCopies: 1);
            shoppingBasket.Add(3, numberOfCopies: 1);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(24 * 0.90);
        }

        [Test]
        public void Return_25_6_when_buy_4_different_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, numberOfCopies: 1);
            shoppingBasket.Add(2, numberOfCopies: 1);
            shoppingBasket.Add(3, numberOfCopies: 1);
            shoppingBasket.Add(4, numberOfCopies: 1);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(8 * 4 * 0.80);
        }

        [Test]
        public void Return_30_4_when_buy_2_book_1_and_2_book_2()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, numberOfCopies: 2);
            shoppingBasket.Add(2, numberOfCopies: 2);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(8 * 4 * 0.95);
        }

        [Test]
        public void Return_30_when_buy_5_different_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, numberOfCopies: 1);
            shoppingBasket.Add(2, numberOfCopies: 1);
            shoppingBasket.Add(3, numberOfCopies: 1);
            shoppingBasket.Add(4, numberOfCopies: 1);
            shoppingBasket.Add(5, numberOfCopies: 1);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(8 * 5 * 0.75);
        }

        [Test]
        public void Return_30_when_buy_52_different_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, numberOfCopies: 2);
            shoppingBasket.Add(2, numberOfCopies: 2);
            shoppingBasket.Add(3, numberOfCopies: 2);
            shoppingBasket.Add(4, numberOfCopies: 1);
            shoppingBasket.Add(5, numberOfCopies: 1);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(51.20);
        }

        [Test]
        public void Return_8_when_buy_a_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, 1);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(8);
        }
    }

    internal class ShoppingBasket
    {
        private readonly Dictionary<int, int> _books = new Dictionary<int, int>();
        private readonly Dictionary<int, double> _discounts = new Dictionary<int, double>
        {
            [1] = 1,
            [2] = 0.95,
            [3] = 0.90,
            [4] = 0.80,
            [5] = 0.75
        };

        public void Add(int bookId, int numberOfCopies)
        {
            _books.Add(bookId, numberOfCopies);
        }

        public double Price2()
        {
            double price = 0;
            int index = 0;
            int numberOfSerieAlreadyPrice = 0;
            foreach (var keyValuePair in _books.OrderBy(x => x.Value))
            {
                var numberDiferentOfBook = _books.Count - index;
                var numberOfSerie = keyValuePair.Value - numberOfSerieAlreadyPrice;
                if (numberOfSerie > 0)
                {
                    price += numberOfSerie * numberDiferentOfBook * GetDiscount(numberDiferentOfBook) * 8;
                    numberOfSerieAlreadyPrice = keyValuePair.Value;
                }
                index++;
            }
            return price;
        }

        public double Price()
        {
            var bookSets = new List<List<int>>{ new List<int>() };
            foreach (var booksKey in _books.Keys)
            {
                for (int i = 0; i < _books[booksKey]; i++)
                {
                    bool isAdded = false;
                    foreach (var bookSet in bookSets)
                    {
                        if (bookSet.Count == 4)
                        {
                            continue;
                        }
                        if (!bookSet.Contains(booksKey))
                        {
                            bookSet.Add(booksKey);
                            isAdded = true;
                            break;
                        }
                    }
                    if (!isAdded)
                    {
                        bookSets.Add(new List<int>{ booksKey});
                    }
                }
            }
            return bookSets.Sum(x => x.Count * 8 * _discounts[x.Count]);
        }

        private double GetDiscount(int numberDiferentOfBook)
        {
            return _discounts[numberDiferentOfBook];
        }
    }
}