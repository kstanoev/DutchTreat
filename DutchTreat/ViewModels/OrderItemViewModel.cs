using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.ViewModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
    }
}
