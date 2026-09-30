using System;
using System.Collections.Generic;

/// <summary>
/// Репозиторий для работы с данными в оперативной памяти.
/// </summary>
public class InMemoryRepository
{
    private List<Category> _categories;
    private List<Pharmacist> _pharmacists;
    private List<Medicine> _medicines;

    public InMemoryRepository()
    {
        // 1. Заполняем категории (5 штук по заданию)
        _categories = new List<Category>
        {
            new Category { Id = 1, Name = "Обезболивающие", Description = "От боли и жара" },
            new Category { Id = 2, Name = "Антибиотики", Description = "Борьба с бактериями" },
            new Category { Id = 3, Name = "Витамины", Description = "Поддержание иммунитета" },
            new Category { Id = 4, Name = "Сердечные", Description = "Для работы сердца" },
            new Category { Id = 5, Name = "Успокоительные", Description = "Для нормализации сна" }
        };

        // 2. Заполняем фармацевтов (5 штук по заданию)
        _pharmacists = new List<Pharmacist>
        {
            new Pharmacist { Id = 1, FullName = "Иванова А.А.", Shift = "Дневная", Experience = 5 },
            new Pharmacist { Id = 2, FullName = "Петров П.П.", Shift = "Ночная", Experience = 2 },
            new Pharmacist { Id = 3, FullName = "Сидорова С.С.", Shift = "Дневная", Experience = 10 },
            new Pharmacist { Id = 4, FullName = "Кузнецова Е.В.", Shift = "Ночная", Experience = 1 },
            new Pharmacist { Id = 5, FullName = "Смирнов К.А.", Shift = "Дневная", Experience = 4 }
        };

        // 3. Заполняем лекарства (связываем их через CategoryId и PharmacistId)
        _medicines = new List<Medicine>
        {
            new Medicine { Id = 1, Name = "Аспирин", CategoryId = 1, PharmacistId = 1, Price = 50m, Quantity = 100 },
            new Medicine { Id = 2, Name = "Амоксициллин", CategoryId = 2, PharmacistId = 1, Price = 250m, Quantity = 15 }, // меньше порога (20)
            new Medicine { Id = 3, Name = "Витамин C", CategoryId = 3, PharmacistId = 3, Price = 120m, Quantity = 200 },
            new Medicine { Id = 4, Name = "Валидол", CategoryId = 4, PharmacistId = 2, Price = 80m, Quantity = 8 },   // меньше порога (20)
            new Medicine { Id = 5, Name = "Новопассит", CategoryId = 5, PharmacistId = 5, Price = 450m, Quantity = 35 }
        };
    }

    public List<Category> GetCategories() => _categories;
    public List<Pharmacist> GetPharmacists() => _pharmacists;
    public List<Medicine> GetMedicines() => _medicines;
}
