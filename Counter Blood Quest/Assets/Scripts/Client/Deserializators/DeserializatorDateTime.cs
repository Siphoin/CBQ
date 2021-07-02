using System;
using System.Collections;
using UnityEngine;

namespace Client.Deserializators
{
    public static class DeserializatorDateTime
    {
        public static object DeserializeDateTime (byte[] data)
        {

            long ticks = BitConverter.ToInt64(data, 0);

            return new DateTime(ticks);
        }

        public static byte[] SerializeDateTime (object obj)
        {
            DateTime time = (DateTime)obj;

            byte[] result = new byte[8];


           BitConverter.GetBytes(time.Ticks).CopyTo(result, 0);


            return result;
        }

    }
}