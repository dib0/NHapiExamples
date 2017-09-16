using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NHapi.Base;
using NHapi.Base.Model;

namespace nHapiExampleApplication.Util
{
    public class HL7InputSreamMessageStringEnumerator : IEnumerator<string>
    {
        #region Private properties
        private Stream inputStream;
        private bool myIgnoreComments;
        private bool myHasNext;
        private string myNext;
        private StringBuilder myBuffer;
        private bool myFoundMessageInBuffer = false;
        #endregion

        #region Constructor
        public HL7InputSreamMessageStringEnumerator(Stream inStream)
        {
            inputStream = inStream;
            myBuffer = new StringBuilder();
        }
        #endregion

        #region Public properties
        public string Current
        {
            get
            {
                string result = myNext;
                myNext = null;

                return result;
            }
        }

        object System.Collections.IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public bool IgnoreComments
        {
            get
            {
                return myIgnoreComments;
            }
            set
            {
                myIgnoreComments = value;
            }
        }
        #endregion

        #region Public methods
        public void Dispose()
        {
            inputStream.Dispose();
        }

        public bool MoveNext()
        {
            return hasNext();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private methods
        public bool hasNext()
        {
            if (myNext == null)
            {
                int next;
                int prev = -1;
                int endOfBuffer = -1;
                bool inComment = false;

                while (true)
                {
                    try
                    {
                        next = inputStream.ReadByte();
                    }
                    catch (IOException e)
                    {
                        throw new Exception("IOException reading from input", e);
                    }

                    if (next == -1)
                    {
                        break;
                    }

                    char nextChar = (char)next;
                    if (nextChar == '#' && myIgnoreComments && (prev == -1 || prev == '\n' || prev == '\r'))
                    {
                        inComment = true;
                        continue;
                    }

                    // Convert '\n' or "\r\n" to '\r'
                    if (nextChar == 10)
                    {
                        if (myBuffer.Length > 0)
                        {
                            if (myBuffer[myBuffer.Length - 1] == 13)
                            {
                                // don't append
                            }
                            else
                            {
                                myBuffer.Append((char)13);
                            }
                        }
                    }
                    else if (inComment)
                    {
                        if (nextChar == 10 || nextChar == 13)
                        {
                            inComment = false;
                        }
                    }
                    else
                    {
                        myBuffer.Append(nextChar);
                    }

                    prev = next;

                    int bLength = myBuffer.Length;
                    if (nextChar == 'H' && bLength >= 3)
                    {
                        if (myBuffer[bLength - 2] == 'S')
                        {
                            if (myBuffer[bLength - 3] == 'M')
                            {
                                if (myFoundMessageInBuffer)
                                {
                                    if (myBuffer[bLength - 4] < 32)
                                    {
                                        endOfBuffer = bLength - 3;
                                        break;
                                    }
                                }
                                else
                                {
                                    // Delete any whitespace or other stuff before
                                    // the first message
                                    myBuffer.Remove(0, bLength - 3);
                                    myFoundMessageInBuffer = true;
                                }
                            }
                        }
                    }

                } // while(true)

                if (!myFoundMessageInBuffer)
                {
                    myHasNext = false;
                    return myHasNext;
                }

                String msgString;
                if (endOfBuffer > -1)
                {
                    msgString = myBuffer.ToString().Substring(0, endOfBuffer);
                    myBuffer.Remove(0, endOfBuffer);
                }
                else
                {
                    msgString = myBuffer.ToString();
                    myBuffer.Clear();
                }

                if (!msgString.StartsWith("MSH"))
                {
                    myHasNext = false;
                    return myHasNext;
                }

                myNext = msgString;
                myHasNext = true;
            }

            return myHasNext;
        }
        #endregion
    }
}
