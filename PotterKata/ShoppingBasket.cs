using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace PotterKata
{
    internal class ShoppingBasket
    {
        private readonly Dictionary<int, int> _books = new Dictionary<int, int>();

        public void Add(int bookId, int numberOfCopies)
        {
            _books.Add(bookId, numberOfCopies);
        }

        public double Price()
        {
            double price = 0;

            var allKeys = _books.Keys.ToList();

            while (_books.Any(x => x.Value > 0))
            {
                var discountMultiplyFactor = _books.Values.Min();

                double discount = GetDiscountRate(_books);

                var discountableBooksCount = GetDiscoutableBooksCount(_books);
                
                price += discount * 8 * discountMultiplyFactor * discountableBooksCount;

                foreach (var bookId in allKeys)
                {
                    if (!_books.ContainsKey(bookId))
                    {
                        continue;
                    }

                    var remainingSizeForThisBook = _books[bookId] - discountMultiplyFactor;
                    if (remainingSizeForThisBook == 0)
                    {
                        _books.Remove(bookId);
                        continue;
                    }
                    _books[bookId] = remainingSizeForThisBook;
                }
            }

            return price;
        }

        private double GetDiscountRate(Dictionary<int, int> books)
        {
            var discountableBooksCount = GetDiscoutableBooksCount(_books);
            
            switch (discountableBooksCount)
            {
                case 1:
                    return 1;
                case 2:
                    return 0.95;
                case 3:
                    return 0.9;
                case 4:
                    return 0.8;
                case 5:
                    return 0.75;
            }

            throw new ConfigurationErrorsException("Potter have only 5 volumes");
        }

        private int GetDiscoutableBooksCount(Dictionary<int, int> books)
        {
            return books.Count(x => x.Value > 0);
        }
    }
}