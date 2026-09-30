using System;

/// <summary>
/// Фармацевт (провизор), отпускающий лекарства.
/// </summary>
public class Pharmacist
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Shift { get; set; }
    public int Experience { get; set; }

    /// <summary>
    /// Проверяет, является ли фармацевт опытным (стаж > 3 лет).
    /// </summary>
    public bool IsExperienced => Experience > 3;

    /// <summary>
    /// Возвращает форматированную строку с информацией о фармацевте.
    /// </summary>
    public string GetInfo()
    {
        return $"{FullName} ({Experience} лет опыта)";
    }
}
