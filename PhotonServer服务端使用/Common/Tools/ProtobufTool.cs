using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using System.IO;

namespace Common.Tools
{
    public class ProtobufTool
    {
        public static byte[] Serialize<T>(T instance) {
            byte[] bytes = null;
            using (MemoryStream ms = new MemoryStream()) {
                Serializer.Serialize<T>(ms, instance);
                bytes = new byte[ms.Position];
                var fullBytes = ms.GetBuffer();
                Array.Copy(fullBytes, bytes, bytes.Length);
            }
            return bytes;
        }
        public static T Deserialize<T>(byte[] bytes) {
            using (MemoryStream ms = new MemoryStream(bytes)) {
              return   Serializer.Deserialize<T>(ms);
            }
        }
    }
}
