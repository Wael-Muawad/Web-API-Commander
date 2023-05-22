using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Repository
{
    public class MockCommanderRepository : ICommanderRepository
    {
        public void CreateCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommand()
        {
            List<Command> commands = new List<Command>
            {
                new Command { Id = 0, HowTo = "How TO", Line = "Line", Platform = "Platform" },
                new Command { Id = 1, HowTo = "How TO", Line = "Line", Platform = "Platform" },
                new Command { Id = 2, HowTo = "How TO", Line = "Line", Platform = "Platform" }
            };
            return commands;
        }

        public Command GetCommandById(int Id)
        {
            return new Command { Id = 0, HowTo = "How TO", Line = "Line", Platform = "Platform" };
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new NotImplementedException();
        }
    }
}
