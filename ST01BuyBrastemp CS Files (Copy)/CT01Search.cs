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
    public class CT01SearchTest {
        //Init
        private IWebDriver driver;
        public IDictionary<string, object> vars { get; private set; }
        private IJavaScriptExecutor js;
        private string searchText;

        [SetUp]
        public void SetUp() {
            driver = new FirefoxDriver();
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<string, object>();
            searchText = "Geladeira Brastemp";
        }

        [TearDown]
        protected void TearDown() {
            driver.Quit();
        }

        [Test]
        public void cT01Search() {
            //Open in Desktop Mode (720p)
            driver.Navigate().GoToUrl("https://www.ricardoeletro.com.br/");
            driver.Manage().Window.Size = new System.Drawing.Size(1280, 720);

            //Validate Search Bar and Button
            var searchBar = driver.FindElement(By.Id("search-desktop"));
            Assert.True(searchBar.Enabled && searchBar.GetAttribute("readonly") == null);
            Assert.True(driver.FindElements(By.CssSelector("#i9-search-desktop__addon > img")).Count > 0);

            //Search for the product
            driver.FindElement(By.Id("search-desktop")).SendKeys(searchText);
            driver.FindElement(By.Id("i9-search-desktop__addon")).Click();

            //Validate Search Echo
            Assert.That(driver.FindElement(By.CssSelector(".catalog-options-total-products > span")).Text, Is.EqualTo("Busca: " + searchText));

            //Checks if no product was found
            try {
                var noProductFound = driver.FindElement(By.CssSelector(".col-12 > p"));
                Assert.That(noProductFound.Text, Is.EqualTo("Sua busca não retornou nenhum produto. Faça uma nova busca com outros refinamentos."));
                Console.WriteLine("Failure: No Products Found");
                return;
            }
            catch (NoSuchElementException) {
                
            }

            //Validate "Sort By" Button
            var sortMenu = driver.FindElements(By.CssSelector("#open-order-options-desktop > span"));
            Assert.True(sortMenu.Count > 0);
            driver.FindElement(By.CssSelector("small")).Click();

            //Validate "Sort By" Menu
            Assert.That(driver.FindElement(By.LinkText("Mais Vendidos")).Text, Is.EqualTo("Mais Vendidos"));
            Assert.That(driver.FindElement(By.LinkText("Menor Preço")).Text, Is.EqualTo("Menor Preço"));
            Assert.That(driver.FindElement(By.LinkText("Marca")).Text, Is.EqualTo("Marca"));
            Assert.That(driver.FindElement(By.LinkText("Ordem Alfabética")).Text, Is.EqualTo("Ordem Alfabética"));
            Assert.That(driver.FindElement(By.LinkText("Mais Novos")).Text, Is.EqualTo("Mais Novos"));
            Assert.That(driver.FindElement(By.LinkText("Preço De/Por")).Text, Is.EqualTo("Preço De/Por"));
            Assert.That(driver.FindElement(By.LinkText("Relevância")).Text, Is.EqualTo("Relevância"));
            Assert.That(driver.FindElement(By.LinkText("Melhor Avaliados")).Text, Is.EqualTo("Melhor Avaliados"));

            //Validate "Show As" Menu
            Assert.That(driver.FindElement(By.CssSelector(".catalog-options-showby > span")).Text, Is.EqualTo("Exibir Como:"));
            Assert.True(driver.FindElements(By.CssSelector(".icon_thumbs-icon")).Count > 0);
            Assert.True(driver.FindElements(By.CssSelector(".icon_list-show-icon")).Count > 0);

            //Check that the first page of results exists
            Assert.True(driver.FindElements(By.CssSelector(".\\<li > span")).Count > 0);

            //Close the browser
            driver.Close();
        }
    }
}