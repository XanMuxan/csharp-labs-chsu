using System;
using Lab2.Interfaces;
using Lab2.Models;
using QrLib.Enums;
using QrLib.Interfaces;
using StorageLibrary.Models;
using Terminal.LabsTests.Interfaces;

namespace Terminal.LabsTests.Tests.TestLab3;

/// <summary>
/// Тестирование лабораторной работы №3.
/// </summary>
internal class TestLab3 : ITest
{
    /// <summary>
    /// Автоматический тест
    /// </summary>
    public int Test()
    {
        Console.Clear();
        Console.WriteLine("=== АВТОМАТИЧЕСКИЙ ТЕСТ ===");

        IQRCode.OutputType = QrCodeType.Full;

        IProduct device1 = new MemoryDevice(100, 100m, "Флешка A", "USB", 64, "exFAT");
        var device2 = new Package(new MemoryDevice(200, 200m, "Флешка B", "USB", 128, "NTFS"));

        Storage<IProduct> assortment1 = 10;
        Storage<Package> assortment2 = 10;

        assortment1[0] = device1;
        assortment1.Push(device2);
        assortment1.SortById();
        ShowAndWait(assortment1, "Этап 1");

        assortment1.Id = 100;
        ShowAndWait(assortment1, "Этап 2");

        var package = assortment1[assortment1.Find(device2.Id)];
        assortment2[0] = (Package)package;

        ShowAndWait(assortment1, "Этап 3");
        ShowAndWait(assortment2, "Этап 4");


        return 0;
    }

    private void ShowAndWait<T>(Storage<T> a, string title) where T : IProduct
    {
        Console.WriteLine($"--- {title} ---");
        Console.WriteLine(a.ToString());
        Console.WriteLine("Нажмите Enter...");
        Console.ReadLine();
    }
}
