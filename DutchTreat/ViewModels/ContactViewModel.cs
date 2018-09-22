using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.ViewModels
{
	public class ContactViewModel
	{
		[Required]
		[MaxLength(140)]
		public string Message { get; set; }
	}
}
