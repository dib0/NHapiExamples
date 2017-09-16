using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nHapiExampleApplication.Service;

namespace nHapiExampleApplication
{
    /// <summary>
    /// This is an example application for nHapi.
    /// </summary>
    class Program
    {
        #region Private constants
        // This is an example message from 7Edit (see http://www.7edit.com for more information)
        private const string HL7ExampleMessage = "MSH|^~\\&|ADT1|MCM|LABADT|MCM|198808181126|SECURITY|ADT^A01|MSG00001|P|2.4\rEVN|A01-|198808181123\rPID|||PATID1234^5^M11||JONES^WILLIAM^A^III||19610615|M-||2106-3|1200 N ELM STREET^^GREENSBORO^NC^27401-1020|GL|(919)379-1212|(919)271-3434~(919)277-3114||S||PATID12345001^2^M10|123456789|9-87654^NC\rNK1|1|JONES^BARBARA^K|SPO|||||20011105\rNK1|1|JONES^MICHAEL^A|FTH\rPV1|1|I|2000^2012^01||||004777^LEBAUER^SIDNEY^J.|||SUR||-||1|A0-\rAL1|1||^PENICILLIN||PRODUCES HIVES~RASH\rAL1|2||^CAT DANDER\rDG1|001|I9|1550|MAL NEO LIVER, PRIMARY|19880501103005|F||\rPR1|2234|M11|111^CODE151|COMMON PROCEDURES|198809081123\rROL|45^RECORDER^ROLE MASTER LIST|AD|CP|KATE^SMITH^ELLEN|199505011201\rGT1|1122|1519|BILL^GATES^A\rIN1|001|A357|1234|BCMD|||||132987\rIN2|ID1551001|SSN12345678\rROL|45^RECORDER^ROLE MASTER LIST|AD|CP|KATE^ELLEN|199505011201";
        #endregion

        #region Main
        static void Main(string[] args)
        {
            Console.WriteLine("Starting a handler to handle the message.");
            MessageHandler handler = new MessageHandler(HL7ExampleMessage);
            handler.ParseMessage();
        }
        #endregion
    }
}
