using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Model
{
    /// <summary>
    /// Application domain wide exceptions that are displayed to end user.
    /// </summary>
    public class GUIVisibleException : Exception
    {
        public GUIVisibleException(string message) : base(message)
        {
            //intentionally left empty
        }

        public GUIVisibleException(string message, Exception innerException)
            : base(message, innerException)
        {
            //intentionally left empty
        }
    }
}
