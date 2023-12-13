using System.Reflection;

namespace AnimalHealth.Domain.Abstract;

public class UpdatableEntity<TEntity> where TEntity : UpdatableEntity<TEntity>
{
    private readonly PropertyInfo[] _properties =
        typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

    public void UpdateFields(TEntity updatedEntity)
    {
        var oldEntity = this as TEntity;
        foreach (var property in _properties)
        {
            var updatedProperty = property.GetValue(updatedEntity);
            property.SetValue(oldEntity, updatedProperty);
        }
    }
}