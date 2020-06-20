using BLL.BusinessLogic.Report;
using BLL.Interfaces;
using BLL.Interfaces.ServicesInterfaces;
using BLL.Repositories;
using BLL.Repositories.Mapper;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using module_20.Interfaces;
using module_20.Mapper;

namespace module_20
{
    public static class ServicesBuilder
    {
        public static void BuildServices(IServiceCollection services)
        {
            services.AddScoped<IRepository<Homework>, HomeworkRepository>();
            services.AddScoped<IHomeworkService, HomeworkService>();
            
            services.AddScoped<IRepository<Lecture>, LectureRepository>();
            services.AddScoped<ILectureService, LectureService>();
            
            services.AddScoped<IRepository<Professor>, ProfessorRepository>();
            services.AddScoped<IProfessorService, ProfessorService>();        
            
            services.AddScoped<IRepository<Student>, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<IReportService, ReportService>();
            
            services.AddSingleton<IMapperBLL, MapperBll>();

            services.AddSingleton<IMapperPL, MapperPL>();
        }
    }
}
