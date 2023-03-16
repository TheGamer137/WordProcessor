using Azure;
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
                while (Console.ReadKey().Key!=ConsoleKey.Escape && !string.IsNullOrWhiteSpace(Console.ReadLine()))
                {
                    var input = Console.ReadLine();
                    var words = _repository.AutocompleteUserInput(input);
                    foreach (var word in words)
                    {
                        Console.WriteLine("-{0}", word);
                    }

                }
            }
            else if (args.Length>0)
            {
                _repository.DictionaryCommands(args);
            }
        }
        
    }
}
