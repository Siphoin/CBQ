[System.Serializable]
public class InventoryCharacterException : System.Exception
{
    public InventoryCharacterException() { }
    public InventoryCharacterException(string message) : base(message) { }
    public InventoryCharacterException(string message, System.Exception inner) : base(message, inner) { }
    protected InventoryCharacterException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}