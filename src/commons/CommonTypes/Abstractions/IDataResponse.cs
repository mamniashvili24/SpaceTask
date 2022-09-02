namespace CommonTypes.Abstractions;

public interface IDataResponse<T>
{
    public T Data { get; set; }
    public bool IsError { get; set; }
    public string Message { get; set; }
}

public interface IDataResponse
{
    public bool IsError { get; set; }
    public string Message { get; set; }
}