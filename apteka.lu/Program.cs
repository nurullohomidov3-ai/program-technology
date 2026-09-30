using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        List<Category> categories = null;
        List<Pharmacist> pharmacists = null;
        List<Medicine> medicines = null;

        Console.WriteLine("========================================");
        Console.WriteLine("  АПТЕКА (ЛАБОРАТОРНАЯ РАБОТА, ВАР. 25) ");
        Console.WriteLine("========================================");
        Console.WriteLine("Выберите источник данных:");
        Console.WriteLine("1 - Загрузка из InMemoryRepository (В памяти)");
        Console.WriteLine("2 - Загрузка из CsvRepository (С диска)");
        Console.Write("\nВаш выбор: ");

        string input = Console.ReadLine();

        // Путь к папке "data" рядом с запускаемым .exe файлом программы
        string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

        if (input == "1")
        {
            InMemoryRepository inMemory = new InMemoryRepository();
            categories = inMemory.GetCategories();
            pharmacists = inMemory.GetPharmacists();
            medicines = inMemory.GetMedicines();
            Console.WriteLine("\n[Успешно]: Данные загружены из памяти.\n");
        }
        else if (input == "2")
        {
            // Проверяем, существует ли вообще папка с файлами
            if (!Directory.Exists(dataFolder))
            {
                Console.WriteLine($"\n[Ошибка]: Не найдена папка с файлами по пути:\n{dataFolder}");
                Console.WriteLine("Пожалуйста, создайте папку 'data' и положите туда CSV-файлы.");
                Console.ReadLine();
                return;
            }

            CsvRepository csvRepo = new CsvRepository(dataFolder);
            categories = csvRepo.GetCategories();
            pharmacists = csvRepo.GetPharmacists();
            medicines = csvRepo.GetMedicines();
            Console.WriteLine("\n[Успешно]: Данные загружены из CSV-файлов.\n");
        }
        else
        {
            Console.WriteLine("\n[Ошибка]: Неверный выбор.");
            Console.ReadLine();
            return;
        }

        // ==========================================
        // ДЕМОНСТРАЦИЯ РАБОТЫ АНАЛИТИЧЕСКИХ МЕТОДОВ
        // ==========================================

        // 1. Поиск фармацевта по названию лекарства
        string searchMed1 = "Амоксициллин";
        Pharmacist ph = FindPharmacist(searchMed1, medicines, pharmacists);
        Console.WriteLine($"1. Поиск фармацевта для \"{searchMed1}\": " + (ph != null ? ph.GetInfo() : "Не найден"));

        // 2. Поиск категории по названию лекарства
        string searchMed2 = "Валидол";
        Category cat = FindCategory(searchMed2, medicines, categories);
        Console.WriteLine($"2. Поиск категории для \"{searchMed2}\": " + (cat != null ? cat.Info : "Не найдена"));

        // 3. Суммарное количество упаковок
        int totalQty = GetTotalQuantity(medicines);
        Console.WriteLine($"3. Общее количество упаковок на складе: {totalQty} шт.");

        // 4. Лекарства ниже порога (меньше 20 штук)
        int threshold = 20;
        List<Medicine> lowStock = GetLowStockMedicines(threshold, medicines);
        Console.Write($"4. Лекарства, которых осталось меньше {threshold} уп.: ");
        foreach (var item in lowStock)
        {
            Console.Write($"{item.Name} ({item.Quantity} уп.) ");
        }
        Console.WriteLine();

        // 5. Вывод всех лекарств со связями
        Console.WriteLine("\n5. Полный список лекарств:");
        PrintAllMedicines(medicines, categories, pharmacists);

        Console.WriteLine("\nПрограмма успешно завершена. Нажмите Enter для выхода...");
        Console.ReadLine();
    }

    // Аналитические методы без LINQ
    static Pharmacist FindPharmacist(string medicineName, List<Medicine> medicines, List<Pharmacist> pharmacists)
    {
        Medicine foundMedicine = null;
        foreach (var med in medicines)
        {
            if (med.Name == medicineName) { foundMedicine = med; break; }
        }
        if (foundMedicine == null) return null;
        foreach (var p in pharmacists)
        {
            if (p.Id == foundMedicine.PharmacistId) return p;
        }
        return null;
    }

    static Category FindCategory(string medicineName, List<Medicine> medicines, List<Category> categories)
    {
        Medicine foundMedicine = null;
        foreach (var med in medicines)
        {
            if (med.Name == medicineName) { foundMedicine = med; break; }
        }
        if (foundMedicine == null) return null;
        foreach (var c in categories)
        {
            if (c.Id == foundMedicine.CategoryId) return c;
        }
        return null;
    }

    static int GetTotalQuantity(List<Medicine> medicines)
    {
        int total = 0;
        foreach (var med in medicines) { total += med.Quantity; }
        return total;
    }

    static List<Medicine> GetLowStockMedicines(int threshold, List<Medicine> medicines)
    {
        List<Medicine> result = new List<Medicine>();
        foreach (var med in medicines)
        {
            if (med.IsLowStock(threshold)) result.Add(med);
        }
        return result;
    }

    static void PrintAllMedicines(List<Medicine> medicines, List<Category> categories, List<Pharmacist> pharmacists)
    {
        foreach (var med in medicines)
        {
            string categoryName = "—";
            foreach (var cat in categories)
            {
                if (cat.Id == med.CategoryId) { categoryName = cat.Name; break; }
            }
            string pharmacistName = "—";
            foreach (var ph in pharmacists)
            {
                if (ph.Id == med.PharmacistId) { pharmacistName = ph.FullName; break; }
            }
            Console.WriteLine($"\"{med.GetInfo()}\" — фармацевт {pharmacistName}, категория \"{categoryName}\"");
        }
    }
}
