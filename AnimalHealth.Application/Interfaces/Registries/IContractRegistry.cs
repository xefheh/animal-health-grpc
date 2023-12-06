using AnimalHealth.Application.Models;

namespace AnimalHealth.Application.Interfaces.Registries;

public interface IContractRegistry
{
    /// <summary>
    /// Получение контракта по ключевому полю.
    /// </summary>
    /// <param name="lookup">Поисковая модель с ключевым полем.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>gRPC модель контракта.</returns>
    public Task<ContractModel> GetContractAsync(ContractLookup lookup, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение списка контрактов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список gRPC моделей контрактов</returns>
    public Task<ContractModelList> GetContractsAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавление нового контракта.
    /// </summary>
    /// <param name="addedContract">gRPC модель контракта для добавления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Поисковая модель контракта</returns>
    public Task<ContractLookup> AddContractAsync(ContractAddModel addedContract, CancellationToken cancellationToken);
    
    /// <summary>
    /// Изменить поля существующего контракта.
    /// </summary>
    /// <param name="updatedContract">gRPC модель контракта с обновлёнными полями.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Состояние сохранения БД.</returns>
    public Task<DbSaveCondition> UpdateContractAsync(ContractModel updatedContract, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить контракт по ключевому полю.
    /// </summary>
    /// <param name="lookup">Поисковая модель с ключевым полем.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Состояние сохранение БД.</returns>
    public Task<DbSaveCondition> DeleteContractAsync(ContractLookup lookup, CancellationToken cancellationToken);
}