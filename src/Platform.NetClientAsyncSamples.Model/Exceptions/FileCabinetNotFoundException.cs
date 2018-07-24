using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.NetClientAsyncSamples.Model.Exceptions
{
    public class FileCabinetNotFoundException : Exception
    {
        public FileCabinetNotFoundException(string message) : base(message)
        {
        }

        public FileCabinetNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
