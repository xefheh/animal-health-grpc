using AnimalHealth.Application.Models;
using AnimalHealth.Application.Reports.LocalityDiseaseReport;
using AnimalHealth.Application.Reports.LocalityVaccinationReport;
using AnimalHealth.Domain.BasicReportEntities;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Mapping.ReportMappings
{
    public class VaccinationReportMappingProfile : Profile
    {
        public VaccinationReportMappingProfile()
        {
            CreateMappingToModelFromEntity();
            CreateMappingToEntityFromModel();
            CreateMappingToGeneralFromSon();
            CreateMappingToSonFromGeneral();
        }

        private void CreateMappingToModelFromEntity()
        {
            CreateMap<VaccinationReport, ReportModel>()
                .ForMember(model => model.Id, opt => opt.MapFrom(dreport => dreport.Id))
                .ForMember(model => model.State, opt => opt.MapFrom(dreport => dreport.State))
                .ForMember(model => model.CreateDate, opt => opt.MapFrom(dreport => dreport.CreateDate.ToTimestamp()))
                .ForMember(model => model.Type, opt => opt.MapFrom(dreport => dreport.Type))
                .ForMember(model => model.UserCreator, opt => opt.MapFrom(dreport => dreport.User))
                .ForMember(model => model.Values, opt => opt.MapFrom(dreport => dreport.Values));
        }

        private void CreateMappingToEntityFromModel()
        {
            CreateMap<ReportModel, VaccinationReport>()
                .ForMember(dreport => dreport.Id, opt => opt.MapFrom(model => model.Id))
                .ForMember(dreport => dreport.State, opt => opt.MapFrom(model => model.State))
                .ForMember(dreport => dreport.CreateDate, opt => opt.MapFrom(model => model.CreateDate.ToDateTime()))
                .ForMember(dreport => dreport.Type, opt => opt.MapFrom(model => model.Type))
                .ForMember(dreport => dreport.User, opt => opt.MapFrom(model => model.UserCreator))
                .ForMember(dreport => dreport.Values, opt => opt.MapFrom(model => model.Values));
        }

        private void CreateMappingToGeneralFromSon()
        {
            CreateMap<Report, VaccinationReport>()
                .ForMember(dreport => dreport.Id, opt => opt.MapFrom(model => model.Id))
                .ForMember(dreport => dreport.State, opt => opt.MapFrom(model => model.State))
                .ForMember(dreport => dreport.CreateDate, opt => opt.MapFrom(model => model.CreateDate))
                .ForMember(dreport => dreport.Type, opt => opt.MapFrom(model => model.Type))
                .ForMember(dreport => dreport.User, opt => opt.MapFrom(model => model.User))
                .ForMember(dreport => dreport.Values, opt => opt.MapFrom(model => model.Values));
        }

        private void CreateMappingToSonFromGeneral()
        {
            CreateMap<VaccinationReport, Report>()
                .ForMember(dreport => dreport.Id, opt => opt.MapFrom(model => model.Id))
                .ForMember(dreport => dreport.State, opt => opt.MapFrom(model => model.State))
                .ForMember(dreport => dreport.CreateDate, opt => opt.MapFrom(model => model.CreateDate))
                .ForMember(dreport => dreport.Type, opt => opt.MapFrom(model => model.Type))
                .ForMember(dreport => dreport.User, opt => opt.MapFrom(model => model.User))
                .ForMember(dreport => dreport.Values, opt => opt.MapFrom(model => model.Values));
        }
    }
}
