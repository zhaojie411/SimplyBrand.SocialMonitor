using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.Validation
{
    public class InvalidModelException : Exception
    {
        public InvalidModelException()
            : base()
        { }
        public InvalidModelException(string msg)
            : base(msg)
        { }
        private InvalidModelException(SerializationInfo inputinfo, StreamingContext sc) : base(inputinfo, sc) { }
    }

    public class InvalidIntValueException : Exception
    {
        public InvalidIntValueException()
            : base()
        { }
        public InvalidIntValueException(string msg)
            : base(msg)
        { }
        private InvalidIntValueException(SerializationInfo inputinfo, StreamingContext sc) : base(inputinfo, sc) { }
    }

    public class InValidFilepathException : Exception
    {
        public InValidFilepathException()
            : base()
        { }
        public InValidFilepathException(string msg)
            : base(msg)
        { }
        private InValidFilepathException(SerializationInfo inputinfo, StreamingContext sc) : base(inputinfo, sc) { }
    }
}
