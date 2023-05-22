using Alura.ByteBank.WebApp.Testes.PageObjects;
using Alura.ByteBank.WebApp.Testes.Utilitarios;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace Alura.ByteBank.WebApp.Testes
{
    public class NavegandoNaPaginaHome : IClassFixture<Gerenciador>
    {
        IWebDriver driver;
        LoginPO loginPO;
        HomePO homePO;

        public NavegandoNaPaginaHome(Gerenciador gerenciador)
        {
            driver = gerenciador.Driver;
            loginPO = new LoginPO(driver);
            homePO = new HomePO(driver);
        }

        [Fact]
        public void CarregaPaginaHomeEVerificaTituloDaPagina()
        {
            //Act
            loginPO.Navegar("https://localhost:44309");

            //Assert
            Assert.Contains("WebApp", driver.Title);
        }

        [Fact]
        public void CarregadaPaginaHomeVerificaExistenciaLinkLoginEHome()
        {
            //Act
            loginPO.Navegar("https://localhost:44309");

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
            loginPO.Navegar("https://localhost:44309/");
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
            loginPO.Navegar("https://localhost:44309/Agencia/Index");

            //Assert
            Assert.Contains("401", driver.PageSource);
        }

        [Fact]
        public void TentaAcessarPaginaSemEstarLogadoVerificaURL()
        {
            //Arrange & Act
            loginPO.Navegar("https://localhost:44309/Agencia/Index");

            //Assert
            Assert.Contains("https://localhost:44309/Agencia/Index", driver.Url);
            Assert.Contains("401", driver.PageSource);
        }

        //cpf 16218407004
        //guid ca3e926f-0b1b-4bbb-bc17-11ef530a77d4
        [Fact]
        public void RealizaLoginAcessaMenuCadastraCliente()
        {
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

            loginPO.PreencherCampos("matheus@email.com", "senha01");                        
            loginPO.Logar();                        

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

        [Fact]
        public void RealizarLoginAcessaListagemDeContas()
        {

            //Arrange
            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

            //Act
            loginPO.PreencherCampos("andre@email.com", "senha01");
            loginPO.Logar();

            homePO.LinkContaCorrenteClick();

            //Assert   
            Assert.Contains("Adicionar Conta-Corrente", driver.PageSource);
        }
    }
}
