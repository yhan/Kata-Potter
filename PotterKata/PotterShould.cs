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
            shoppingBasket.Add(1, 1);
            shoppingBasket.Add(2, 1);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(16 * 0.95);
        }

        [Test]
        public void Return_16_when_buy_2_same_book_and_a_different_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, 1);
            shoppingBasket.Add(2, 2);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(16 * 0.95 + 8);
        }

        [Test]
        public void Return_21_6_when_buy_3_different_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, 1);
            shoppingBasket.Add(2, 1);
            shoppingBasket.Add(3, 1);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(24 * 0.90);
        }

        [Test]
        public void Return_25_6_when_buy_4_different_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, 1);
            shoppingBasket.Add(2, 1);
            shoppingBasket.Add(3, 1);
            shoppingBasket.Add(4, 1);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(8 * 4 * 0.80);
        }

        [Test]
        public void Return_30_4_when_buy_2_book_1_and_2_book_2()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, 2);
            shoppingBasket.Add(2, 2);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(8 * 4 * 0.95);
        }

        [Test]
        public void Return_30_when_buy_5_different_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, 1);
            shoppingBasket.Add(2, 1);
            shoppingBasket.Add(3, 1);
            shoppingBasket.Add(4, 1);
            shoppingBasket.Add(5, 1);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(8 * 5 * 0.75);
        }

        [Test]
        public void Return_51_20_when_buy_52_different_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, 2);
            shoppingBasket.Add(2, 2);
            shoppingBasket.Add(3, 2);
            shoppingBasket.Add(4, 1);
            shoppingBasket.Add(5, 1);
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
        
        private readonly List<int> _books = new List<int>();
        
        public void Add(int bookId, int numberOfCopies)
        {
            for (var i = 0; i < numberOfCopies; i++)
            {
                _books.Add(bookId);
            }
        }

        public double Price()
        {
            var bookSets = new List<BookSet>
                               {
                                  new BookSet()
                               };
            var pivot = CalculatePivot();

            foreach (var bookId in _books)
            {
                ConstructBookSets(bookSets, pivot, bookId);
            }

            return bookSets.Sum(x => x.GetPrice());
        }

        private static void ConstructBookSets(List<BookSet> bookSets, int pivot, int bookId)
        {
            var isAdded = false;
            foreach (var bookSet in bookSets)
            {
                isAdded = bookSet.IsNotOptimizationPossible(pivot) && bookSet.DoesNotContain(bookId);
                if (isAdded)
                {
                    bookSet.Add(bookId);
                    break;
                }
            }

            if (!isAdded)
            {
                bookSets.Add(new BookSet().Add(bookId));
            }
        }

        private int CalculatePivot()
        {
            var count = _books.GroupBy(x => x).Select(x => x.Count()).Distinct().Count();
            var pivot = count == 1 ? 5 : 4;
            return pivot;
        }
    }


    class BookSet
    {

        private const int _bookPrice = 8;

        private readonly Dictionary<int, double> _discounts = new Dictionary<int, double>
                                                                  {
                                                                      [1] = 1,
                                                                      [2] = 0.95,
                                                                      [3] = 0.90,
                                                                      [4] = 0.80,
                                                                      [5] = 0.75
                                                                  };

        List<int> books = new List<int>();
        
        public BookSet()
        {
            
        }

        public int Count()
        {
            return books.Count;
        }

        public BookSet Add(int bookId)
        {
            books.Add(bookId);
            return this;
        }

        public bool DoesNotContain(int bookId)
        {
            return !books.Contains(bookId);
        }

        public double GetPrice()
        {
            return this.Count() * _bookPrice * _discounts[this.Count()];
        }

        public bool IsNotOptimizationPossible( int pivot)
        {
            return this.books.Count < pivot;
        }
    }
}