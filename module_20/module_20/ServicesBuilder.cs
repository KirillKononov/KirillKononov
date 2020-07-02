using System;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Interfaces.ServicesInterfaces;
using BLL.Mapper;
using BLL.Services;
using BLL.Services.Report;
using BLL.Services.StudentHomeworkUpdater;
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

            services.AddScoped<IStudentHomeworkUpdater, StudentHomeworkUpdater>();
            
            services.AddScoped<IReportService, ReportService>();

            services.AddSingleton<SMSSender>();
            services.AddSingleton<EmailSender>();

            services.AddTransient<Func<string, IMessageSender>>(serviceProvider => key =>
            {
                return key switch
                {
                    "SMS" => serviceProvider.GetService<SMSSender>(),
                    "Email" => serviceProvider.GetService<EmailSender>(),
                    _ => throw new ValidationException($"Was entered wrong message type: {key}")
                };
            });
            
            services.AddSingleton<IMapperBLL, MapperBll>();

            services.AddSingleton<IMapperPL, MapperPL>();
        }
    }
}
