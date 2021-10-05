using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace capacitacion
{
    public class Tests
    {

        IWebDriver driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory + @"\Drivers");

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            driver.Navigate().GoToUrl("https://www.google.com");

            string title = driver.Title;

            driver.Navigate().GoToUrl("https://www.facebook.com");

            driver.Navigate().Back();

            Assert.AreEqual(driver.Title, title);

        }

        [Test]
        public void Test2()
        {
            driver.Navigate().GoToUrl("https://icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/basic.htm");

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
        public void Extra()
        {
            driver.Navigate().GoToUrl("https://icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/interes.html");

            driver.FindElement(By.ClassName("button")).Click();

            List<string> errores = new List<string>();

            IWebElement monto = driver.FindElement(By.Name("amount"));
            if (!Convert.ToBoolean(monto.GetAttribute("required")))
            {
                errores.Add("El campo mount no es obligatorio");
            }

            IWebElement rate = driver.FindElement(By.Name("rate"));
            if (!Convert.ToBoolean(rate.GetAttribute("required")))
            {
                errores.Add("El campo rate no es obligatorio");
            }

            IWebElement time = driver.FindElement(By.Name("time"));
            if (!Convert.ToBoolean(time.GetAttribute("required")))
            {
                errores.Add("El campo time no es obligatorio");
            }

            IWebElement time_units = driver.FindElement(By.Name("time_units"));
            if (!Convert.ToBoolean(time_units.GetAttribute("required")))
            {
                errores.Add("El campo time_units no es obligatorio");
            }


            int conteoErrores = errores.Count;

            if (conteoErrores != 0)
            {
                errores.ForEach(Console.WriteLine);
                Assert.IsTrue(false);
            }

        }

        [Test]
        public void Test4()
        {
            driver.Navigate().GoToUrl("https://icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/alerts.html");

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
        public void Extra1()
        {
            driver.Navigate().GoToUrl("https://icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/alerts.html");

            IWebElement element = driver.FindElement(By.Id("show-alert"));
            element.Click();

            IAlert alert = driver.SwitchTo().Alert();

            alert.Accept();

            string resultado = driver.FindElement(By.Id("result-value")).Text;

            Assert.AreEqual(resultado, "Alert");

        }

        [Test]
        public void Extra2()
        {
            driver.Navigate().GoToUrl("https://icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/alerts.html");

            IWebElement element = driver.FindElement(By.Id("show-confirm"));
            element.Click();

            IAlert alert = driver.SwitchTo().Alert();

            alert.Accept();

            string resultado = driver.FindElement(By.Id("result-value")).Text;

            Assert.AreEqual(resultado, "Alert");

        }

        [Test]
        public void Test5()
        {
            driver.Navigate().GoToUrl("https://icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/ajax.html");

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
        public void Test6()
        {

            driver.Navigate().GoToUrl("https://icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/basic.htm");

            string ventanaAutomatizacion = driver.Title;

            driver.FindElement(By.LinkText("Enlace 2")).Click();

            var ventanas = driver.WindowHandles;

            driver.SwitchTo().Window(ventanas[1]);

            string ventanaActual = driver.Title;

            Assert.AreNotEqual(ventanaAutomatizacion, ventanaActual);

            driver.Close();// Si no se cierra la ventana a la que dirige "Enlace 2" falla la prueba Test7_Extra2 al ejecutar todas las pruebas a la vez

            driver.SwitchTo().Window(ventanas[0]);

            ventanaActual = driver.Title;

            Assert.AreEqual(ventanaAutomatizacion, ventanaActual);

        }

        [Test]
        public void Test7()
        {

            driver.Navigate().GoToUrl("https://icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/frames/index.html");
            driver.SwitchTo().Frame("menu");

            IWebElement link = driver.FindElement(By.LinkText("White Page"));
            link.Click();

            driver.SwitchTo().DefaultContent();

            driver.SwitchTo().Frame("content");

            string titulo = driver.FindElement(By.TagName("h1")).Text;

            Assert.AreEqual("White Page", titulo);
        }

        [Test]
        public void Test7_Extra1()
        {

            driver.Navigate().GoToUrl("https://icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/frames/index.html");
            driver.SwitchTo().Frame("content");

            IWebElement link = driver.FindElement(By.XPath("//ul[1]/li[1]/a"));
            link.Click();

            driver.SwitchTo().DefaultContent();

            driver.SwitchTo().Frame("content");

            IWebElement linkBack = driver.FindElement(By.LinkText("Back to original page"));
            linkBack.Click();

            driver.SwitchTo().DefaultContent();

        }

        [Test]
        public void Test7_Extra2()
        {
            driver.Navigate().GoToUrl("https://icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/frames/index.html");
            driver.SwitchTo().Frame("content");

            IWebElement link = driver.FindElement(By.XPath("//ul[3]/li[1]/a"));
            link.Click();

            var ventanas = driver.WindowHandles;

            driver.SwitchTo().Window(ventanas[1]);

            IWebElement linkBack = driver.FindElement(By.LinkText("Back to original page"));
            linkBack.Click();

            driver.SwitchTo().Window(ventanas[0]);

            driver.SwitchTo().DefaultContent();

        }

        [Test]
        public void Test7a()
        {

            driver.Navigate().GoToUrl("https://icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/drag_and_drop.html");

            IWebElement amarillo = driver.FindElement(By.Id("draggable"));

            IWebElement verde = driver.FindElement(By.Id("droppable"));

            Actions dragAndDrop = new Actions(driver);

            dragAndDrop.DragAndDrop(amarillo, verde).Perform();

            Assert.AreEqual(verde.Text, "Dropped");
        }

        [Test]
        public void Test8()
        {

            driver.Navigate().GoToUrl("https://icy-dune-0d3ed7d0f.azurestaticapps.net/selenium/selectable.html");

            IWebElement numero2 = driver.FindElement(By.XPath("//*[@id='selectable']/li[2]"));

            IWebElement numero3 = driver.FindElement(By.XPath("//*[@id='selectable']/li[3]"));

            IWebElement numero4 = driver.FindElement(By.XPath("//*[@id='selectable']/li[4]"));

            Actions multipleSelect = new Actions(driver);

            multipleSelect.KeyDown(Keys.Control).Click(numero2).Click(numero3).Click(numero4).KeyUp(Keys.Control).Perform();

            string resultado = driver.FindElement(By.Id("select-result")).Text;

            Assert.AreEqual(resultado, "#2 #3 #4");
        }




        [TearDown]
        public void TearDown()
        {

            //Si dejo esta sentencia ejecutable o driver.Quit(); hace el primer test y luego tira error de conexión

            //driver.Close();
        }
    }
}