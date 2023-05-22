using AutoMapper;
using Commander.DTOs;
using Commander.Models;
using Commander.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepository _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            IEnumerable<Command> commands = _repository.GetAllCommand();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{id}", Name = nameof(GetCommandById))]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            Command command = _repository.GetCommandById(id);
            if (command is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<Command> CreateCommand(CommandCreateDto commandCreateDto)
        {
            Command model = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(model);
            _repository.SaveChanges();
            CommandReadDto commandReadDto = _mapper.Map<CommandReadDto>(model);
            return CreatedAtRoute(nameof(GetCommandById), new { id = model.Id }, commandReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult<Command> UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            Command commandModelFromRepository = _repository.GetCommandById(id);
            if (commandModelFromRepository is null)
            {
                return NotFound();
            }
            Command updatedCommand = _mapper.Map(commandUpdateDto, commandModelFromRepository);

            _repository.UpdateCommand(commandModelFromRepository);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PatchCommand(int id, JsonPatchDocument<CommandUpdateDto> jsonPatchDocument)
        {
            Command commandModelFromRepository = _repository.GetCommandById(id);
            if (commandModelFromRepository is null)
            {
                return NotFound();
            }

            CommandUpdateDto commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepository);

            jsonPatchDocument.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepository);
            _repository.UpdateCommand(commandModelFromRepository);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            Command command = _repository.GetCommandById(id);
            if (command is null)
            {
                return NotFound();
            }
            _repository.DeleteCommand(command);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
