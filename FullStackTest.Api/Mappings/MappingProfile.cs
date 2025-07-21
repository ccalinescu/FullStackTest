namespace FullStackTest.Api.Mappings
{
    public class MappingProfile : AutoMapper.Profile { 
        public MappingProfile()
        {
            CreateMap<Models.CreateTaskRequest, Models.MyTask>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => false));

            CreateMap<Models.UpdateTaskRequest, Models.MyTask>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.Completed));

            CreateMap<Models.MyTask, Models.MyTaskDto>();
        }
    }
}
