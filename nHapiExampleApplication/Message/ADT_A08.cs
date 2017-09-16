using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nHapiExampleApplication.Segment;

namespace nHapiExampleApplication.Message
{
    class ADT_A08 : NHapi.Model.V23.Message.ADT_A08
    {
        public ADT_A08()
            : base()
        {
            init(new NHapi.Base.Parser.DefaultModelClassFactory());
        }

        public ADT_A08(NHapi.Base.Parser.IModelClassFactory factory)
            : base(factory)
        {
            init(factory);
        }

        ///<summary>
        /// initalize method for ADT_A08. This does the segment setup for the message.
        ///</summary>
        private void init(NHapi.Base.Parser.IModelClassFactory factory)
        {
            try
            {
                this.add(typeof(ZD1), false, false);
            }
            catch (NHapi.Base.HL7Exception e)
            {
                NHapi.Base.Log.HapiLogFactory.GetHapiLog(GetType()).Error("Unexpected error creating ADT_A08 - this is probably a bug in the source code generator.", e);
            }
        }

        public ZD1 ZD1
        {
            get;
            set;
        }
    }
}
