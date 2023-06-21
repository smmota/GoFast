using AutoMapper;
using GoFast.API.Models;
using GoFast.API.Models.InputModel;
using GoFast.API.Models.ViewModels;
using System.Runtime.InteropServices;

namespace GoFast.API.Data.Mappers
{
    public class MappersProfile : Profile
    {
        public MappersProfile()
        {
            CreateMap<MotoristaViewModel, MotoristaInputModel>().ReverseMap();
            CreateMap<CarroViewModel, CarroInputModel>().ReverseMap();
            CreateMap<DocumentoViewModel, DocumentoInputModel>().ReverseMap();
            CreateMap<DocumentoCarroViewModel, DocumentoCarroInputModel>().ReverseMap();
            CreateMap<EnderecoViewModel, EnderecoInputModel>().ReverseMap();

            CreateMap<Motorista, MotoristaViewModel>().ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<Carro, CarroViewModel>().ReverseMap();
            CreateMap<Documento, DocumentoViewModel>().ReverseMap();
            CreateMap<DocumentoCarro, DocumentoCarroViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<BlobStorage, BlobStorageViewModel>().ReverseMap();

            AddGlobalIgnore("Id");
        }
    }
}
