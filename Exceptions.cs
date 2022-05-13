namespace GenshinCheckIn
{
    public class Exceptions
    {
        [Serializable]
        public class InvalidCookiesException : Exception
        {
            public InvalidCookiesException() { }
            public InvalidCookiesException(string message) : base(message) { }
            public InvalidCookiesException(string message, Exception inner) : base(message, inner) { }
            protected InvalidCookiesException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}
