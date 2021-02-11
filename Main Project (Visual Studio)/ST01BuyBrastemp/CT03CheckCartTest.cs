using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

namespace ST01BuyBrastemp {

    [TestFixture]
    public class CT03CheckCartTest {
        //Init
        private IWebDriver driver;
        public IDictionary<string, object> vars { get; private set; }
        private IJavaScriptExecutor js;

        [SetUp]
        public void SetUp() {
            driver = new FirefoxDriver();
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<string, object>();
        }

        [TearDown]
        protected void TearDown() {
            driver.Quit();
        }

        [Test]
        public void cT03CheckCart() {
            //Open in Desktop Mode (720p)
            driver.Navigate().GoToUrl("https://www.ricardoeletro.com.br/");
            driver.Manage().Window.Size = new System.Drawing.Size(1280, 720);

            //Validate Cart Button
            var cartButton = driver.FindElements(By.CssSelector(".i9-header__desktop--cart"));
            Assert.True(cartButton.Count > 0);
            driver.FindElement(By.CssSelector(".i9-header__desktop--cart")).Click();

            //Checks if the cart is empty
            try {
                var emptyCartMessage = driver.FindElement(By.CssSelector(".i9-balao > p:nth-child(1)"));
                Assert.That(emptyCartMessage.Text, Is.EqualTo("SEU CARRINHO AINDA ESTÁ VAZIO :("));
                Console.WriteLine("Failure: The cart is Empty");
                return;
            }
            catch (NoSuchElementException) {

            }

            //Checks if there are two or more itens on the cart
            try {
                var secondRemoveButton = driver.FindElements(By.CssSelector(".col-12:nth-child(3) .cart-checkout-products-product-remove"));
                if (secondRemoveButton.Count > 0) {
                    Console.WriteLine("Failure: there is a secound item on the cart");
                    return;
                }
            }
            catch (NoSuchElementException) {

            }

            //Validate Progress Bar and Icons
            Assert.True(driver.FindElements(By.CssSelector(".i9-header__desktop__checkout .i9-header-banner__logo")).Count > 0);
            Assert.True(driver.FindElements(By.CssSelector(".icon_cart-step-icon")).Count > 0);
            Assert.True(driver.FindElements(By.CssSelector(".icon_login-step-icon")).Count > 0);
            Assert.True(driver.FindElements(By.CssSelector(".icon_address-step-icon")).Count > 0);
            Assert.True(driver.FindElements(By.CssSelector(".icon_credit-step-icon")).Count > 0);
            Assert.True(driver.FindElements(By.CssSelector(".icon_finish-step-icon")).Count > 0);
            Assert.True(driver.FindElements(By.CssSelector(".checkout-steps-progress-bar")).Count > 0);
            Assert.True(driver.FindElements(By.CssSelector(".checkout-steps-progress-bar.step1")).Count > 0);
            Assert.True(driver.FindElements(By.CssSelector(".i9-header__desktop__checkout .i9-header-banner__logo")).Count > 0);


            //Validate Page Text Layout
            Assert.That(driver.FindElement(By.CssSelector(".checkout-steps-title")).Text, Is.EqualTo("Carrinho"));
            Assert.That(driver.FindElement(By.CssSelector(".d-none > .row > .col-4")).Text, Is.EqualTo("PRODUTO"));
            Assert.That(driver.FindElement(By.CssSelector(".col-2:nth-child(2)")).Text, Is.EqualTo("FRETE"));
            Assert.That(driver.FindElement(By.CssSelector(".col-1")).Text, Is.EqualTo("QTD"));
            Assert.That(driver.FindElement(By.CssSelector(".col-2:nth-child(4)")).Text, Is.EqualTo("UNITÁRIO"));
            Assert.That(driver.FindElement(By.CssSelector(".col-2:nth-child(5)")).Text, Is.EqualTo("TOTAL"));
            Assert.That(driver.FindElement(By.CssSelector(".product-freight-actions-form-title")).Text, Is.EqualTo("CONSULTE O FRETE"));
            Assert.That(driver.FindElement(By.CssSelector(".product-discount-actions-form-title")).Text, Is.EqualTo("CUPOM DE DESCONTO"));
            Assert.That(driver.FindElement(By.CssSelector(".col-12:nth-child(2) > .cart-checkout-total-title")).Text, Is.EqualTo("Total da sua compra"));

            //Validate Page Buttons Layout
            Assert.True(driver.FindElements(By.CssSelector(".product-freight-actions-form-title")).Count > 0);
            Assert.True(driver.FindElements(By.CssSelector(".product-discount-actions-form-title")).Count > 0);
            Assert.True(driver.FindElements(By.LinkText("VOLTAR")).Count > 0);
            Assert.True(driver.FindElements(By.Id("concluir-compra")).Count > 0);

            //Makes sure the product amount is one and that it's value exists
            Assert.That(driver.FindElement(By.Name("quantity")).GetAttribute("value"), Is.EqualTo("1"));
            Assert.True(driver.FindElements(By.CssSelector(".valor-unitario")).Count > 0);
            Assert.True(driver.FindElements(By.CssSelector(".col-12:nth-child(8)")).Count > 0);
            Assert.True(driver.FindElements(By.CssSelector(".col-12:nth-child(8)")).Count > 0);

            //Store product info
            var cartProductName = driver.FindElement(By.CssSelector(".card-title")).Text;
            var cartProductBrand = driver.FindElement(By.CssSelector(".i9-product-card__sold__by")).Text;
            var productTotal = driver.FindElement(By.CssSelector(".col-12:nth-child(8)")).Text;
            var cartTotal = driver.FindElement(By.CssSelector(".col-12:nth-child(8)")).Text;

            //Check if it really is a Brastemp
            if (!cartProductBrand.Equals("Vendido por: Brastemp"))
                Console.WriteLine("Failure: The product on the cart is not from Brastemp");

            //Check if something else is weighting on the price
            else if (!productTotal.Equals(cartTotal))
                Console.WriteLine("Failure: There is something else is being account for in the final price");

            //Everything is in order
            else Console.WriteLine("Success: Product " + cartProductName + " is in the cart for " + cartTotal);

            //Close the browsers
            driver.Close();
        }
    }
}