using System.Text;
using System.Web.Script.Serialization;

namespace DeMariaTeste.Application.Services
{
    // JavaScriptSerializer evita dependencia do Newtonsoft.Json. Em outro
    // contexto seria Json.NET ou System.Text.Json.
    internal static class AuditoriaSerializer
    {
        private static readonly JavaScriptSerializer _serializer = new JavaScriptSerializer
        {
            MaxJsonLength = int.MaxValue
        };

        public static string ToJson(object obj)
        {
            if (obj == null) return null;
            return _serializer.Serialize(obj);
        }
    }
}
