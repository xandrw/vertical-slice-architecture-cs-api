using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Spec.Helpers;

public static class JsonHelper
{
    public static void CompareJson(JToken expected, JToken actual)
    {
        switch (expected.Type)
        {
            case JTokenType.Object:
                foreach (var expectedProperty in expected.Children<JProperty>())
                {
                    var actualProperty = actual[expectedProperty.Name];
                    CompareJson(expectedProperty.Value, actualProperty!);
                }
                break;
            case JTokenType.Array:
                var expectedArray = expected as JArray;
                var actualArray = actual as JArray;

                for (int i = 0; i < expectedArray!.Count; i++)
                {
                    CompareJson(expectedArray[i], actualArray![i]);
                }
                break;
            default:
                Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
                break;
        }
    }
}