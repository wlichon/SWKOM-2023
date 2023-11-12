using AutoMapper;
using NPaperless.Services.Models;

namespace NPaperless.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CorrespondentDto, Correspondent>();
            //CreateMap<GetSavedViews200ResponseResultsInner, GetSavedViewsResponseResultsInnerDto>();
        }
    }
}
