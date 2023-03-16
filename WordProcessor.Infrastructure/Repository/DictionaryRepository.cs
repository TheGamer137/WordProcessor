using WordProcessor.Domain.Repository;
using WordProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace WordProcessor.Infrastructure.Repository
{
    public class DictionaryRepository : IDictionaryRepository
    {
        private readonly WordProcessorContext _context;
        public char[] Separators { get; set; } = new char[] { ' ' };
        public DictionaryRepository(WordProcessorContext context)
        {
            _context = context;
        }

        public IEnumerable<Dictionary> GetWordsFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                char[] delimiters = { ' ', '.', ',', ':', '!', '?', ';', '\n', '\t', '\r', '-', '"', '«', '»', '\'', '-' };
                
                return File.ReadAllText(filePath, Encoding.UTF8)
                    .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                    .GroupBy(g => g.ToLower())
                    .Select(d => new Dictionary
                    {
                        Word = d.Key,
                        Count = d.Count()
                    })
                    .Where(d => d.Count >= 3);


            }
            throw new Exception(string.Format("Файл не найден!", filePath));
        }

        public void CreateDictionary(string filePath)
        {
            if (_context.Dictionaries.Any())
            {
                ConsoleKey response;
                do
                {
                    Console.Write("Словарь уже создан, вы уверены что хотите создать новый словарь? [y/n]");
                    response = Console.ReadKey(false).Key;
                    if (response != ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                    }
                    if(response==ConsoleKey.Y)
                    {
                        ClearDictionary();
                        SaveWords(filePath);
                    }
                    if(response==ConsoleKey.N)
                    {
                        Console.ReadLine();
                    }
                } while (response != ConsoleKey.Y && response != ConsoleKey.N);
            }
            else
            {
                SaveWords(filePath);
            }

        }

        public void ClearDictionary()
        {
            _context.Dictionaries.RemoveRange(_context.Dictionaries);
            _context.SaveChanges();
        }

        public void DictionaryCommands(string[] args)
        {
            switch (args[0])
            {
                case "create":
                    if (args.Length >= 2)
                    {
                        CreateDictionary(args[1]);
                    }
                    else
                        Console.WriteLine("Введите имя файла!");
                    break;

                case "update":
                    if (args.Length >= 2)
                    {
                        UpdateDictionary(args[1]);
                    }
                    else
                        Console.WriteLine("Введите имя файла!");
                    break;


                case "clear":
                    ClearDictionary();
                    break;

                default:
                    throw new Exception(string.Format("Неизвестная комманда '{0}'", args[0]));
            }
        }

        public void UpdateDictionary(string filePath)
        {
            var words = GetWordsFromFile(filePath);
            foreach (var w in words)
            {
                var word = _context.Dictionaries.FirstOrDefault(d => d.Word == w.Word);

                if (word == null)
                {
                    _context.Dictionaries.Add(w);
                }
                else
                {
                    word.Count += w.Count;
                }
            }
            _context.SaveChanges();
        }

        public string[] AutocompleteUserInput(string word)
        {
            word = word.ToLower();
            return _context.Dictionaries
                      .Where(k => k.Word.StartsWith(word))
                      .OrderByDescending(b => b.Count)
                      .ThenBy(c => c.Word)
                      .Take(5)
                      .Select(a => a.Word)
                      .ToArray();
        }

        public void SaveWords(string filePath)
        {
            _context.Dictionaries.AddRange(GetWordsFromFile(filePath));
            _context.SaveChanges();
        }
    }
}
