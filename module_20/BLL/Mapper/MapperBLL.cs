using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;

namespace BLL.Mapper
{
    public class MapperBll : IMapperBLL
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
