using WordProcessor.Domain.Repository;
using WordProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;

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
                
                return File.ReadAllText(filePath, Encoding.UTF8).Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                    .GroupBy(g => g.ToLower())
                    .ToDictionary(g => g.Key, z => z.Count())
                    .Where(g => g.Value >= 3)
                    .Select(d => new Dictionary()
                    {
                        Word = d.Key
                    });
                
            }
            throw new Exception(string.Format("Файл {0} не найден!", filePath));
        }

        public void CreateDictionary(string filePath)
        {
            if (_context.Dictionaries.Any())
            {
                
            }
            _context.Dictionaries.AddRange(GetWordsFromFile(filePath));
            _context.SaveChanges();
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
                    _context.Dictionaries.Update(word);
                }
            }
            _context.SaveChanges();
        }

        public string[] GetSuggestions(string text, int index)
        {
            return _context.Dictionaries.Select(d => d.Word).ToArray();
        }
    }
}
