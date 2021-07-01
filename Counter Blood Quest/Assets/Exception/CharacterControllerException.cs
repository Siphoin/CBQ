[System.Serializable]
public class CharacterControllerException : System.Exception
{
    public CharacterControllerException() { }
    public CharacterControllerException(string message) : base(message) { }
    public CharacterControllerException(string message, System.Exception inner) : base(message, inner) { }
    protected CharacterControllerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
