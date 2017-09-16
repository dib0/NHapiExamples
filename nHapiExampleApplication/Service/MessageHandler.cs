using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Base.Parser;
using NHapi.Base.Model;
using NHapi.Base.Util;
using NHapi.Base;
using nHapiExampleApplication.Service;

namespace nHapiExampleApplication.Service
{
    /// <summary>
    /// This class handles a HL7 message
    /// </summary>
    internal class MessageHandler
    {
        #region Private properties
        private string message;
        #endregion

        #region Constructor
        public MessageHandler(string hl7Message)
        {
            message = hl7Message;
        }
        #endregion

        #region Public methods
        public void ParseMessage()
        {
            IMessage hl7Message = null;
            string result = null;

            try
            {
                PipeParser parser = new PipeParser();
                hl7Message = parser.Parse(message);
                
                IParser messageParser = HL7VersionFactory.GetParserOnHl7Message(hl7Message);
                result = messageParser.Parse();
            }
            catch (HL7Exception ex)
            {
                // Handle error, probably by sending a NACK message
                Console.WriteLine(ex);
            }

            // Send the result back
            Console.WriteLine("Result: {0}", result);
        }
        #endregion
    }
}
