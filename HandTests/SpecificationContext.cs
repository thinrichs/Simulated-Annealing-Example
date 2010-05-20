using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokerTest
{
    [TestClass]
    public class SpecificationContext
    {
        [TestInitialize]
        public void SetUp()
        {
            CreateContext();
            Because();
        }

        [TestCleanup]
        public void TearDown()
        {
            CleanUp();
        }
        public virtual void CreateContext() { }
        public virtual void Because() { }
        public virtual void CleanUp() { }
    }    
}
