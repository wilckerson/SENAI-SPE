using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Senai.SPE.Domain;

namespace BusinessLogic.Tests
{
    [TestClass()]
    public class UsuarioBLTests
    {
        [TestMethod()]
        public void UsuarioBLTest()
        {
            var todo = new UsuarioBL();

        }

        [TestMethod()]
        public void InserirTest()
        {
            var foo = new UsuarioBL();
            var mock = new Mock<IUsuarioBL>();
            var usuario = new Usuario();
            foo.Inserir(model: usuario);
         
            var usuarioObj = mock.Object;

            mock.Setup(a => a.Inserir(new Usuario())).Returns(new Usuario());
            usuarioObj.Inserir(usuario);
            mock.Setup(a => a.GetById(1)).Returns(() => new Usuario());




        }

        [TestMethod()]
        public void AtualizarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ModificarTest()
        {
            Assert.Fail();
        }
    }
}
