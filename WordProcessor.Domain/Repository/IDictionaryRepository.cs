using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessor.Domain.Entities;

namespace WordProcessor.Domain.Repository
{
    public interface IDictionaryRepository: IAutoCompleteHandler
    {
        /// <summary>
        /// Метод считывает слова в файле
        /// </summary>
        /// <param name="filePath">Адрес файла</param>
        /// <returns>Слова, длина которых от 3 до 15, повторяющиеся в файле не менее 3 раз</returns>
        IEnumerable<Dictionary> GetWordsFromFile(string filePath);

        /// <summary>
        /// Метод создает словарь по входящему файлу
        /// </summary>
        /// <param name="filePath">Адрес файла</param>
        void CreateDictionary(string filePath);

        /// <summary>
        /// Метод обновляет словарь по входящему файлу
        /// </summary>
        /// <param name="filePath">Адрес файла</param>
        void UpdateDictionary(string filePath);

        /// <summary>
        /// Метод удаляет все слова из базы
        /// </summary>
        void ClearDictionary();

        void DictionaryCommands(string[] args);
    }
}
