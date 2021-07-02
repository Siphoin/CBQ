[System.Serializable]
public class WeaponDataException : System.Exception
{
    public WeaponDataException() { }
    public WeaponDataException(string message) : base(message) { }
    public WeaponDataException(string message, System.Exception inner) : base(message, inner) { }
    protected WeaponDataException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}