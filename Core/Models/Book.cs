using Core.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Core.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Book_ValidatePublishDate]
        public DateTime PublishDate { get; set; }
        [JsonIgnore]
        public Author? Author { get; set; }
        public int AuthorId { get; set; }
        public string Publisher { get; set; }

        public bool ValidatePublishDate() =>
            PublishDate < DateTime.Now && PublishDate > new DateTime(1900, 1, 1);

        public bool ValidatePrice() =>
            Price > 0;
    }
}
