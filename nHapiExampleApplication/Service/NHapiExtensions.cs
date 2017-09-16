using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Base.Parser;
using NHapi.Base.Model;

namespace nHapiExampleApplication.Service
{
    public static class NHapiExtensions
    {
        public static bool SegmentExists(this PipeParser parser, IMessage message, string segment)
        {
            // Parse the message to a string and test for the segment name
            return parser.Encode(message).Contains(segment + "|");
        }
    }
}
