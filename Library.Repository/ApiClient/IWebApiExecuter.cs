
namespace Library.Repository.ApiClient
{
    public interface IWebApiExecuter
    {
        Task InvokeDelete<T>(string uri);
        Task<T> InvokeGet<T>(string uri);
        Task<T> InvokePost<T>(string uri, T obj);
        Task<string> InvokePostAsString<T>(string uri, T obj);
        Task InvokePut<T>(string uri, T obj);
    }
}