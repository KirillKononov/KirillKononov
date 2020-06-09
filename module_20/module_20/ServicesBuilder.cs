using BLL.BusinessLogic.Report;
using BLL.Interfaces;
using BLL.Repositories;
using BLL.Repositories.Mapper;
using BLL.Repositories.UnitOfWork;
using DAL.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace module_20
{
    public static class ServicesBuilder
    {
        public static void BuildServices(IServiceCollection services)
        {
            services.AddSingleton<IMapperCreator, MapperCreator>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IReportService, ReportService>();
        }
    }
}
