using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator()
        {
            RuleFor(updatenoteCommand => updatenoteCommand.Title).NotEmpty().MaximumLength(500);
            RuleFor(updateNoteCommand => updateNoteCommand.Id).NotEqual(Guid.Empty);

        }
    }
}
