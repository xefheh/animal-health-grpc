using AnimalHealth.Application.Models;
using AnimalHealth.Application.Reports.LocalityDiseaseReport;
using AnimalHealth.Domain.BasicReportEntities;
using AutoMapper;

namespace AnimalHealth.Application.Mapping.ReportValuesMapping
{
    public class DiseaseReportValueMappingProfile : Profile
    {
        public DiseaseReportValueMappingProfile()
        {
            CreateMappingToModelFromEntity();
            CreateMappingToEntityFromModel();
            CreateMappingToGeneralFromSon();
            CreateMappingToSonFromGeneral();
        }

        private void CreateMappingToModelFromEntity()
        {
            CreateMap<DiseaseReportValue, ReportValueModel>()
                .ForMember(model => model.Id, opt => opt.MapFrom(rv => rv.Id))
                .ForMember(model => model.FirstFeature, opt => opt.MapFrom(rv => rv.Locality))
                .ForMember(model => model.SecondFeature, opt => opt.MapFrom(rv => rv.Disease))
                .ForMember(model => model.Count, opt => opt.MapFrom(rv => rv.Count));
        }

        private void CreateMappingToEntityFromModel()
        {
            CreateMap<ReportValueModel, DiseaseReportValue>()
                .ForMember(rv => rv.Id, opt => opt.MapFrom(model => model.Id))
                .ForMember(rv => rv.Locality, opt => opt.MapFrom(model => model.FirstFeature))
                .ForMember(rv => rv.Disease, opt => opt.MapFrom(model => model.SecondFeature))
                .ForMember(rv => rv.Count, opt => opt.MapFrom(model => model.Count));
        }

        private void CreateMappingToGeneralFromSon()
        {
            CreateMap<DiseaseReportValue, ReportValue>()
               .ForMember(model => model.Id, opt => opt.MapFrom(rv => rv.Id))
               .ForMember(model => model.FirstFeature, opt => opt.MapFrom(rv => rv.Locality))
               .ForMember(model => model.SecondFeature, opt => opt.MapFrom(rv => rv.Disease))
               .ForMember(model => model.Count, opt => opt.MapFrom(rv => rv.Count));
        }

        private void CreateMappingToSonFromGeneral()
        {
            CreateMap<ReportValue, DiseaseReportValue>()
               .ForMember(model => model.Id, opt => opt.MapFrom(rv => rv.Id))
               .ForMember(model => model.Locality, opt => opt.MapFrom(rv => rv.FirstFeature))
               .ForMember(model => model.Disease, opt => opt.MapFrom(rv => rv.SecondFeature))
               .ForMember(model => model.Count, opt => opt.MapFrom(rv => rv.Count));
        }
    }
}
