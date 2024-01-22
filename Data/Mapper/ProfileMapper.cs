using AutoMapper;
using Data.Models;
using Data.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapper
{
    public class ProfileMapper:Profile
    {
        public ProfileMapper()
        {
            CreateMap<UsuarioModelView, Usuario>().ReverseMap();
            CreateMap<SalaModelView, Sala>().ReverseMap();
            CreateMap<Mensaje, MessagesMV>()
                .ForMember(d => d.UserNick, o => o.MapFrom( m => m.UsuarioNavigation.NickName));

        }
    }
}
