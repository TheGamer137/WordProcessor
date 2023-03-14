using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessor.Domain.Repository;

namespace WordProcessor.ConsoleApp
{
    public class App
    {
        private readonly IDictionaryRepository _repository;
        public App(IDictionaryRepository repository) =>_repository=repository;

        public void Run(string[] args)
        {
            if (!args.Any())
            {
                ReadLine.Read();
            }
            else if (args.Length>0)
            {
                _repository.DictionaryCommands(args);
            }
        }
        
    }
}
