namespace TidalApi.Web.Core.Data.Utilities;

public class ManyParamsBuilder
{
    public static string Build(List<string> parameter)
    {
        var parametersToReturn = string.Empty;

        foreach (var id in parameter)
        {
            if (id == parameter.Last())
            {
                parametersToReturn += $"filter%5Bid%5D={id}";
            }
            else
            {
                parametersToReturn += $"filter%5Bid%5D={id}&";
            }
           
        }

        return parametersToReturn;
    }
}