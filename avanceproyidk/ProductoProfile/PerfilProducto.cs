using AutoMapper;
using avanceproyidk.DataAccess.DBEntities;
using avanceproyidk.Models;

namespace avanceproyidk.ProductoProfile
{
    public class PerfilProducto : Profile
    {
        public PerfilProducto() 
        {
            CreateMap<ProductoEntity, ProductosCatalogosModel>()
                .ForMember(dest => dest.idproducto, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
