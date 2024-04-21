using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    /* public record BookDtoForUpdate
   {
      public int Id { get; init; } //init deyince: immutable ve değer verilince değişmez oldu
       public String Title { get; init; }
       public decimal Price { get; init; }
   }*/

    public record BookDtoForUpdate: BookDtoForManipulation        //(int Id, String Title, decimal Price);
    {
        [Required]
        public int Id { get; init; }

        [Required(ErrorMessage = "Category Id is required")]
        public int CategoryId { get; init; }
    }
}
