using System;

/// <summary>
/// Категория лекарственных средств.
/// </summary>
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// Вычисляемое свойство для красивого вывода информации.
    /// </summary>
    public string Info => $"{Name} — {Description}";
}
