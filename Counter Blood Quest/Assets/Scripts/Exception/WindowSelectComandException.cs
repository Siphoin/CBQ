[System.Serializable]
public class WindowSelectComandException : System.Exception
{
    public WindowSelectComandException() { }
    public WindowSelectComandException(string message) : base(message) { }
    public WindowSelectComandException(string message, System.Exception inner) : base(message, inner) { }
    protected WindowSelectComandException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}