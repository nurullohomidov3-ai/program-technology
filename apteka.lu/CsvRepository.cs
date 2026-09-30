using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

public class CsvRepository
{
    private readonly string _basePath;

    public CsvRepository(string basePath)
    {
        _basePath = basePath;
    }

    /// <summary>
    /// Чтение и ручной парсинг лекарств из CSV-файла.
    /// </summary>
    public List<Medicine> GetMedicines()
    {
        List<Medicine> list = new List<Medicine>();
        string path = Path.Combine(_basePath, "medicines.csv");
        if (!File.Exists(path) || new FileInfo(path).Length == 0) return list;

        string[] lines = File.ReadAllLines(path);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');
            if (parts.Length < 6) continue;

            try
            {
                Medicine item = new Medicine();
                item.Id = int.Parse(parts[0]);
                item.Name = parts[1].Trim();
                item.CategoryId = int.Parse(parts[2]);
                item.PharmacistId = int.Parse(parts[3]);
                item.Price = decimal.Parse(parts[4], CultureInfo.InvariantCulture);
                item.Quantity = int.Parse(parts[5]);

                list.Add(item);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[Ошибка в CSV лекарств, строка {i + 1}]: {ex.Message}. Пропущено.");
                continue;
            }
            catch (Exception)
            {
                Console.WriteLine($"[Ошибка формата CSV лекарств, строка {i + 1}]: Неверный формат. Пропущено.");
                continue;
            }
        }
        return list;
    }

    /// <summary>
    /// Чтение и ручной парсинг категорий из CSV-файла.
    /// </summary>
    public List<Category> GetCategories()
    {
        List<Category> list = new List<Category>();
        string path = Path.Combine(_basePath, "categories.csv");
        if (!File.Exists(path) || new FileInfo(path).Length == 0) return list;

        string[] lines = File.ReadAllLines(path);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');
            if (parts.Length < 3) continue;

            try
            {
                Category item = new Category();
                item.Id = int.Parse(parts[0]);
                item.Name = parts[1].Trim();
                item.Description = parts[2].Trim();

                list.Add(item);
            }
            catch (Exception)
            {
                Console.WriteLine($"[Ошибка формата CSV категорий, строка {i + 1}]. Пропущено.");
                continue;
            }
        }
        return list;
    }

    /// <summary>
    /// Чтение и ручной парсинг фармацевтов из CSV-файла.
    /// </summary>
    public List<Pharmacist> GetPharmacists()
    {
        List<Pharmacist> list = new List<Pharmacist>();
        string path = Path.Combine(_basePath, "pharmacists.csv");
        if (!File.Exists(path) || new FileInfo(path).Length == 0) return list;

        string[] lines = File.ReadAllLines(path);

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');
            if (parts.Length < 3) continue; // Проверяем, что в строке есть хотя бы Id, FullName и Shift

            try
            {
                Pharmacist item = new Pharmacist();
                item.Id = int.Parse(parts[0]);
                item.FullName = parts[1].Trim();
                item.Shift = parts[2].Trim();

                list.Add(item);
            }
            catch (Exception)
            {
                Console.WriteLine($"[Ошибка формата CSV фармацевтов, строка {i + 1}]. Пропущено.");
                continue;
            }

        }
        return list;
    }
}
