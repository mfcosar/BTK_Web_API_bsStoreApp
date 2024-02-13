using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract record BookDtoForManipulation
    {
        [Required(ErrorMessage= "Title is required")]
        [MinLength(2, ErrorMessage = "Title must be at least 2 chars.")]
        [MaxLength(50, ErrorMessage = "Title must be max 50 chars.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(10, 1000, ErrorMessage = "Price must be in the range 10-1000.")]
        public decimal Price { get; set; }
    }
}
