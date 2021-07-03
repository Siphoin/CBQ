[System.Serializable]
public class WindowSelectPlayerClassException : System.Exception
{
    public WindowSelectPlayerClassException() { }
    public WindowSelectPlayerClassException(string message) : base(message) { }
    public WindowSelectPlayerClassException(string message, System.Exception inner) : base(message, inner) { }
    protected WindowSelectPlayerClassException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}