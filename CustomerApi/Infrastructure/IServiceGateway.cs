using System;
using SharedModels;

namespace CustomerApi.Infrastructure
{
    public interface IServiceGateway<T>
    {
        T Get(int id);
    }
}
