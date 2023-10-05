// See https://aka.ms/new-console-template for more information
using UmlGen.Service;

Console.WriteLine("Hello, World!");


var currentPath = $"C:/Users/nurzh/source/repos/UmlGen/UmlGen/Samples";

ClassService classService = new ClassService();

classService.ReadFiles(currentPath);
classService.WriteUmlText($"{currentPath}/uml.md");



Console.WriteLine("Bye");