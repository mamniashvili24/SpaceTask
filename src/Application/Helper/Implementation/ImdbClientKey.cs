using Application.Helper.Abstraction;

namespace Application.Helper.Implementation;

public class ImdbClientKey : IImdbClientKey
{
    public string Key { get; set; }
}