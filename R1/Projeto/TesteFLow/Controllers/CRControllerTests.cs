using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPERepository.Fakes;
using SPE.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace SPE.Controllers.Tests
{
    [TestClass()]
    public class CRControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var controller = new CRController();
            var result= controller.Index();
            Assert.IsInstanceOfType(result,typeof(ViewResult));
          
        }

        [TestMethod()]
        public void DetalharCRTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CadastrarCRTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CadastrarCRTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditarCRTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditarCRTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExcluirCRTest()
        {
            Assert.Fail();
        }
    }
}
