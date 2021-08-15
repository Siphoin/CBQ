using Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Deserializators
{
  public static  class DeserializatorComandType
    {
        public static object DeserializeComandType(byte[] data)
        {
            int comandIndex = BitConverter.ToInt32(data, 0);

            return (ComandType)comandIndex;
        }

        public static byte[] SerializeComandType(object obj)
        {
            ComandType comand = (ComandType)obj;

            byte[] result = new byte[4];

            BitConverter.GetBytes((int)comand).CopyTo(result, 0);
          
            return result;
        }
    }
}
