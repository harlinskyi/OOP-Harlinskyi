using System;
using iw16v7.Interfaces;

namespace iw16v7.Services;


public class ImageFilter : IImageFilter
{
    public void ApplyFilter(string filterName) => Console.WriteLine($"[Filter] Фільтр '{filterName}' застосовано.");
}