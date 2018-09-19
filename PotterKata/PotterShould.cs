namespace PotterKata
{
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
            Check.That(price).IsEqualTo(8 * 2);
        }

        [Test]
        public void Return_16_with_discount_when_buy_2_different_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, numberOfCopies: 1);
            shoppingBasket.Add(2, numberOfCopies: 1);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(8 * 2 * 0.95);
        }

        [Test]
        public void Return_price_when_buy_2_identical_books_and_a_different_book()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, numberOfCopies: 1);
            shoppingBasket.Add(2, numberOfCopies: 2);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(8 * 2 * 0.95 + 8);
        }

        [Test]
        public void Return_price_when_buy_3_book1_and_1_book2_and_1_book3()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, numberOfCopies: 3);
            shoppingBasket.Add(2, numberOfCopies: 1);
            shoppingBasket.Add(3, numberOfCopies: 1);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(8 * 3 * 0.9 + 8 * 2);
        }

        [Test]
        public void Return_price_when_buy_3_book1_and_1_book2_and_1_book3_and_1_book4()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, numberOfCopies: 3);
            shoppingBasket.Add(2, numberOfCopies: 1);
            shoppingBasket.Add(3, numberOfCopies: 1);
            shoppingBasket.Add(4, numberOfCopies: 1);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(8 * 4 * 0.8 + 8 * 2);
        }

        [Test]
        public void Return_price_when_buy_3_book1_and_1_book2_and_1_book3_and_1_book4_and_1_book5()
        {
            var shoppingBasket = new ShoppingBasket();
            shoppingBasket.Add(1, numberOfCopies: 3);
            shoppingBasket.Add(2, numberOfCopies: 1);
            shoppingBasket.Add(3, numberOfCopies: 1);
            shoppingBasket.Add(4, numberOfCopies: 1);
            shoppingBasket.Add(5, numberOfCopies: 1);
            var price = shoppingBasket.Price();
            Check.That(price).IsEqualTo(8 * 5 * 0.75 + 8 * 2);
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
}