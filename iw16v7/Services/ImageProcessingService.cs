using System;
using iw16v7.Interfaces;

namespace iw16v7.Services;

public class ImageProcessingService
{
    private readonly IImageLoader _loader;
    private readonly IImageResizer _resizer;
    private readonly IImageFilter _filter;
    private readonly IImageSaver _saver;

    public ImageProcessingService(IImageLoader loader, IImageResizer resizer, IImageFilter filter, IImageSaver saver)
    {
        _loader = loader;
        _resizer = resizer;
        _filter = filter;
        _saver = saver;
    }

    public void Process(string fileName)
    {
        _loader.Load(fileName);
        _resizer.Resize(800, 600);
        _filter.ApplyFilter("B&W");
        _saver.Save($"{fileName}_edited.png");
        Console.WriteLine("Обробка успішно завершена!");
    }
}