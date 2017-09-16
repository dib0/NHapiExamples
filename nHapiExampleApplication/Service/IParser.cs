using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Base.Model;

namespace nHapiExampleApplication.Service
{
    internal interface IParser
    {
        /// <summary>
        /// Set the message to be parsed
        /// </summary>
        /// <param name="msg">HL7 message</param>
        void SetMessage(IMessage msg);

        /// <summary>
        /// Parse the message
        /// </summary>
        /// <returns>Acknowledge message</returns>
        string Parse();
    }
}
