using System.Reflection;

namespace AnimalHealth.Application.Exceptions;

public class NotFoundException : Exception
{
    /// <summary>
    /// Конструктор исключения с указанием конкретного типа и значения ключевого поля этого типа.
    /// </summary>
    /// <param name="type">Тип, объект которого не был найден.</param>
    /// <param name="key">Значение ключевого поля.</param>
    public NotFoundException(MemberInfo type, object key) : base($"{type.Name} with key {key} not founded!") { }
}