using System;
using NHapi.Base;
using NHapi.Base.Parser;
using NHapi.Base.Model;
using NHapi.Model.V23.Datatype;
using NHapi.Base.Log;

namespace nHapiExampleApplication.Segment
{
    ///
    /// Represents a ZD1 Segment. This is a locally defined segment
    ///
    [Serializable]
    public sealed class ZD1 : AbstractSegment
    {
        public ZD1(IGroup parent, IModelClassFactory factory)
            : base(parent, factory)
        {
            IMessage message = Message;
            try
            {
                add(typeof(EI), true, 0, 1, new object[] { message }, "NroControlDelPrestador");
                add(typeof(EI), false, 1, 4, new object[] { message }, "NroControlDelFinanciador");
                add(typeof(CE), false, 1, 180, new object[] { message }, "EstadoDeLaAutorizacion");
                add(typeof(EI), false, 1, 180, new object[] { message }, "NumeroDePreAutorizacion");
                add(typeof(TS), false, 1, 180, new object[] { message }, "FechaDeEmisionDePreAutorizacion");
            }
            catch (HL7Exception he)
            {
                HapiLogFactory.GetHapiLog(GetType()).Error("Can't instantiate " + GetType().Name, he);
            }

        }

        public EI NroControlPrestador
        {
            get
            {
                EI ret;
                try
                {
                    IType t = GetField(1, 0);
                    ret = (EI)t;
                }
                catch (HL7Exception he)
                {
                    HapiLogFactory.GetHapiLog(GetType()).Error(
                        "Unexpected problem obtaining field value.  This is a bug.", he);
                    throw new Exception("An unexpected error ocurred", he);
                }
                catch (Exception ex)
                {
                    HapiLogFactory.GetHapiLog(GetType()).Error(
                        "Unexpected problem obtaining field value.  This is a bug.", ex);
                    throw new Exception("An unexpected error ocurred", ex);
                }
                return ret;

            }
        }

        public EI NroControlFinanciador
        {
            get
            {
                EI ret;
                try
                {
                    IType t = GetField(2, 0);
                    ret = (EI)t;
                }
                catch (HL7Exception he)
                {
                    HapiLogFactory.GetHapiLog(GetType()).Error(
                        "Unexpected problem obtaining field value.  This is a bug.", he);
                    throw new Exception("An unexpected error ocurred", he);
                }
                catch (Exception ex)
                {
                    HapiLogFactory.GetHapiLog(GetType()).Error(
                        "Unexpected problem obtaining field value.  This is a bug.", ex);
                    throw new Exception("An unexpected error ocurred", ex);
                }
                return ret;

            }
        }

        public CE EstadoAutorizacion
        {
            get
            {
                CE ret;
                try
                {
                    IType t = GetField(3, 0);
                    ret = (CE)t;
                }
                catch (HL7Exception he)
                {
                    HapiLogFactory.GetHapiLog(GetType()).Error(
                        "Unexpected problem obtaining field value.  This is a bug.", he);
                    throw new Exception("An unexpected error ocurred", he);
                }
                catch (Exception ex)
                {
                    HapiLogFactory.GetHapiLog(GetType()).Error(
                        "Unexpected problem obtaining field value.  This is a bug.", ex);
                    throw new Exception("An unexpected error ocurred", ex);
                }
                return ret;

            }
        }

        public EI NumeroPreAutorizacion
        {
            get
            {
                EI ret;
                try
                {
                    IType t = GetField(4, 0);
                    ret = (EI)t;
                }
                catch (HL7Exception he)
                {
                    HapiLogFactory.GetHapiLog(GetType()).Error(
                        "Unexpected problem obtaining field value.  This is a bug.", he);
                    throw new Exception("An unexpected error ocurred", he);
                }
                catch (Exception ex)
                {
                    HapiLogFactory.GetHapiLog(GetType()).Error(
                        "Unexpected problem obtaining field value.  This is a bug.", ex);
                    throw new Exception("An unexpected error ocurred", ex);
                }
                return ret;

            }
        }

        public TS FechaEmisionPreAutorizacion
        {
            get
            {
                TS ret;
                try
                {
                    IType t = GetField(5, 0);
                    ret = (TS)t;
                }
                catch (HL7Exception he)
                {
                    HapiLogFactory.GetHapiLog(GetType()).Error(
                        "Unexpected problem obtaining field value.  This is a bug.", he);
                    throw new Exception("An unexpected error ocurred", he);
                }
                catch (Exception ex)
                {
                    HapiLogFactory.GetHapiLog(GetType()).Error(
                        "Unexpected problem obtaining field value.  This is a bug.", ex);
                    throw new Exception("An unexpected error ocurred", ex);
                }
                return ret;

            }
        }
    }
}