using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using UmlGen.Infrastructure;
using UmlGen.Model;

namespace UmlGen.Service
{
    public class ClassService
    {
        private Dictionary<string, ClassStructure> classes;
        
        public void ReadFiles(string path)
        {
            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                if (!File.Exists(file))
                {
                    continue;
                }

                var code = IoHelper.ReadFile(file);
                var tree = CSharpSyntaxTree.ParseText(code);
                var root = tree.GetRoot();

                var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

                foreach (var classDeclaration in classDeclarations)
                {
                    var classname = classDeclaration.Identifier.ToString();
                    
                    var props = classDeclaration.DescendantNodes().OfType<PropertyDeclarationSyntax>().ToList();

                    var methods = classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();

                    var classStructure = new ClassStructure();
                    classStructure.Name = classname;
                    classStructure.Properties = new List<string>();
                    classStructure.Methods = new List<string>();


                    foreach (var csProp in props)
                    {
                        var modifiers = GetModifiers(csProp.Modifiers);

                        var umlProperty = $"{modifiers.accessModifier}{csProp.Identifier.Text} {csProp.Type.ToString}{modifiers.additionalModifier}";

                        classStructure.Properties.Add(umlProperty);
                    }

                    foreach (var csMethod in methods)
                    {
                        var modifiers = GetModifiers(csMethod.Modifiers);

                        var parameters = string.Join(", ", csMethod.ParameterList.Parameters.Select(x => $"{x.Identifier.ValueText} {x.Type.ToString}"));

                        var umlMethod = $"{modifiers.accessModifier}{csMethod.Identifier.Text}({parameters}) {csMethod.ReturnType.ToString}{modifiers.additionalModifier}";

                        classStructure.Methods.Add(umlMethod);
                    }

                    
                }
            }
        }

        private (char accessModifier, string additionalModifier) GetModifiers(SyntaxTokenList tokens)
        {
            bool isPublic = tokens.Any(m => m.IsKind(SyntaxKind.PublicKeyword));
            bool isPrivate = tokens.Any(m => m.IsKind(SyntaxKind.PrivateKeyword));
            bool isProtected = tokens.Any(m => m.IsKind(SyntaxKind.ProtectedKeyword));
            bool isInternal = tokens.Any(m => m.IsKind(SyntaxKind.InternalKeyword));

            bool isStatic = tokens.Any(m => m.IsKind(SyntaxKind.StaticKeyword));

            bool isAbstract = tokens.Any(m => m.IsKind(SyntaxKind.AbstractKeyword));
            bool isVirtual = tokens.Any(m => m.IsKind(SyntaxKind.VirtualKeyword));

            var accessModifier = isPublic ? '+' : isPrivate ? '-' : isProtected ? '#' : '~';

            var additionalModifierBuilder = new StringBuilder();
            if (isStatic)
            {
                additionalModifierBuilder.Append("$");
            }
            if (isAbstract)
            {
                additionalModifierBuilder.Append("*");
            }

            return (accessModifier, additionalModifierBuilder.ToString());
        }
    }
}
