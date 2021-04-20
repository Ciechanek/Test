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
    public class AutomationTests
    {
        static void Main(string[] args)
        {
            LoadingPageTest loadingPageTest = new LoadingPageTest();
            LoginTest loginTest = new LoginTest();
            Orders orders = new Orders();
          
            loadingPageTest.LoadPage("https://www.saucedemo.com/"); //load page test

            loginTest.SuccesfulLogIn(); //succesful login test

            loginTest.UnsuccesfulLogIn(); //unsuccesful login test

            loginTest.LockedUserLogIn(); //locked user login test

            loginTest.LogInWithoutTypingLogin(); //login without login test

            loginTest.LogInWithoutTypingPassword(); //login without password test

            orders.AddThreeItemsToCart(); //adding 3 items to cart test

            orders.RemovingItemFromCart(); //removing item from cart test

            orders.CheckoutAttempt(); //checkout attempt test

            orders.CheckoutAttemptWithInvalidPostalCode(); //checkout with invalid postal code test

            orders.MakeAnOrder(); //making an order test

            orders.CheckIfCorrectItemsAreInCart(); //checking of correct items test
        }
    }
}