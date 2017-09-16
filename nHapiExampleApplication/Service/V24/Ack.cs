using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Model.V24.Message;
using NHapi.Base.Model;
using NHapi.Base.Parser;

namespace nHapiExampleApplication.Service.V24
{
    internal class Ack : IAckMessage
    {
        #region Private properties
        private ACK message;
        #endregion

        #region Constructor
        internal Ack()
        {
            message = new ACK();
        }
        #endregion

        #region IAckMessage Members
        public void SetMessage(IMessage msg)
        {
            message = (ACK)HL7MessageHelper.MakeACK(msg, "AA", "Error");
        }

        public void SetError(IMessage msg)
        {
            message = (ACK)HL7MessageHelper.MakeACK(msg, "AE", "Error");

            // Add an ERR segment
            NHapi.Model.V24.Datatype.ELD eld = message.ERR.GetErrorCodeAndLocation(0);
            eld.CodeIdentifyingError.AlternateIdentifier.Value = "1";
            eld.CodeIdentifyingError.AlternateText.Value = "Something went wrong.";
        }

        public string GetAckMessage()
        {
            PipeParser parser = new PipeParser();
            return parser.Encode(message);
        }
        #endregion
    }
}
