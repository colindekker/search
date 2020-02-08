namespace D3.Tests.Core.Search.Mapping.EFCore.Query.Handlers
{
    using System.Linq;
    using AutoMapper;
    using D3.Tests.Models.Entities;
    using D3.Tests.Models.Queryable;

    public class Mapping
        : Profile
    {
        public Mapping()
        {
            CreateMap<Parent, TestItemMapped>(MemberList.Source)
                .ForMember(s => s.Children, o => o.MapFrom(t => t.Children.Select(c => c.Name)));

            CreateMap<Child, TestItemMapped>(MemberList.Source);
        }
    }
}
