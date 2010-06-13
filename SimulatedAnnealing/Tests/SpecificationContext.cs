using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace PokerTest
{
    [TestFixture]
    public class SpecificationContext
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            CreateContext();
            Because();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            CleanUp();
        }
        public virtual void CreateContext() { }
        public virtual void Because() { }
        public virtual void CleanUp() { }
    }    
}
