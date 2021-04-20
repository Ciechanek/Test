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

namespace TaskMateuszCiechan
{
    public class LoginTest
    {
        public IWebDriver webDriver;
        public IWebElement webElement;
        public void EnterPageAndLogIn(string login, string password)
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            webDriver.Navigate().GoToUrl("https://www.saucedemo.com/");
            
            webElement = webDriver.FindElement(By.Id("user-name"));//typing login
            webElement.SendKeys(login);
            
            webElement = webDriver.FindElement(By.Id("password"));//typing password
            webElement.SendKeys(password);
            
            webElement = webDriver.FindElement(By.ClassName("btn_action"));//log in
            webElement.Click();
        }
        public void SuccesfulLogIn()
        {
            webDriver = new ChromeDriver(); //initialization of webDriver
            EnterPageAndLogIn("standard_user", "secret_sauce");

            string ActualURL = webDriver.Url;
            string ExpectedURL = "https://www.saucedemo.com/inventory.html";

            //assertion
            try
            {
                Assert.That(ActualURL, Is.EqualTo(ExpectedURL));
                string text = "Succesful Login test passed succesfully - user logged in\nExpected value was: " + ActualURL + "\nActual Value is: " + ExpectedURL + "\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }
            catch
            {
                string text = "Succesful Login test not passed\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }
            webDriver.Close();
        }
        public void UnsuccesfulLogIn()
        {
            webDriver = new ChromeDriver();
            EnterPageAndLogIn("standard_user", "bad_password");

            string ActualURL = webDriver.Url;
            string ExpectedURL = "https://www.saucedemo.com/inventory.html";

            try
            {
                Assert.AreNotEqual(ActualURL, ExpectedURL);
                string text = "Unsuccesful Login test passed succesfully - user not logged in after typing a bad password\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }
            catch
            {
                string text = "Unsuccesful Login test not passed\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }

            webDriver.Close();
        }
        public void LockedUserLogIn()
        {
            webDriver = new ChromeDriver();
            EnterPageAndLogIn("locked_out_user", "secret_sauce");
            try
            {
                Assert.IsTrue(webDriver.FindElement(By.ClassName("error-button")).Displayed);
                string text = "Locked user login test passed succesfully - locked user not logged in\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }

            catch
            {
                string text = "Locked user login test pnot passed\n\n";
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", text);
            }
            webDriver.Close();
        }
        public void LogInWithoutTypingLogin()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            webDriver.Navigate().GoToUrl("https://www.saucedemo.com/");

            IWebElement webElement = webDriver.FindElement(By.ClassName("btn_action"));
            webElement.Click();

            if (webDriver.PageSource.Contains("Username is required"))
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", "Login without username test passed - user not logged in without typing login\n\n");

            else
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", "Login without username test not passed - user logged in without typing login\n\n");

            webDriver.Close();
        }
        public void LogInWithoutTypingPassword()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            webDriver.Navigate().GoToUrl("https://www.saucedemo.com/");
            
            IWebElement webElement = webDriver.FindElement(By.Id("user-name"));
            webElement.SendKeys("standard_user");

            webElement = webDriver.FindElement(By.ClassName("btn_action"));
            webElement.Click();

            if (webDriver.PageSource.Contains("Password is required"))
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", "Login without password test passed succesfully - user not logged in without typing password\n\n");

            else
                File.AppendAllText("../../../TestReportMateuszCiechan.txt", "Login without password test not passed - user logged in without typing password\n\n");

            webDriver.Close();
        }
    }
}
