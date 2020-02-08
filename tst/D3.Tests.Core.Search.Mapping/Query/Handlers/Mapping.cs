namespace D3.Tests.Core.Search.Mapping.Query.Handlers
{
    using System.Linq;
    using AutoMapper;
    using D3.Tests.Models.Queryable;

    public class Mapping
        : Profile
    {
        public Mapping()
        {
            CreateMap<TestItem, TestItemMapped>(MemberList.Source)
                .ForMember(s => s.Children, o => o.MapFrom(t => t.Children.Select(c => c.Name)))
                .ForMember(s => s.Identifier, o => o.MapFrom(t => t.Identifier.ToString()));
        }
    }
}
