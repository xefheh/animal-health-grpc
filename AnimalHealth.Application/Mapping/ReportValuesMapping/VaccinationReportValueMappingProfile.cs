﻿using AnimalHealth.Application.Models;
using AnimalHealth.Application.Reports.LocalityVaccinationReport;
using AnimalHealth.Domain.BasicReportEntities;
using AutoMapper;

namespace AnimalHealth.Application.Mapping.ReportValuesMapping
{
    public class VaccinationReportValueMappingProfile : Profile
    {
        public VaccinationReportValueMappingProfile()
        {
            CreateMappingToModelFromEntity();
            CreateMappingToEntityFromModel();
            CreateMappingToGeneralFromSon();
            CreateMappingToSonFromGeneral();
        }

        private void CreateMappingToModelFromEntity()
        {
            CreateMap<VaccinationReportValue, ReportValueModel>()
                .ForMember(model => model.Id, opt => opt.MapFrom(rv => rv.Id))
                .ForMember(model => model.FirstFeature, opt => opt.MapFrom(rv => rv.Locality))
                .ForMember(model => model.SecondFeature, opt => opt.MapFrom(rv => rv.Vaccine))
                .ForMember(model => model.Count, opt => opt.MapFrom(rv => rv.Count));
        }

        private void CreateMappingToEntityFromModel()
        {
            CreateMap<ReportValueModel, VaccinationReportValue>()
                .ForMember(rv => rv.Id, opt => opt.MapFrom(model => model.Id))
                .ForMember(rv => rv.Locality, opt => opt.MapFrom(model => model.FirstFeature))
                .ForMember(rv => rv.Vaccine, opt => opt.MapFrom(model => model.SecondFeature))
                .ForMember(rv => rv.Count, opt => opt.MapFrom(model => model.Count));
        }

        private void CreateMappingToGeneralFromSon()
        {
            CreateMap<VaccinationReportValue, ReportValue>()
               .ForMember(model => model.Id, opt => opt.MapFrom(rv => rv.Id))
               .ForMember(model => model.FirstFeature, opt => opt.MapFrom(rv => rv.Locality))
               .ForMember(model => model.SecondFeature, opt => opt.MapFrom(rv => rv.Vaccine))
               .ForMember(model => model.Count, opt => opt.MapFrom(rv => rv.Count));
        }

        private void CreateMappingToSonFromGeneral()
        {
            CreateMap<ReportValue, VaccinationReportValue>()
               .ForMember(model => model.Id, opt => opt.MapFrom(rv => rv.Id))
               .ForMember(model => model.Locality, opt => opt.MapFrom(rv => rv.FirstFeature))
               .ForMember(model => model.Vaccine, opt => opt.MapFrom(rv => rv.SecondFeature))
               .ForMember(model => model.Count, opt => opt.MapFrom(rv => rv.Count));
        }
    }
}
