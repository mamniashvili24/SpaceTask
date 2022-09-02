using CommonTypes.Abstractions;

namespace CommonTypes.Implementations;

public class DataResponse<T> : IDataResponse<T>
{
    public DataResponse()
    {
    }

    public DataResponse(Exception ex)
    {
        IsError = true;
        Message = ex?.Message;
    }
    public DataResponse(bool isError, string message)
    {
        IsError = isError;
        Message = message;
    }
    public DataResponse(string errorMessage)
    {
        IsError = true;
        Message = errorMessage;
    }
    public DataResponse(T data)
    {
        Data = data;
    }
    public T Data { get; set; }
    public bool IsError { get; set; }
    public string Message { get; set; }
}
public class DataResponse : IDataResponse
{
    public DataResponse()
    {
    }
    public DataResponse(Exception ex)
    {
        IsError = true;
        Message = ex?.Message;
    }
    public DataResponse(bool isError, string message)
    {
        IsError = isError;
        Message = message;
    }
    public DataResponse(string errorMessage)
    {
        IsError = true;
        Message = errorMessage;
    }
    public bool IsError { get; set; }
    public string Message { get; set; }
}