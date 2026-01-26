using System;

namespace iw16v7.Legacy;

public class BadImageProcessor
{
    public void ProcessImage(string path)
    {
        // 1. Завантаження
        Console.WriteLine($"Завантаження зображення з: {path}");

        // 2. Зміна розміру
        Console.WriteLine("Зміна розміру зображення до 1920x1080...");

        // 3. Фільтрація
        Console.WriteLine("Застосування фільтра 'Sepia'...");

        // 4. Збереження
        Console.WriteLine($"Збереження результату за шляхом: {path}_processed.jpg");
    }
}
