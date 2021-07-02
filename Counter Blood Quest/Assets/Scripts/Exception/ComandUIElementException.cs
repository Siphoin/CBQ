[System.Serializable]
public class ComandUIElementException : System.Exception
{
    public ComandUIElementException() { }
    public ComandUIElementException(string message) : base(message) { }
    public ComandUIElementException(string message, System.Exception inner) : base(message, inner) { }
    protected ComandUIElementException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}