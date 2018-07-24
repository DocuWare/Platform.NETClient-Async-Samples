using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.NetClientAsyncSamples.Model.Exceptions
{
    public class OrganizationNotFoundException : Exception
    {
        public OrganizationNotFoundException(string message) : base(message)
        {
        }

        public OrganizationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
