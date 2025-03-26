using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace BetsAppMVC.Services
{
    public static class JsonHelper
    {
        public static List<T> ReadJson<T>(string fileName)
        {
            List<T> result = new List<T>();
            if (File.Exists(fileName)) 
            {
                var json = File.ReadAllText(fileName);
                result = JsonConvert.DeserializeObject<List<T>>(json);
               
            }
            return result;
        }
        public static void WriteJson<T>(string fileName, List<T> entities)
        {
            var json = JsonConvert.SerializeObject(entities);
            File.WriteAllText(fileName, json);
        }
    }
}
