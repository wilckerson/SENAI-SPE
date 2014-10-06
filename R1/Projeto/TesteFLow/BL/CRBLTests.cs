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
using Microsoft.QualityTools.Testing.Fakes;
using SPERepository.Fakes;

namespace BusinessLogic.Tests
{
    [TestClass()]
    public class CRBLTests
    {
        [TestMethod()]
        public void CRBLTest()
        {
            var teste = new CRBL();

        }

        [TestMethod()]
        public void ListarCRTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InserirCRTest()
        {
            //var mock = new Mock<ICRBL>();
            //var cr = new CR();
            //var objMock = mock.Object;
            //objMock.Get(a=>a.IdCR == 1);
            //mock.Verify(a => a.Get(b=>It.IsAny<bool>()));
            //using (ShimsContext.Create())
            //{
            //    var bl = new SPERepository.Fakes.ShimEntityRepository<CR>();

            //    bl.Get = (a, b) => { new };

            //}
        }

        [TestMethod()]
        public void AtualizarCRTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeletarCRTest()
        {
            Assert.Fail();
        }
    }
}
