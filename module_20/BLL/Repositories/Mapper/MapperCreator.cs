using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;

namespace BLL.Repositories.Mapper
{
    public class MapperCreator : IMapperCreator
    {
        public IMapper CreateMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Homework, HomeworkDTO>().ReverseMap();
                cfg.CreateMap<Student, StudentDTO>().ReverseMap();
                cfg.CreateMap<Professor, ProfessorDTO>().ReverseMap();
                cfg.CreateMap<Lecture, LectureDTO>().ReverseMap();
            }).CreateMapper();
        }
    }
}
