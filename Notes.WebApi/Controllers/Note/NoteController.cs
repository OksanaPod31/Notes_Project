using MediatR;
using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Models;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Commands.DeleteCommand;

namespace Notes.WebApi.Controllers.Note
{
    // [Route("api/note")]
    public class NoteController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        private readonly IMapper mapper;
        public NoteController(IMapper _mapper)
        {

            mapper = _mapper;
        }

        //[Route("~/NoteController/GetAll")]
        [HttpGet]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery { };
            var vm = await Mediator.Send(query);
            return View(vm);

        }



        //[Route("~/NoteController/Get/{id}")]
        [HttpGet]
        public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery
            {

                Id = id
            };
            var vm = await Mediator.Send(query);
            return View(vm);
        }
        //[Route("~/NoteController/Create")]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();

        }

        //[Route("~/NoteController/Create")]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateNoteDto createNoteDto)
        {
            var command = mapper.Map<CreateNoteCommand>(createNoteDto);

            var noteId = await Mediator.Send(command);
            return RedirectToAction("GetAll");

        }
        [HttpGet]
        public async Task<ActionResult> Update(Guid id)
        {
            var upd = new UpdateNoteDto { Id = id };
            return View(upd);
        }
        //[Route("~/api/Update/{id}")]
        [HttpPost]
        public async Task<ActionResult> Update(UpdateNoteDto updateNoteDto)
        {

            var command = mapper.Map<UpdateNoteCommand>(updateNoteDto);
            await Mediator.Send(command);
            return RedirectToAction("Get", new { id = command.Id });
        }
        //[Route("~/NoteController/Delete/{id}")]
        //[HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand
            {
                Id = id,

            };
            await Mediator.Send(command);
            return RedirectToAction("GetAll");
        }
    }



}
