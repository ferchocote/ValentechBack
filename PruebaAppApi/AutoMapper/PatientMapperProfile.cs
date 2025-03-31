using AplicationServices.DTOs.Patient;
using AutoMapper;

namespace PruebaAppApi.AutoMapper
{
    public class PatientMapperProfile : Profile
    {
        public PatientMapperProfile()
        {
            FromPatientToGetAllPatientDto();
            FromSavePatientDtoToPatient();
        }

        private void FromPatientToGetAllPatientDto()
        {
            _ = CreateMap<PruebaAppApi.DataAccess.Entities.Patient, GetAllPatientDto>()
                 .ForMember(target => target.Id, opt => opt.MapFrom(source => source.ID))
                 .ForMember(target => target.FullName, opt => opt.MapFrom(source => string.Concat(source.FirstName, " ", source.LastName)))
                 .ForMember(target => target.DocumentNumber, opt => opt.MapFrom(source => source.DocumentNumber))
                 .ForMember(target => target.Email, opt => opt.MapFrom(source => source.Email))
                 .ForMember(target => target.Phone, opt => opt.MapFrom(source => source.Phone))
                 .ForMember(target => target.Disease, opt => opt.MapFrom(source => source.Disease));
        }

        private void FromSavePatientDtoToPatient()
        {
            _ = CreateMap<SavePatientDto, PruebaAppApi.DataAccess.Entities.Patient>()
                 .ForMember(target => target.ID, opt => opt.MapFrom(source => Guid.NewGuid()))
                 .ForMember(target => target.FirstName, opt => opt.MapFrom(source => source.FirstName))
                 .ForMember(target => target.LastName, opt => opt.MapFrom(source => source.LastName))
                 .ForMember(target => target.DocumentNumber, opt => opt.MapFrom(source => source.DocumentNumber))
                 .ForMember(target => target.Email, opt => opt.MapFrom(source => source.Email))
                 .ForMember(target => target.Phone, opt => opt.MapFrom(source => source.Phone))
                 .ForMember(target => target.Disease, opt => opt.MapFrom(source => source.Disease))
                 .ForMember(target => target.State, opt => opt.MapFrom(source => true))
                 .ForMember(target => target.CreationUser, opt => opt.MapFrom(source => source.CreationUser))
                 .ForMember(target => target.CreationDate, opt => opt.MapFrom(source => DateTime.Now));
        }

    }
}
