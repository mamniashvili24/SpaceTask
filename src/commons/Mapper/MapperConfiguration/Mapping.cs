namespace Mapper.MapperConfiguration;

public static class Mapping
{
    /// <summary>
    /// TSource From Sorce
    /// TTarget To Sorce
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TTarget Map<TSource, TTarget>(TSource value)
        where TSource : class
        where TTarget : class
    {
        var config = new AutoMapper.MapperConfiguration(cfg =>
            cfg.CreateMap<TSource, TTarget>());

        var mapper = new AutoMapper.Mapper(config);
        var result = mapper.Map<TSource, TTarget>(value);

        return result;
    }
}