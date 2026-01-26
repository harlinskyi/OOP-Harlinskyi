using System;
using iw16v7.Interfaces;
using iw16v7.Services;

namespace iw16v7;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Створюємо конкретні реалізації (заглушки)
        IImageLoader loader = new ImageLoader();
        IImageResizer resizer = new ImageResizer();
        IImageFilter filter = new ImageFilter();
        IImageSaver saver = new ImageSaver();

        // Впроваджуємо залежності в сервіс
        var service = new ImageProcessingService(loader, resizer, filter, saver);

        // Виконуємо роботу
        Console.WriteLine("=== Запуск обробки зображення ===");
        service.Process("photo.jpg");
        Console.WriteLine("=================================");
    }
}