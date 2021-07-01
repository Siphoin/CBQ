[System.Serializable]
public class JoystikException : System.Exception
{
    public JoystikException() { }
    public JoystikException(string message) : base(message) { }
    public JoystikException(string message, System.Exception inner) : base(message, inner) { }
    protected JoystikException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
