using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Base.Model;
using NHapi.Base.Util;
using System.Configuration;

namespace nHapiExampleApplication.Service
{
    internal abstract class HL7MessageHelper
    {
        #region Private properties
        private static string communicationName;
        private static string environmentIdentifier;

        private static string CommunicationName
        {
            get
            {
                if (communicationName == null)
                    communicationName = ConfigurationSettings.AppSettings["ApplicationCommunicationName"];

                return communicationName;
            }
        }

        private static string EnvironmentIdentifier
        {
            get
            {
                if (environmentIdentifier == null)
                    environmentIdentifier = ConfigurationSettings.AppSettings["EnvironmentIdentifier"];

                return environmentIdentifier;
            }
        }
        #endregion

        #region Static methods
        /// <summary>
        /// Create an Ack message based on a received message
        /// </summary>
        /// <param name="inboundMessage">received message</param>
        /// <returns>Created ACK message</returns>
        public static IMessage MakeACK(IMessage inboundMessage)
        {
            return MakeACK(inboundMessage, "AA", null);
        }

        /// <summary>
        /// Create an Ack message based on a received message
        /// </summary>
        /// <param name="inboundMessage">received message</param>
        /// <param name="ackCode">Should be "AE" or "AR", can be "AA".</param>
        /// <param name="errorMessage">The reason the message was rejected or an error. If "AA" was supplied as ackCode the errorMessage should be null.</param>
        /// <returns>Created ACK message</returns>
        public static IMessage MakeACK(IMessage inboundMessage, string ackCode, string errorMessage)
        {
            IMessage ackMessage = null;
            // Get an object from the right ACK class
            string ackClassType = string.Format("NHapi.Model.V{0}.Message.ACK, NHapi.Model.V{0}", inboundMessage.Version.Remove(inboundMessage.Version.IndexOf('.'), 1));
            Type x = Type.GetType(ackClassType);
            ackMessage = (IMessage)Activator.CreateInstance(x);

            Terser inboundTerser = new Terser(inboundMessage);
            ISegment inboundHeader = null;
            inboundHeader = inboundTerser.getSegment("MSH");
            
            // Find the HL7 version of the inbound message:
            //
            string version = null;
            try
            {
                version = Terser.Get(inboundHeader, 12, 0, 1, 1);
            }
            catch (NHapi.Base.HL7Exception)
            {
                // I'm not happy to proceed if we can't identify the inbound
                // message version.
                throw new NHapi.Base.HL7Exception("Failed to get valid HL7 version from inbound MSH-12-1");
            }

            // Create a Terser instance for the outbound message (the ACK).
            Terser terser = new Terser(ackMessage);

            // Populate outbound MSH fields using data from inbound message
            ISegment outHeader = (ISegment)terser.getSegment("MSH");
            DeepCopy.copy(inboundHeader, outHeader);

            // Now set the message type, HL7 version number, acknowledgement code
            // and message control ID fields:
            string sendingApp = terser.Get("/MSH-3");
            string sendingEnv = terser.Get("/MSH-4");
            terser.Set("/MSH-3", CommunicationName);
            terser.Set("/MSH-4", EnvironmentIdentifier);
            terser.Set("/MSH-5", sendingApp);
            terser.Set("/MSH-6", sendingEnv);
            terser.Set("/MSH-7", DateTime.Now.ToString("yyyyMMddmmhh"));
            terser.Set("/MSH-9", "ACK");
            terser.Set("/MSH-12", version);
            terser.Set("/MSA-1", ackCode == null ? "AA" : ackCode);
            terser.Set("/MSA-2", Terser.Get(inboundHeader, 10, 0, 1, 1));

            // Set error message
            if (errorMessage != null)
                terser.Set("/ERR-7", errorMessage);

            return ackMessage;
        }
        #endregion
    }
}
