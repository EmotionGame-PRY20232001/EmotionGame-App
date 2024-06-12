using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
//using Unity.Plastic.Newtonsoft.Json;
//using Unity.Plastic.Newtonsoft.Json.Serialization;
//com.unity.nuget.newtonsoft-json
//https://discussions.unity.com/t/json-newtonsoft-the-type-or-namespace-name-plastic-does-not-exist-in-the-namespace-unity/256898
public class IgnoreSerializeTagContractResolver : DefaultContractResolver
{
    protected override IList<JsonProperty> CreateProperties(System.Type type, MemberSerialization memberSerialization)
    {
        var properties = base.CreateProperties(type, memberSerialization);

        foreach (var property in properties)
        {
            if (property.UnderlyingName.Contains("k__BackingField"))
            {
                property.PropertyName = property.UnderlyingName
                    .Replace("<", "")
                    .Replace(">k__BackingField", "");
            }
        }

        return properties;
    }
}
