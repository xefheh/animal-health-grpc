using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Domain.Identity;
using AnimalHealth.Persistence;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Mapping.EntityMappings;

public class VaccinationMapper : IEntityMapper<Vaccination, VaccinationAddModel, VaccinationModel>
{ 
    private readonly AnimalHealthContext _context;
    private readonly IEntityMapper<User, UserModel> _userMapper;
    private readonly IEntityMapper<Animal, AnimalModel> _animalMapper;
    private readonly IEntityMapper<Contract, ContractAddModel, ContractModel> _contractMapper;
    private readonly IEntityMapper<Vaccine, VaccineModel> _vaccineMapper;

    public VaccinationMapper(AnimalHealthContext context, IEntityMapper<User, UserModel> userMapper,
        IEntityMapper<Animal, AnimalModel> animalMapper,
        IEntityMapper<Contract, ContractAddModel, ContractModel> contractMapper,
        IEntityMapper<Vaccine, VaccineModel> vaccineMapper)
    {
        _context = context;
        _userMapper = userMapper;
        _animalMapper = animalMapper;
        _contractMapper = contractMapper;
        _vaccineMapper = vaccineMapper;
    }

    public Vaccination Map(VaccinationModel model) => new()
    {
        Id = model.Id,
        Date = model.Date.ToDateTime(),
        ExpirationDate = model.ExpirationDate.ToDateTime(),
        User = _context.Users.Find(model.User.Id) ?? throw new NotFoundException
            (typeof(User), model.User.Id),
        Animal = _context.Animals.Find(model.Animal.RegNumber) ??
                 throw new NotFoundException(typeof(Animal), model.Animal.RegNumber),
        Contract = _context.Contracts.Find(model.Contract.Id) ??
                   throw new NotFoundException(typeof(Locality), model.Contract.Id),
        Vaccine = _context.Vaccines.Find(model.Vaccine.Serial) ??
                  throw new NotFoundException(typeof(Vaccine), model.Vaccine.Serial)
    };

    public VaccinationModel Map(Vaccination entity) => new()
    {
        Id = entity.Id,
        Date = entity.Date.ToTimestamp(),
        ExpirationDate = entity.ExpirationDate.ToTimestamp(),
        User = _userMapper.Map(entity.User),
        Animal = _animalMapper.Map(entity.Animal),
        Contract = _contractMapper.Map(entity.Contract),
        Vaccine = _vaccineMapper.Map(entity.Vaccine)
    };

    public Vaccination Map(VaccinationAddModel model) => new()
    {
        Date = model.Date.ToDateTime(),
        ExpirationDate = model.ExpirationDate.ToDateTime(),
        User = _context.Users.Find(model.User.Id) ?? throw new NotFoundException
            (typeof(User), model.User.Id),
        Animal = _context.Animals.Find(model.Animal.RegNumber) ??
                 throw new NotFoundException(typeof(Animal), model.Animal.RegNumber),
        Contract = _context.Contracts.Find(model.Contract.Id) ??
                   throw new NotFoundException(typeof(Locality), model.Contract.Id),
        Vaccine = _context.Vaccines.Find(model.Vaccine.Serial) ??
                  throw new NotFoundException(typeof(Vaccine), model.Vaccine.Serial)
    };
}