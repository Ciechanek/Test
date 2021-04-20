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
    public class LoadingPageTest
    {
        public void LoadPage(string url)
        {
            IWebDriver webDriver = new ChromeDriver(); //initialization of chromeDriver
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); //waiting for element on a page
            webDriver.Navigate().GoToUrl(url); //navigate to some url

            string ActualURL = webDriver.Url; //url of the page 
            string ExpectedURL = url; //url given to the function

            //assertion
            try
            {
                Assert.AreEqual(ActualURL, ExpectedURL);
                string text = "Load page test passed succesfully - page was loaded\n\n";
                File.WriteAllTextAsync("../../../TestReportMateuszCiechan.txt", text); //write to file execution result
            }
            catch
            {
                string text = "Load page test not passed\n\n";
                File.WriteAllTextAsync("../../../TestReportMateuszCiechan.txt", text); //write to file execution result
            }
            webDriver.Close(); //closing the driver
        }
    }
}
