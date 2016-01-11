namespace OmniXaml.ObjectAssembler
{
    public class NullLevel : Level
    {
        public NullLevel()
        {
            IsEmpty = true;
        }

        public override string ToString()
        {
            return "{Null Level}";
        }
    }
}