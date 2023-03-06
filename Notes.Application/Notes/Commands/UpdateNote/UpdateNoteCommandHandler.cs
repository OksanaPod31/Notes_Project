using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
    {
        private readonly INoteDbContext _dbContext;

        public UpdateNoteCommandHandler(INoteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Notes.FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);
            if(entity == null)
            {
                throw new NotFoundException(nameof(Notes), request.Id);
            }
            
           
            entity.Details = request.Details;
            entity.Title = request.Title;
            entity.EditDate = DateTime.Now;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }
    }
}
