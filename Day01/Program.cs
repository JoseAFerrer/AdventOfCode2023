// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var path = "input.txt";

var readText = File.ReadAllLines(path);
foreach (var s in readText)
{
    Console.WriteLine(s);
}

Console.WriteLine("Hello, World!");