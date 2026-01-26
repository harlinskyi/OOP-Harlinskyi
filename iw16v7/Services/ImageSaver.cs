using System;
using iw16v7.Interfaces;

namespace iw16v7.Services;

public class ImageSaver : IImageSaver
{
    public void Save(string path) => Console.WriteLine($"[Saver] Фільтр збережено у {path}.");
}