using AutoMapper;
using NPaperless.Services.Models;

namespace NPaperless.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CorrespondentDto, Correspondent>()
                .ForMember(x => x.slug, opt => opt.Ignore())
                .ForMember(x => x.id, opt => opt.Ignore());


            CreateMap<DocumentDto, Document>()
                .ForMember(x => x.id, opt => opt.Ignore())
                .ForMember(x => x.storagePath, opt => opt.Ignore())
                .ForMember(x => x.content, opt => opt.Ignore())
                .ForMember(x => x.modified, opt => opt.Ignore())
                .ForMember(x => x.ArchivedFileName, opt => opt.Ignore())
                .ForMember(x => x.OriginalFileName, opt => opt.Ignore())
                .ForMember(x => x.ArchiveSerialNumber, opt => opt.Ignore())
                .ReverseMap();

           
            //CreateMap<GetSavedViews200ResponseResultsInner, GetSavedViewsResponseResultsInnerDto>();
        }
    }
}
