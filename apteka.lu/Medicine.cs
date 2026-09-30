using System;

/// <summary>
/// Класс, описывающий сущность "Лекарство".
/// Реализует строгую инкапсуляцию и проверку правил предметной области.
/// </summary>
public class Medicine
{
    // Приватные поля для хранения данных (скрыты от прямого изменения)
    private int _id;
    private string _name;
    private int _categoryId;
    private int _pharmacistId;
    private decimal _price;
    private int _quantity;

    // Публичные свойства с логикой валидации
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int CategoryId
    {
        get { return _categoryId; }
        set { _categoryId = value; }
    }

    public int PharmacistId
    {
        get { return _pharmacistId; }
        set { _pharmacistId = value; }
    }

    /// <summary>
    /// Стоимость лекарства. Правило предметной области: Price >= 0.
    /// </summary>
    public decimal Price
    {
        get { return _price; }
        set
        {
            // Если переданное значение меньше нуля, выбрасываем ошибку
            if (value < 0)
            {
                throw new ArgumentException("Цена лекарства не может быть отрицательной!");
            }
            _price = value;
        }
    }

    /// <summary>
    /// Количество упаковок на складе. Правило предметной области: Quantity >= 0.
    /// </summary>
    public int Quantity
    {
        get { return _quantity; }
        set
        {
            // Проверка на отрицательное количество упаковок
            if (value < 0)
            {
                throw new ArgumentException("Количество упаковок не может быть меньше нуля!");
            }
            _quantity = value;
        }
    }

    /// <summary>
    /// Вычисляемое свойство (чтение): общая стоимость позиции на складе.
    /// </summary>
    public decimal TotalValue
    {
        get { return _price * _quantity; }
    }

    /// <summary>
    /// Проверяет, находится ли количество ниже заданного порога.
    /// </summary>
    public bool IsLowStock(int threshold)
    {
        return _quantity < threshold;
    }

    /// <summary>
    /// Возвращает строку с базовой информацией о лекарстве.
    /// </summary>
    public string GetInfo()
    {
        return $"{_name} ({_price:0} руб., {_quantity} уп.)";

    }
}
