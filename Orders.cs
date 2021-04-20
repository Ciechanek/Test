/*
 * Task for Mateusz Ciechan
 * Author: Mateusz Ciechan
 * Completion Date:
 */
using System;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Linq;
using System.Text.RegularExpressions;

namespace TaskMateuszCiechan
{
    public class Orders
    {
        //public IWebDriver webDriver;
        //public IWebElement webElement;
        LoginTest loginTest = new LoginTest();

        private int numberOfItems;
        private string[] OrderedItems = new string[5];
        private Regex regex = new Regex("^d{2}-d{3}]$");
        private bool validation;
        private static string text = "";

        public bool PostalCodeValidation(string postalCode)
        {
            Regex regex = new Regex("^[0-9]{2}-[0-9]{3}$");
            bool validationScore = regex.IsMatch(postalCode);
            //string score = validationScore.ToString();
            return validationScore;
        }
        public void MakingCheckout(string firstName, string lastName, string postalCode)
        {
            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("shopping_cart_container"));
            loginTest.webElement.Click();
            //making an checkout
            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("checkout"));
            loginTest.webElement.Click();
            //typing first name
            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("first-name"));
            loginTest.webElement.SendKeys(firstName);
            //typing last name
            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("last-name"));
            loginTest.webElement.SendKeys(lastName);
            //typing postal code
            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("postal-code"));
            loginTest.webElement.SendKeys(postalCode);
        }
        public void AddThreeItemsToCart()
        {
            numberOfItems = 0;
            loginTest.webDriver = new ChromeDriver();
            loginTest.EnterPageAndLogIn("standard_user", "secret_sauce");

            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
            loginTest.webElement.Click();
            numberOfItems++;
            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light"));
            loginTest.webElement.Click();
            numberOfItems++;
            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("add-to-cart-sauce-labs-fleece-jacket"));
            loginTest.webElement.Click();
            numberOfItems++;

            string numberOfAddedItems = loginTest.webDriver.FindElement(By.ClassName("shopping_cart_badge")).Text;

            try
            {
                Assert.AreEqual(numberOfItems.ToString(), numberOfAddedItems);
                string text = "Add three items to cart test passed succesfully - number of added items is correct\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }
            catch
            {
                string text = "Add three items to cart test not passed\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }

            loginTest.webDriver.Close();
        }
        public void RemovingItemFromCart()
        {
            numberOfItems = 0;
            loginTest.webDriver = new ChromeDriver();
            loginTest.EnterPageAndLogIn("standard_user", "secret_sauce");

            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
            loginTest.webElement.Click();
            numberOfItems++;
            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light"));
            loginTest.webElement.Click();
            numberOfItems++;
            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("remove-sauce-labs-backpack"));
            loginTest.webElement.Click();
            numberOfItems--;

            string numberOfAddedItems = loginTest.webDriver.FindElement(By.ClassName("shopping_cart_badge")).Text;

            try
            {
                Assert.AreEqual(numberOfItems.ToString(), numberOfAddedItems);
                string text = "Removing item from cart test passed succesfully - number of added items is correct\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }
            catch
            {
                string text = "Removing item from cart test not passed\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }

            loginTest.webDriver.Close();
        }
        public void MakeAnOrder()
        {
            loginTest.webDriver = new ChromeDriver();
            loginTest.EnterPageAndLogIn("standard_user", "secret_sauce");

            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
            loginTest.webElement.Click();
            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light"));
            loginTest.webElement.Click();

            MakingCheckout("Mateusz", "Ciechan", "22-300");
            validation = PostalCodeValidation("22-300");

            if (validation == false)
            {
                string text = "Make an order test interrupted - invalid postal code\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
                loginTest.webDriver.Close();
                return;
            }

            //continue
            loginTest.webElement = loginTest.webDriver.FindElement(By.XPath("//div[contains(@class, 'checkout_buttons')]/input[1]"));
            loginTest.webElement.Click();

            //finish order
            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("finish"));
            loginTest.webElement.Click();

            string ActualURL = loginTest.webDriver.Url;
            string ExpectedURL = "https://www.saucedemo.com/checkout-complete.html";

            try
            {
                Assert.That(ActualURL, Is.EqualTo(ExpectedURL));
                string text = "Make an order test passed succesfully\nExpected value was: " + ActualURL + "\nActual Value is: " + ExpectedURL + "\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }
            catch
            {
                string text = "Make an order test not passed\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }
            loginTest.webDriver.Close();
        }
        public void CheckoutAttempt()
        {
            loginTest.webDriver = new ChromeDriver();
            loginTest.EnterPageAndLogIn("standard_user", "secret_sauce");

            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
            loginTest.webElement.Click();
            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light"));
            loginTest.webElement.Click();

            MakingCheckout("Mateusz", "Ciechan", "22-300");
            validation = PostalCodeValidation("22-300");

            if (validation == false)
            {
                string text = "Checkout Attempt test interrupted - invalid postal code\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
                loginTest.webDriver.Close();
                return;
            }
            loginTest.webElement.SendKeys(Keys.Return);

            string ActualURL = loginTest.webDriver.Url;
            string ExpectedURL = "https://www.saucedemo.com/checkout-step-two.html";

            try
            {
                Assert.That(ActualURL, Is.EqualTo(ExpectedURL));
                string text = "Checkout attempt test passed succesfully\nExpected value was: " + ActualURL + "\nActual Value is: " + ExpectedURL + "\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }
            catch
            {
                string text = "Checkout attempt test not passed\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }
            loginTest.webDriver.Close();
        }
        public void CheckoutAttemptWithInvalidPostalCode()
        {
            loginTest.webDriver = new ChromeDriver();
            loginTest.EnterPageAndLogIn("standard_user", "secret_sauce");

            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
            loginTest.webElement.Click();
            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light"));
            loginTest.webElement.Click();

            MakingCheckout("Mateusz", "Ciechan", "invalidPostalCode");
            //continue
            loginTest.webElement = loginTest.webDriver.FindElement(By.XPath("//div[contains(@class, 'checkout_buttons')]/input[1]"));
            loginTest.webElement.Click();

            string ActualURL = loginTest.webDriver.Url;
            string ExpectedURL = "https://www.saucedemo.com/checkout-step-two.html";

            try
            {
                Assert.AreNotEqual(ActualURL, ExpectedURL);
                string text = "Checkout with bad postal code test passed succesfully, user didnt make checkout with invalid Postal Code\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }
            catch
            {
                string text = "Checkout with bad postal code test not passed - user done checkout with invalid postal code\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }
            loginTest.webDriver.Close();
        }
        public void CheckIfCorrectItemsAreInCart()
        {
            loginTest.webDriver = new ChromeDriver();
            loginTest.EnterPageAndLogIn("standard_user", "secret_sauce");

            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
            loginTest.webElement.Click();
            OrderedItems[0] = "Sauce Labs Backpack";

            loginTest.webElement = loginTest.webDriver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light"));
            loginTest.webElement.Click();
            OrderedItems[1] = "Sauce Labs Bike Light";

            MakingCheckout("Mateusz", "Ciechan", "22-300");

            loginTest.webElement = loginTest.webDriver.FindElement(By.XPath("//div[contains(@class, 'checkout_buttons')]/input[1]"));
            loginTest.webElement.Click();

            for (int i=0; i < loginTest.webDriver.FindElements(By.XPath("//div[@class='inventory_item_name']")).Count(); i++)
            {
                //Console.WriteLine(OrderedItems[i]);
                //Console.WriteLine(loginTest.webDriver.FindElement(By.XPath("(//div[@class='inventory_item_name'])" + "[" +(i+1) +"]")).Text);
                if (OrderedItems[i] == loginTest.webDriver.FindElement(By.XPath("(//div[@class='inventory_item_name'])" + "["+(i+1)+"]")).Text)
                {
                    Orders.text = "Item " + (i+1) + " is OK\n";
                    File.AppendAllText("../../../TestReportMateuszCiechan.txt", Orders.text);
                }
                else
                {
                    Orders.text = "Checking correctness of items test not passed - wrong items in cart\n\n";
                    File.AppendAllText("../../../TestReportMateuszCiechan.txt", Orders.text);
                    break;
                }
            }

            text = "Checking correctness of items test passed succesfully\n\n";
            File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            loginTest.webDriver.Close();
        }
    }
}