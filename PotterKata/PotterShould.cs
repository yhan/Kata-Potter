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

        public void Add(int bookId, int numberOfCopies)
        {
            _books.Add(bookId, numberOfCopies);
        }

        public double Price()
        {
            double discount = 1;
            double price = 0;
            var numberOfBooks = this._books.Values.Sum();
            if (this._books.Count > 1)
            {
                discount = 0.95;
                price = this._books.Count * discount * 8;

                numberOfBooks -= this._books.Count;
            }

            return price + numberOfBooks * 8;
        }
    }
}