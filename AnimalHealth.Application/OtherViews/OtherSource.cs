using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Interfaces.OtherSource;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Domain.Identity;
using AnimalHealth.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Application.OtherViews;

public class OtherSource : IOtherSource
{
    private readonly AnimalHealthContext _context;
    private readonly IEntityMapper<User, UserModel> _userMapper;
    private readonly IEntityMapper<Animal, AnimalModel> _animalMapper;
    private readonly IEntityMapper<Disease, DiseaseModel> _diseaseMapper;
    private readonly IEntityMapper<Vaccine, VaccineModel> _vaccineMapper;
    private readonly IEntityMapper<Locality, LocalityModel> _localityMapper;
    
    public OtherSource(AnimalHealthContext context, IEntityMapper<User, UserModel> userMapper,
        IEntityMapper<Animal, AnimalModel> animalMapper,
        IEntityMapper<Disease, DiseaseModel> diseaseMapper,
        IEntityMapper<Vaccine, VaccineModel> vaccineMapper,
        IEntityMapper<Locality, LocalityModel> localityMapper)
    {
        _context = context;
        _userMapper = userMapper;
        _animalMapper = animalMapper;
        _diseaseMapper = diseaseMapper;
        _vaccineMapper = vaccineMapper;
        _localityMapper = localityMapper;
    }

    public async Task<AnimalModelList> GetAnimalsAsync(CancellationToken cancellationToken)
    {
        var animals = await _context.Animals.ToListAsync(cancellationToken);
        var animalModels = animals.Select(animal => _animalMapper.Map(animal));
        var animalModelList = new AnimalModelList();
        animalModelList.Animals.AddRange(animalModels);
        return animalModelList;
    }
    
    public async Task<DiseaseModelList> GetDiseasesAsync(CancellationToken cancellationToken)
    {
        var diseases = await _context.Diseases.ToListAsync(cancellationToken);
        var diseaseModels = diseases.Select(disease => _diseaseMapper.Map(disease));
        var diseaseModelList = new DiseaseModelList();
        diseaseModelList.Diseases.AddRange(diseaseModels);
        return diseaseModelList;
    }
    
    public async Task<VaccineModelList> GetVaccinesAsync(CancellationToken cancellationToken)
    {
        var vaccines = await _context.Vaccines.ToListAsync(cancellationToken);
        var vaccineModels = vaccines.Select(vaccine => _vaccineMapper.Map(vaccine));
        var vaccineModelList = new VaccineModelList();
        vaccineModelList.Vaccines.AddRange(vaccineModels);
        return vaccineModelList;
    }
    
    public async Task<LocalityModelList> GetLocalitiesAsync(CancellationToken cancellationToken)
    {
        var localities = await _context.Localities.ToListAsync(cancellationToken);
        var localityModels = localities.Select(locality => _localityMapper.Map(locality));
        var localityModelList = new LocalityModelList();
        localityModelList.Localities.AddRange(localityModels);
        return localityModelList;
    }
    
    public async Task<UserModelList> GetUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _context.Users.ToListAsync(cancellationToken);
        var userModels = users.Select(user => _userMapper.Map(user));
        var userModelList = new UserModelList();
        userModelList.Users.AddRange(userModels);
        return userModelList;
    }
}