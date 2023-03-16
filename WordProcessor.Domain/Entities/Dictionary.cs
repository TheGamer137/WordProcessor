using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessor.Domain.Entities
{
    public class Dictionary
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(15)]
        [MinLength(3)]
        public string Word { get; set; }
        public int Count { get; set; }
    }
}
