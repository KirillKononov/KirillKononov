using AutoMapper;
using BLL.DTO;
using module_20.DTO;
using module_20.Interfaces;

namespace module_20.Mapper
{
    public class MapperPL : IMapperPL
    {
        public IMapper CreateMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HomeworkDTO, HomeworkViewModel>().ReverseMap();
                cfg.CreateMap<StudentDTO, StudentViewModel>().ReverseMap();
                cfg.CreateMap<ProfessorDTO, ProfessorViewModel>().ReverseMap();
                cfg.CreateMap<LectureDTO, LectureViewModel>().ReverseMap();
            }).CreateMapper();
        }
    }
}
