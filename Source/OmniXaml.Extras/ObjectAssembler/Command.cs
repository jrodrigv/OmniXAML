namespace OmniXaml.Extras.ObjectAssembler
{
    using OmniXaml.ObjectAssembler;

    public abstract class Command
    {
        protected Command(ObjectAssembler assembler)
        {
            Assembler = assembler;
        }

        protected ObjectAssembler Assembler { get; }

        public abstract void Execute();
        protected StateCommuter StateCommuter => Assembler.StateCommuter;   
    }
}