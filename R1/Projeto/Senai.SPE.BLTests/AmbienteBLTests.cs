using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Interfaces;

namespace BusinessLogic.Tests
{
    [TestClass()]
    public class AmbienteBLTests
    {
        [TestMethod()]
        public void AmbienteBLTest()
        {

            Assert.Fail();
        }

        [TestMethod()]
        public void InserirAmbienteRecursoTest()
        {
            var mockAmbiente = new Mock<IAmbienteBL>();
            var ambiente = mockAmbiente.Object;
            ambiente.DeletarAmbiente(1);
            mockAmbiente.Verify(a=>a.Delete(1));
            
        }

        [TestMethod()]
        public void AtualizarAmbienteRecursoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeletarAmbienteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RetornarAmbientesPorTest()
        {
            Assert.Fail();
        }
    }
}
