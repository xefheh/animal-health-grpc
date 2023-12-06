using AnimalHealth.Application.Models;

namespace AnimalHealth.Application.Interfaces.OtherSource;

public interface IOtherSource
{
    /// <summary>
    /// Получить список животных.
    /// </summary>
    /// <returns>Список gRPC моделей животных.</returns>
    Task<AnimalModelList> GetAnimalsAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить список болезней.
    /// </summary>
    /// <returns>Список gRPC моделей болезней.</returns>
    Task<DiseaseModelList> GetDiseasesAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить список вакцин.
    /// </summary>
    /// <returns>Список gRPC моделей вакцин.</returns>
    Task<VaccineModelList> GetVaccinesAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить список городов.
    /// </summary>
    /// <returns>Список gRPC моделей городов.</returns>
    Task<LocalityModelList> GetLocalitiesAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить список пользователей.
    /// </summary>
    /// <returns>Список gRPC моделей пользователей.</returns>
    Task<UserModelList> GetUsersAsync(CancellationToken cancellationToken);

}