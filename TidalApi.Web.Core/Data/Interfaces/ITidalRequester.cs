namespace TidalApi.Web.Core.Data.Interfaces;

public interface ITidalRequester
{
    Task<TidalResponse> Request(string endPoint);
    void SetToken(string token);
}