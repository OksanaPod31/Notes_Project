using AutoMapper;
using Notes.Application.Common.Mapping;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class NoteLookupDto : IMapWith<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? PathImage { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Note, NoteLookupDto>().ForMember(noteDto => noteDto.Id,
                opt => opt.MapFrom(node => node.Id))
                .ForMember(noteDto => noteDto.Title,
                opt => opt.MapFrom(node => node.Title))
                .ForMember(noteDto => noteDto.PathImage,
                opt => opt.MapFrom(node => node.PathImage));
        }
    }
}
