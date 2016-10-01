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
    public abstract class ShimTest
    {
        private IDisposable shimContext;
        
        public virtual void InitializeClass()
        {
            shimContext = ShimsContext.Create();
        }

        public virtual void CleanUpClass()
        {
            shimContext.Dispose();
        }
    }
}
