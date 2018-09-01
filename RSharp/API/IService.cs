using Microsoft.Extensions.DependencyInjection;

namespace RSharp.API
{
    public interface IService
    {
        void SetupService(IServiceCollection collection);
    }
}
