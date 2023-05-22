using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Repository
{
    public class SqlCommanderRepository : ICommanderRepository
    {
        private readonly CommanderContext _context;

        public SqlCommanderRepository(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            _context.Add(command);
        }
      

        public IEnumerable<Command> GetAllCommand()
        {
            return _context.Commands.ToList();
            
        }

        public Command GetCommandById(int Id)
        {
            return _context.Commands.SingleOrDefault(c => c.Id == Id);
        }

        public void UpdateCommand(Command command)
        {
            //Do Nothing
        }

        public void DeleteCommand(Command command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            _context.Remove(command);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }    
    }
}
