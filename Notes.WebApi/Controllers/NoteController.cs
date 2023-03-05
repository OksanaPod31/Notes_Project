using MediatR;
using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Models;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Commands.DeleteCommand;

namespace Notes.WebApi.Controllers
{
    [Route("api/note")]
    public class NoteController : BaseController
    {
       
        private readonly IMapper mapper;
        public NoteController(IMapper _mapper)
        {
            
            mapper = _mapper;
        }

        [Route("~/api/GetAll")]
        [HttpGet]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery {};
            var vm = await Mediator.Send(query);
            return Ok(vm);

        }

        [Route("~/api/Get/{id}")]
        [HttpGet]
        public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery {
                
                Id = id 
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [Route("~/api/Create")]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            var command = mapper.Map<CreateNoteCommand>(createNoteDto);
            
            var noteId = await Mediator.Send(command);
            return Ok(noteId);

        }
        [Route("~/api/Update/{id}")]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateNoteDto updateNoteDto, Guid id)
        {
            
            var command = mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.Id = id;
            await Mediator.Send(command);
            return NoContent();
        }
        [Route("~/api/Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand
            {
                Id = id,
               
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }

   
            
}
