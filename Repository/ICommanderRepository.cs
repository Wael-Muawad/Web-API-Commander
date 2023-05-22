using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Repository
{
    public interface ICommanderRepository
    {
        IEnumerable<Command> GetAllCommand();
        Command GetCommandById(int Id);
        void CreateCommand(Command command);
        void UpdateCommand(Command command);
        void DeleteCommand(Command command);
        bool SaveChanges();
    }
}
