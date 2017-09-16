using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Base.Model;

namespace nHapiExampleApplication.Service
{
    internal abstract class HL7VersionFactory
    {
        #region Static methods
        public static IParser GetParserOnHl7Message(IMessage message)
        {
            IParser parser = null;

            switch (message.Version)
            {
                case "2.4":
                    parser = GetEventMessageParserV24(message);
                    break;
                default:
                    throw new NotImplementedException("HL7 version not supported.");
            }

            return parser;
        }
        #endregion

        #region Private static methods
        private static IParser GetEventMessageParserV24(IMessage message)
        {
            IParser parser = null;
            string eventName = message.Message.GetStructureName();

            switch (eventName)
            {
                case "ADT_A01":
                    // Init parser
                    parser = new V24.A01();
                    break;
                default:
                    throw new NotImplementedException("HL7 event message is not supported.");
            }

            parser.SetMessage(message);
            return parser;
        }
        #endregion
    }
}
