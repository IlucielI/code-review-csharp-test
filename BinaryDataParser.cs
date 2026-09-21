using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Benchmark
{
    public class BinaryDataParser
    {
        // Vulnerable: Insecure deserialization via obsolete BinaryFormatter (CWE-502)
        public static object DeserializePayload(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                #pragma warning disable SYSLIB0011
                var formatter = new BinaryFormatter();
                // Insecure: Allows arbitrary remote code execution via gadget chains
                return formatter.Deserialize(ms);
                #pragma warning restore SYSLIB0011
            }
        }
    }
}
