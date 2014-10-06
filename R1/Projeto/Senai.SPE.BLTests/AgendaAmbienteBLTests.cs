using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    public class AgendaAmbienteBLTests
    {
        [TestMethod()]
        public void AgendaAmbienteBLTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAmbientesAgendadosTest()
        {
            var ambiente = new Mock<IAmbienteBL>();

            var reseult = ambiente.Object;

            var refel = reseult.Get();

            var revolt = ambiente.Setup(a => a.Get()).Returns(refel);


        }

        [TestMethod()]
        public void AgendarAmbienteTest()
        {
            var ambiente = new Mock<IAmbienteBL>();

            var reseult = ambiente.Object;
           
        }
    }
}
