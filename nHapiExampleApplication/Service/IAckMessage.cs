using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Base.Model;

namespace nHapiExampleApplication.Service
{
    internal interface IAckMessage
    {
        /// <summary>
        /// Set the original message, acknowlegde is ok
        /// </summary>
        /// <param name="msg">HL7 message</param>
        void SetMessage(IMessage msg);

        /// <summary>
        /// Set the original message, acknowledge is error
        /// </summary>
        /// <param name="msg">Original message</param>
        void SetError(IMessage msg);

        /// <summary>
        /// Create and retreive acknowledge message
        /// </summary>
        /// <returns>Ack message</returns>
        string GetAckMessage();
    }
}
