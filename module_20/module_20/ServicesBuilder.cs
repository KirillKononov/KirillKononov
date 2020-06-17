using BLL.BusinessLogic.Report;
using BLL.Interfaces;
using BLL.Repositories.Mapper;
using BLL.Repositories.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using module_20.Interfaces;
using module_20.Mapper;

namespace module_20
{
    public static class ServicesBuilder
    {
        public static void BuildServices(IServiceCollection services)
        {
            services.AddSingleton<IMapperBLL, MapperBll>();

            services.AddSingleton<IMapperPL, MapperPL>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IReportService, ReportService>();
        }
    }
}
