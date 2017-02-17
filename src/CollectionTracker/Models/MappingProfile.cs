using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using swdestinydb.Models;

namespace CollectionTracker.Models
{
    public class MappingProfile : Profile
    {
	    public MappingProfile()
	    {
		    CreateMap<Card, CardViewModel>(MemberList.Source);
	    }
    }
}
