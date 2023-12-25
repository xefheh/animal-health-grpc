using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Mapping.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Domain.Identity;
using AnimalHealth.Persistence;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Mapping.EntityMappings;

public class InspectionMapper : IEntityMapper<Inspection, InspectionAddModel, InspectionModel>
{
    private readonly AnimalHealthContext _context;
    private readonly IEntityMapper<User, UserModel> _userMapper;
    private readonly IEntityMapper<Animal, AnimalModel> _animalMapper;
    private readonly IEntityMapper<Disease, DiseaseModel> _diseaseMapper;
    private readonly IEntityMapper<Contract, ContractAddModel, ContractModel> _contractMapper;

    public InspectionMapper(AnimalHealthContext context, IEntityMapper<User, UserModel> userMapper,
        IEntityMapper<Animal, AnimalModel> animalMapper,
        IEntityMapper<Disease, DiseaseModel> diseaseMapper,
        IEntityMapper<Contract, ContractAddModel, ContractModel> contractMapper)
    {
        _context = context;
        _userMapper = userMapper;
        _animalMapper = animalMapper;
        _diseaseMapper = diseaseMapper;
        _contractMapper = contractMapper;
    }
        
    
    
    public Inspection Map(InspectionModel model) => new()
    {
        Id = model.Id,
        FeatureBehaviour = model.FeatureBehaviour,
        AnimalCondition = model.AnimalCondition,
        Temperature = model.Temperature,
        SkinCover = model.SkinCover,
        FurCondition = model.FurCondition,
        Injures = model.Injures,
        IsNeedOperations = model.IsNeedOperations,
        Manipulations = model.Manipulations,
        Treatment = model.Treatment,
        Date = model.Date.ToDateTime(),
        User = _context.Users.Find(model.User.Id) ?? throw new NotFoundException(typeof(User), model.User.Id),
        InspectedAnimal = _context.Animals.Find(model.Animal.RegNumber) ??
                          throw new NotFoundException(typeof(Animal), model.Animal.RegNumber),
        Contract = _context.Contracts.Find(model.Contract.Id) ??
                   throw new NotFoundException(typeof(Locality), model.Contract.Id),
        Disease = _context.Diseases.Find(model.Disease.Id) ??
                  throw new NotFoundException(typeof(Disease), model.Disease.Id)
    };

    public InspectionModel Map(Inspection entity) => new()
    {
        Id = entity.Id,
        FeatureBehaviour = entity.FeatureBehaviour,
        AnimalCondition = entity.AnimalCondition,
        Temperature = entity.Temperature,
        SkinCover = entity.SkinCover,
        FurCondition = entity.FurCondition,
        Injures = entity.Injures,
        IsNeedOperations = entity.IsNeedOperations,
        Manipulations = entity.Manipulations,
        Treatment = entity.Treatment,
        Date = entity.Date.ToTimestamp(),
        User = _userMapper.Map(entity.User),
        Animal = _animalMapper.Map(entity.InspectedAnimal),
        Contract = _contractMapper.Map(entity.Contract),
        Disease = (entity.Disease == null) ? null : _diseaseMapper.Map(entity.Disease)
    };

    public Inspection Map(InspectionAddModel model) => new()
    {
        FeatureBehaviour = model.FeatureBehaviour,
        AnimalCondition = model.AnimalCondition,
        Temperature = model.Temperature,
        SkinCover = model.SkinCover,
        FurCondition = model.FurCondition,
        Injures = model.Injures,
        IsNeedOperations = model.IsNeedOperations,
        Manipulations = model.Manipulations,
        Treatment = model.Treatment,
        Date = model.Date.ToDateTime(),
        User = _context.Users.Find(model.User.Id) ?? throw new NotFoundException(typeof(User), model.User.Id),
        InspectedAnimal = _context.Animals.Find(model.Animal.RegNumber) ??
                          throw new NotFoundException(typeof(Animal), model.Animal.RegNumber),
        Contract = _context.Contracts.Find(model.Contract.Id) ??
                   throw new NotFoundException(typeof(Locality), model.Contract.Id),
        Disease = _context.Diseases.Find(model.Disease.Id) ??
                  throw new NotFoundException(typeof(Disease), model.Disease.Id)
    };
}