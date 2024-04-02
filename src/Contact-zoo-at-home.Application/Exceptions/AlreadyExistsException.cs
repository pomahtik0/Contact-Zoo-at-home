using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application.Exceptions
{
    /// <summary>
    /// throws if trying to create something that is already created in db.
    /// </summary>
    public class AlreadyExistsException : Exception
    {

    }
}
