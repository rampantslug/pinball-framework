using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Configuration
{
    /// <summary>
    /// Contract resolver is used to determine when certain properties should be serialised.
    /// We want ALL properties serialized when using Mass Transit between Client - Server
    /// But we dont want everything seralized to config file.
    /// </summary>
    public class RsContractResolver : DefaultContractResolver
    {
        public static readonly RsContractResolver Instance = new RsContractResolver();
 
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (
                property.PropertyName == "Number"
                || property.PropertyName == "LastChangeTimeStamp" 
                || property.PropertyName == "PlayfieldImage"
                || property.PropertyName == "State"            
                || property.PropertyName == "IsActive"
                || property.PropertyName == "_state"
                || property.PropertyName == "StateString" 
                || property.PropertyName == "Images"
                || property.PropertyName == "Videos"
                || property.PropertyName == "Sounds")
            {
                property.ShouldSerialize =
                    instance => false;
            }

            return property;
        }
    }
}