using System;
using System.Runtime.Serialization;

namespace DXP.SmartConnectPickup.Common.Exceptions
{
    /// <summary>
    /// document : https://stackoverflow.com/questions/94488/what-is-the-correct-way-to-make-a-custom-net-exception-serializable
    /// </summary>
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
