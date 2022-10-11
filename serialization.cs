using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Runtime.Serialization.Formatters;
using ServidorConsola.entidades;

namespace ServidorConsola
{
    public class serialization
    {
        public static byte[] Serializate(object toSeralizate)
        {
            MemoryStream memory = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(memory, toSeralizate);

            return memory.ToArray();
        }
        

        public static object Deserializate(byte[] data)
        {
            Mesa mesa = null;
            MemoryStream memory = new MemoryStream(data);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

               // formatter.Binder = new CurrentAssemblyDeserializationBinder();
               mesa = (Mesa)formatter.Deserialize(memory);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            

            return mesa;

          




        }
       
    }

    public class CurrentAssemblyDeserializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            return Type.GetType(String.Format("{0}, {1}", typeName, Assembly.GetExecutingAssembly().FullName)); 
        }
    }
}
