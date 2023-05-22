using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace Alura.ByteBank.WebApp.Testes
{
    public class NavegandoNaPaginaHome
    {
        IWebDriver driver;

        public NavegandoNaPaginaHome()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Fact]
        public void CarregaPaginaHomeEVerificaTituloDaPagina()
        {
            //Act
            driver.Navigate().GoToUrl("https://localhost:44309");

            //Assert
            Assert.Contains("WebApp", driver.Title);
        }

        [Fact]
        public void CarregadaPaginaHomeVerificaExistenciaLinkLoginEHome()
        {
            //Act
            driver.Navigate().GoToUrl("https://localhost:44309");

            //Assert
            Assert.Contains("Login", driver.PageSource);
            Assert.Contains("Home", driver.PageSource);
        }

        [Fact]
        public void TestandoLogin()
        {
            driver.Navigate().GoToUrl("https://localhost:44309/");
            driver.Manage().Window.Size = new System.Drawing.Size(976, 1040);
            driver.FindElement(By.LinkText("Login")).Click();
            driver.FindElement(By.Id("Email")).Click();
            driver.FindElement(By.Id("Email")).SendKeys("andre@email.com");
            driver.FindElement(By.Id("Senha")).Click();
            driver.FindElement(By.Id("Senha")).SendKeys("senha01");
            driver.FindElement(By.Id("btn-logar")).Click();
            driver.FindElement(By.CssSelector(".container:nth-child(2)")).Click();
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.Close();
        }

        [Fact]
        public void ValidaLinkDeLoginNaHome()
        {
            //Arrange
            driver.Navigate().GoToUrl("https://localhost:44309/");
            var linkLogin = driver.FindElement(By.LinkText("Login"));

            //Act
            linkLogin.Click();

            //Assert
            //Na página login existe uma img
            Assert.Contains("img", driver.PageSource);
        }

        [Fact]
        public void TentaAcessarPaginaSemEstarLogado()
        {
            //Arrange & Act
            driver.Navigate().GoToUrl("https://localhost:44309/Agencia/Index");

            //Assert
            Assert.Contains("401", driver.PageSource);
        }

        [Fact]
        public void TentaAcessarPaginaSemEstarLogadoVerificaURL()
        {
            //Arrange & Act
            driver.Navigate().GoToUrl("https://localhost:44309/Agencia/Index");

            //Assert
            Assert.Contains("https://localhost:44309/Agencia/Index", driver.Url);
            Assert.Contains("401", driver.PageSource);
        }

        //cpf 16218407004
        //guid ca3e926f-0b1b-4bbb-bc17-11ef530a77d4
        [Fact]
        public void RealizaLoginAcessaMenuCadastraCliente()
        {
            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");

            var login = driver.FindElement(By.Name("Email"));
            var senha = driver.FindElement(By.Name("Senha"));

            login.SendKeys("matheus@email.com");
            senha.SendKeys("senha01");

            driver.FindElement(By.Id("btn-logar")).Click();

            driver.FindElement(By.LinkText("Cliente")).Click();
            driver.FindElement(By.LinkText("Adicionar Cliente")).Click();
            
            var identificador = driver.FindElement(By.Id("Identificador"));
            var cpf = driver.FindElement(By.Id("CPF"));
            var nome = driver.FindElement(By.Id("Nome"));
            var profissao = driver.FindElement(By.Id("Profissao"));

            identificador.SendKeys("ca3e926f-0b1b-4bbb-bc17-11ef530a77d4");
            cpf.SendKeys("16218407004");
            nome.SendKeys("Junior");
            profissao.SendKeys("Carpinteiro");
            
            driver.FindElement(By.ClassName("btn-primary")).Click();

        }
    }
}
