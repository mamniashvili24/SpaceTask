using CommonTypes.Abstractions;

namespace Application.Helper.Abstraction;

public interface IImdbService
{
    Task<IDataResponse<T>> GetAsync<T>(string methodName, string lanugageCode, string queryParameter);
}