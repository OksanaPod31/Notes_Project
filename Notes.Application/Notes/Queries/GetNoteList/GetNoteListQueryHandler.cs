using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Application.Notes.Queries.GetNoteDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListVm>
    {
        private readonly INoteDbContext _dbcontext;
        private readonly IMapper _mapper;

        public GetNoteListQueryHandler(INoteDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<NoteListVm> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
        {
            var noteQuery = await _dbcontext.Notes
                 .ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);
            return new NoteListVm { Notes = noteQuery };
        }
    }
}
