using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using module_20.DTO;

namespace module_20.Mapper
{
    public class MapperCreator
    {
        public IMapper CreateMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HomeworkDTO, HomeworkPl>().ReverseMap();
                cfg.CreateMap<StudentDTO, StudentPl>().ReverseMap();
                cfg.CreateMap<ProfessorDTO, ProfessorPl>().ReverseMap();
                cfg.CreateMap<LectureDTO, LecturePl>().ReverseMap();
            }).CreateMapper();
        }
    }
}
