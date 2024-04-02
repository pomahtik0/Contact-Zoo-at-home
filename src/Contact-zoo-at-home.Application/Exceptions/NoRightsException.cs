using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application.Exceptions
{
    /// <summary>
    /// throwed if trying to perform action with no access to it, or with invalid data
    /// </summary>
    public class NoRightsException : Exception
    {

    }
}
