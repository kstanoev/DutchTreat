using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Order, OrderViewModel>()
				.ForMember(destinationVM => destinationVM.OrderId,
					mapper => mapper.MapFrom(sourceModel => sourceModel.Id))
				.ReverseMap();

			CreateMap<OrderItem, OrderItemViewModel>()
				.ReverseMap();
		}
	}
}
