namespace UmlGen.Samples
{
    public class Animal
    {
        public string Name { get; set; }

        public void Move()
        {

        }

        public virtual string Speak()
        {
            return string.Empty;
        }
    }
}
