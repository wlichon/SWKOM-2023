using AutoMapper;
using NPaperless.Services.DTOs;

namespace NPaperless.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GetSavedViews200Response, GetSavedViewsResponseDto>();
            CreateMap<GetSavedViews200ResponseResultsInner, GetSavedViewsResponseResultsInnerDto>();
        }
    }
}
