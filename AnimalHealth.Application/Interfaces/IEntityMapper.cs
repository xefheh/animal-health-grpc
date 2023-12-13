namespace AnimalHealth.Application.Interfaces;

/// <summary>
/// Интерфейс мэппера.
/// </summary>
/// <typeparam name="TEntity">Сущность.</typeparam>
/// <typeparam name="TAddModel">Модель добавления gRPC.</typeparam>
/// <typeparam name="TModel">Модель gRPC.</typeparam>
public interface IEntityMapper<TEntity, TAddModel, TModel> : IEntityMapper<TEntity, TModel>
{
    /// <summary>
    /// Перевести модель добавления gRPC в сущность.
    /// </summary>
    /// <param name="model">Модель добавления gRPC.</param>
    /// <returns>Сущность.</returns>
    TEntity Map(TAddModel model);
}

/// <summary>
/// Интерфейс мэппера.
/// </summary>
/// <typeparam name="TEntity">Сущность.</typeparam>
/// <typeparam name="TModel">Модель gRPC.</typeparam>
public interface IEntityMapper<TEntity, TModel>
{
    /// <summary>
    /// Перевести модель gRPC в сущность.
    /// </summary>
    /// <param name="model">Модель gRPC.</param>
    /// <returns>Сущность.</returns>
    TEntity Map(TModel model);
    
    /// <summary>
    /// Перевести сущность в модель gRPC.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель gRPC=.</returns>
    TModel Map(TEntity entity);
}

/// <summary>
/// Интерфейс мэппера.
/// </summary>
/// <typeparam name="TEntity">Сущность.</typeparam>
/// <typeparam name="TModel">Модель.</typeparam>
public interface IMapper<TEntity, TModel>
{
    /// <summary>
    /// Перевести модель в сущность.
    /// </summary>
    /// <param name="model">Модель.</param>
    /// <returns>Сущность.</returns>
    TEntity Map(TModel model);

    /// <summary>
    /// Перевести сущность в модель.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Модель gRPC=.</returns>
    TModel Map(TEntity entity);
}