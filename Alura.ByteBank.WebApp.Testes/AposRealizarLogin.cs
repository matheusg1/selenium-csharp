using Alura.ByteBank.WebApp.Testes.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Alura.ByteBank.WebApp.Testes
{
    public class AposRealizarLogin
    {
        IWebDriver driver;
        ITestOutputHelper console;

        public AposRealizarLogin(ITestOutputHelper _console)
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            console = _console;
        }

        [Fact]
        public void AposRealizarLoginVerificaSeExisteOpcaoAgenciaMenu()
        {
            var loginPO = new LoginPO(driver);            
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");
            
            loginPO.PreencherCampos("matheus@email.com", "senha01");            
            loginPO.Logar();

            Assert.Contains("Agência", driver.PageSource);
        }

        [Fact]
        public void TentaRealizarLoginSemPreencherCampos()
        {
            //Arrange
            driver.Navigate().GoToUrl("https://localhost:44309/");
            driver.FindElement(By.LinkText("Login")).Click();
            var login = driver.FindElement(By.Id("Email"));
            var senha = driver.FindElement(By.Id("Senha"));
            var btnLogar = driver.FindElement(By.Id("btn-logar"));

            //login.SendKeys("andre@email.com");
            //senha.SendKeys("senha01");

            //Act
            btnLogar.Click();

            //Assert
            Assert.Contains("The Email field is required.", driver.PageSource);
            Assert.Contains("The Senha field is required.", driver.PageSource);
        }

        [Fact]
        public void TentaRealizarLoginComSenhaInvalida()
        {
            //Arrange
            driver.Navigate().GoToUrl("https://localhost:44309/");
            driver.FindElement(By.LinkText("Login")).Click();
            var login = driver.FindElement(By.Id("Email"));
            var senha = driver.FindElement(By.Id("Senha"));
            var btnLogar = driver.FindElement(By.Id("btn-logar"));

            login.SendKeys("andre@email.com");
            senha.SendKeys("senha0");

            //Act
            btnLogar.Click();

            //Assert
            Assert.Contains("Login", driver.Title);
        }

        [Fact]
        public void RealizarLoginAcessaListaDeContas()
        {
            //Arrange
            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");

            var login = driver.FindElement(By.Name("Email"));
            var senha = driver.FindElement(By.Name("Senha"));

            login.SendKeys("matheus@email.com");
            senha.SendKeys("senha01");

            driver.FindElement(By.Id("btn-logar")).Click();

            driver.FindElement(By.Id("contacorrente")).Click();

            IReadOnlyCollection<IWebElement> elements =
                driver.FindElements(By.TagName("a"));

            var elemento = elements
                .FirstOrDefault(x => x.Text.Contains("Detalhes"));

            //Act
            elemento.Click();
            //Assert
            Assert.Contains("Voltar", driver.PageSource);
        }
    }
}
