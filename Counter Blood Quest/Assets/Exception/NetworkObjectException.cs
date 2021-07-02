[System.Serializable]
public class NetworkObjectException : System.Exception
{
    public NetworkObjectException() { }
    public NetworkObjectException(string message) : base(message) { }
    public NetworkObjectException(string message, System.Exception inner) : base(message, inner) { }
    protected NetworkObjectException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}