using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly INoteDbContext _dbContext;
        public CreateNoteCommandHandler(INoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = new Note
            {
               
                Title = request.Title,
                Details = request.Details,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null
            };
            await _dbContext.Notes.AddAsync(note, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return note.Id;


        }
    }
}
