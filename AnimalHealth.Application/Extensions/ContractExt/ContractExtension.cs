using AnimalHealth.Domain.Entities;

namespace AnimalHealth.Application.Extensions.ContractExt;

public static class ContractExtension
{
    /// <summary>
    /// Обновление полей существующего объекта.
    /// </summary>
    /// <param name="oldContract">Существующий объект.</param>
    /// <param name="updatedContract">Объект с новыми полями.</param>
    public static void UpdateFields(this Contract oldContract, Contract updatedContract)
    {
        oldContract.Number = updatedContract.Number;
        oldContract.ConclusionDate = updatedContract.ConclusionDate;
        oldContract.EndDate = updatedContract.EndDate;
        oldContract.Executor = updatedContract.Executor;
        oldContract.Customer = updatedContract.Customer;
    }
}