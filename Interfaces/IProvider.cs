using ElcodeTestTask.Models;

namespace ElcodeTestTask.Interfaces
{
    /// <summary>
    /// Поставщик данных
    /// </summary>
    public interface IProvider
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}