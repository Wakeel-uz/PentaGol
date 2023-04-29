using System.Text.RegularExpressions;

namespace PentaGol.Api.Helpers;

#pragma warning disable
public class ConfigureApiUrlName : IOutboundParameterTransformer
{
    public string TransformOutbound(object value)
    {
        // Slugify value
        return value == null ? null : Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}
