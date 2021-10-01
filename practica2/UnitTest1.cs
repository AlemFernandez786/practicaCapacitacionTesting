using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;


namespace practica2
{
    public class Tests
    {

        IWebDriver driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory + @"\Drivers");

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test2()
        {
            driver.Navigate().GoToUrl("https:\\icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/basic.htm");

            IWebElement element = driver.FindElement(By.XPath("/html/body/section/p[4]"));

            Assert.AreEqual("Parrafo 4", element.Text);

        }

        [Test]
        public void Test3()
        {
            driver.Navigate().GoToUrl("https://icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/interes.html");

            IWebElement monto = driver.FindElement(By.Id("amount"));
            monto.SendKeys("1000");

            IWebElement tasaInteres = driver.FindElement(By.Name("rate"));
            tasaInteres.SendKeys("5");

            IWebElement periodo = driver.FindElement(By.Name("rate_units"));

            //driver.FindElement(By.XPath("//*[@id='porcentaje']/div[2]/select/option[2]")).Click();

            SelectElement selectPeriodo = new SelectElement(periodo);
            selectPeriodo.SelectByValue("month");

            IWebElement tiempo = driver.FindElement(By.Name("time"));
            tiempo.SendKeys("2");

            //driver.FindElement(By.Name("time_units")).Click();
            //driver.FindElement(By.CssSelector("input[value='year']")).Click();
            driver.FindElement(By.XPath("//input[@value='year']")).Click();

            tiempo.Submit();

            string interesDevuelto = driver.FindElement(By.Id("interest")).Text;

            Assert.AreEqual("1200", interesDevuelto);
        }

        [Test]
        public void TestAlerts1()
        {
            driver.Navigate().GoToUrl("https:\\icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/alerts.html");

            IWebElement element = driver.FindElement(By.Id("show-prompt"));
            element.Click();

            IAlert alert = driver.SwitchTo().Alert();

            string texto = "Hola Mundo!";
            alert.SendKeys(texto);

            alert.Accept();

            string resultado = driver.FindElement(By.Id("result-value")).Text;

            Assert.AreEqual(resultado, texto);

        }

        [Test]
        public void TestAlerts2()
        {
            driver.Navigate().GoToUrl("https:\\icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/alerts.html");

            IWebElement element = driver.FindElement(By.Id("show-alert"));
            element.Click();

            IAlert alert = driver.SwitchTo().Alert();

            alert.Accept();

            string resultado = driver.FindElement(By.Id("result-value")).Text;

            Assert.AreEqual(resultado, "Alert");

        }

        [Test]
        public void TestAlerts3()
        {
            driver.Navigate().GoToUrl("https:\\icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/alerts.html");

            IWebElement element = driver.FindElement(By.Id("show-confirm"));
            element.Click();

            IAlert alert = driver.SwitchTo().Alert();

            alert.Accept();

            string resultado = driver.FindElement(By.Id("result-value")).Text;

            Assert.AreEqual(resultado, "Alert");

        }


        [Test]
        public void TestWaits3()
        {
            driver.Navigate().GoToUrl("https:\\icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/ajax.html");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.FindElement(By.XPath("//*[@id='make']/option[3]")).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("model")));

            driver.FindElement(By.XPath("//*[@id='model']/option[4]")).Click();

            driver.FindElement(By.Id("accept")).Click();

            wait.Until(ExpectedConditions.ElementExists(By.Id("value")));

            string modelo = driver.FindElement(By.Id("value")).Text;

            Assert.AreEqual("Audi-A6", modelo);


        }

        [Test]
        public void TestVentanas1()
        {

            driver.Navigate().GoToUrl("https:\\icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/basic.htm");

            string ventanaAutomatizacion = driver.Title;

            driver.FindElement(By.LinkText("Enlace 2")).Click();

            var ventanas = driver.WindowHandles;

            driver.SwitchTo().Window(ventanas[1]);

            string ventanaActual = driver.Title;

            Assert.AreNotEqual(ventanaAutomatizacion, ventanaActual);

            driver.SwitchTo().Window(ventanas[0]);

            ventanaActual = driver.Title;

            Assert.AreEqual(ventanaAutomatizacion, ventanaActual);




        }











        [TearDown]
        public void TearDown()
        {
            //driver.Quit();
        }
    }
}