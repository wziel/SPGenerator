using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Tests
{
    /// <summary>
    /// Abstract unit test that uses Shims (Microsoft Fakes).
    /// </summary>
    public abstract class ShimTests
    {
        private IDisposable shimContext;
        
        public virtual void TestInitialize()
        {
            shimContext = ShimsContext.Create();
        }

        public virtual void TestCleanup()
        {
            shimContext.Dispose();
        }
    }
}
