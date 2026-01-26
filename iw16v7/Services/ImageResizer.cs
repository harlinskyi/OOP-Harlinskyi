using System;
using iw16v7.Interfaces;

namespace iw16v7.Services;


public class ImageResizer : IImageResizer
{
    public void Resize(int width, int height) => Console.WriteLine($"[Resizer] Розмір змінено на {width}x{height}.");
}