namespace UmlGen.Model;

public class ClassStructure
{
    public string FullName { get; set; }

    public string Name { get; set; }
    public List<string> Methods { get; set; }
    public List<string> Properties { get; set; }
}


public abstract class ClassMember
{
    public string Name { get; set; }
    public string AccessModifier { get; set; }
    public string ReturnType { get; set; }
}

public class Property : ClassMember
{

}

public class Method : ClassMember
{
    public List<string> inputs { get; set; }
}