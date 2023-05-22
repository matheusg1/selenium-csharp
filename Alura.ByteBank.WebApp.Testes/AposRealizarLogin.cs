using Alura.ByteBank.WebApp.Testes.PageObjects;
using Alura.ByteBank.WebApp.Testes.Utilitarios;
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
    public class AposRealizarLogin : IClassFixture<Gerenciador>
    {
        IWebDriver driver;
        ITestOutputHelper console;
        LoginPO loginPO;

        public AposRealizarLogin(Gerenciador gerenciador, ITestOutputHelper _console)
        {
            driver = gerenciador.Driver;
            console = _console;
            loginPO = new LoginPO(driver);
        }

        [Fact]
        public void AposRealizarLoginVerificaSeExisteOpcaoAgenciaMenu()
        {         
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");
            
            loginPO.PreencherCampos("matheus@email.com", "senha01");            
            loginPO.Logar();

            Assert.Contains("Agência", driver.PageSource);
        }

        [Fact]
        public void TentaRealizarLoginSemPreencherCampos()
        {

            //Arrange
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");
            loginPO.PreencherCampos("", "");

            //Act
            loginPO.Logar();           

            //Assert
            Assert.Contains("The Email field is required.", driver.PageSource);
            Assert.Contains("The Senha field is required.", driver.PageSource);
        }

        [Fact]
        public void TentaRealizarLoginComSenhaInvalida()
        {
            //Arrange
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");
            loginPO.PreencherCampos("matheus@email.com", "senhi01");

            //Act
            loginPO.Logar();

            //Assert
            Assert.Contains("Login", driver.Title);
        }

        [Fact]
        public void RealizarLoginAcessaListaDeContas()
        {
            //Arrange
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

            loginPO.PreencherCampos("matheus@email.com", "senha01");
            loginPO.Logar();

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
