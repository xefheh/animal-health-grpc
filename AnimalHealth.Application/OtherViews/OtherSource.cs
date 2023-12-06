using AnimalHealth.Application.Interfaces.OtherSource;
using AnimalHealth.Application.Models;
using AnimalHealth.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Application.OtherViews;

public class OtherSource : IOtherSource
{
    private readonly AnimalHealthContext _context;
    private readonly IMapper _mapper;
    
    public OtherSource(AnimalHealthContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

    public async Task<AnimalModelList> GetAnimalsAsync(CancellationToken cancellationToken)
    {
        var animals = await _context.Animals.ToListAsync(cancellationToken);
        var animalModels = animals.Select(animal => _mapper.Map<AnimalModel>(animal));
        var animalModelList = new AnimalModelList();
        animalModelList.Animals.AddRange(animalModels);
        return animalModelList;
    }
    
    public async Task<DiseaseModelList> GetDiseasesAsync(CancellationToken cancellationToken)
    {
        var diseases = await _context.Diseases.ToListAsync(cancellationToken);
        var diseaseModels = diseases.Select(disease => _mapper.Map<DiseaseModel>(disease));
        var diseaseModelList = new DiseaseModelList();
        diseaseModelList.Diseases.AddRange(diseaseModels);
        return diseaseModelList;
    }
    
    public async Task<VaccineModelList> GetVaccinesAsync(CancellationToken cancellationToken)
    {
        var vaccines = await _context.Vaccines.ToListAsync(cancellationToken);
        var vaccineModels = vaccines.Select(vaccine => _mapper.Map<VaccineModel>(vaccine));
        var vaccineModelList = new VaccineModelList();
        vaccineModelList.Vaccines.AddRange(vaccineModels);
        return vaccineModelList;
    }
    
    public async Task<LocalityModelList> GetLocalitiesAsync(CancellationToken cancellationToken)
    {
        var localities = await _context.Localities.ToListAsync(cancellationToken);
        var localityModels = localities.Select(locality => _mapper.Map<LocalityModel>(locality));
        var localityModelList = new LocalityModelList();
        localityModelList.Localities.AddRange(localityModels);
        return localityModelList;
    }
    
    public async Task<UserModelList> GetUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _context.Users.ToListAsync(cancellationToken);
        var userModels = users.Select(user => _mapper.Map<UserModel>(user));
        var userModelList = new UserModelList();
        userModelList.Users.AddRange(userModels);
        return userModelList;
    }
}