using System;
using iw16v7.Interfaces;

namespace iw16v7.Services;


public class ImageLoader : IImageLoader
{
    public void Load(string path) => Console.WriteLine($"[Loader] Зображення {path} завантажено.");
}