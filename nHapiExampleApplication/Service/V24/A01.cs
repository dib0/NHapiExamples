using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Base.Model;
using NHapi.Model.V24.Message;
using NHapi.Model.V24.Datatype;

namespace nHapiExampleApplication.Service.V24
{
    internal class A01 : IParser
    {
        #region Private properties
        private ADT_A01 message;
        #endregion

        #region IParser Members
        public void SetMessage(IMessage msg)
        {
            message = (ADT_A01) msg;
        }

        public string Parse()
        {
            // Do something with this message, for example store it to a database
            XPN name = message.PID.GetPatientName().FirstOrDefault(n => n.GivenName.Value.Length > 0);
            Console.WriteLine("Name of the person: {0} {1} {2}", name.GivenName.Value, name.SecondAndFurtherGivenNamesOrInitialsThereof.Value,
                name.FamilyName.Surname);

            // Create and return an Ack message
            Ack result = new Ack();
            result.SetMessage(message);
            return result.GetAckMessage();
        }
        #endregion
    }
}
