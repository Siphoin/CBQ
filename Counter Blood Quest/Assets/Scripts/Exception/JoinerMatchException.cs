[System.Serializable]
public class JoinerMatchException : System.Exception
{
    public JoinerMatchException() { }
    public JoinerMatchException(string message) : base(message) { }
    public JoinerMatchException(string message, System.Exception inner) : base(message, inner) { }
    protected JoinerMatchException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}