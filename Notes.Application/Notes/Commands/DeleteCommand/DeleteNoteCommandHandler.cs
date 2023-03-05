using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;
using Notes.Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.DeleteCommand
{
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
    {
        private readonly INoteDbContext _dbcontext;

        public DeleteNoteCommandHandler(INoteDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbcontext.Notes.FindAsync(new object[] { request.Id }, cancellationToken);
            if(entity == null)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            _dbcontext.Notes.Remove(entity);
            await _dbcontext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
