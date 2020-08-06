using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmdApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CmdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase // change Controller to ControllerBase
    {
        private readonly CommandContext _context;

        public CommandsController(CommandContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            return _context.CommandItems;
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandItem(int id)
        {
            var commandItem = _context.CommandItems.Find(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            return commandItem;
        }

        [HttpPost]
        public ActionResult<Command> PostCommandItem(Command command)
        {
            _context.CommandItems.Add(command);
            _context.SaveChanges();

            return CreatedAtAction("GetCommandItem", new Command { Id = command.Id }, command);
        }

        [HttpPut("{id}")]
        public ActionResult PutCommandItem(int id, Command command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            _context.Entry(command).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Command> DeleteCommandItem(int id)
        {
            var commandItem = _context.CommandItems.Find(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            _context.CommandItems.Remove(commandItem);
            _context.SaveChanges();

            return commandItem;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<string>> GetString()
        //{
        //    return new string[] { "this", "is", "hard", "coded"};
        //}

    }
}