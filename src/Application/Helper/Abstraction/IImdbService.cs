namespace Application.Helper.Abstraction;

public interface IImdbService
{
    Task<T> GetAsync<T>(string methodName, string lanugageCode, string queryParameter);
}