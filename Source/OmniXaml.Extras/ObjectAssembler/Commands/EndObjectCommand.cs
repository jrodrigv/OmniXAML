namespace OmniXaml.Extras.ObjectAssembler.Commands
{
    using System.Collections;

    public class EndObjectCommand : Command
    {
        public EndObjectCommand(ObjectAssembler assembler) : base(assembler)
        {
        }

        public override void Execute()
        {
            if (!StateCommuter.Current.IsGetObject)
            {
                StateCommuter.CreateInstanceOfCurrentXamlTypeIfNotCreatedBefore();
                StateCommuter.PutNameToCurrentInstanceIfAny();

                if (StateCommuter.Current.Instance is IMarkupExtension)
                {
                    ProcessCurrentIntanceValueWithMarkupExtension();               
                }

                StateCommuter.AssociateCurrentInstanceToParent();

                StateCommuter.RegisterInstanceNameToNamescope();
                StateCommuter.NotifyEnd();
            }

            var suffix = StateCommuter.Level == 1 ? ";" : ",";
            var trailing = StateCommuter.Level == 1 ? "" : " ";
            StateCommuter.Builder.Append($"}}{suffix}{trailing}");
            StateCommuter.DecreaseLevel();
        }

        private void ProcessCurrentIntanceValueWithMarkupExtension()
        {
            var processedValue = StateCommuter.GetValueProvidedByMarkupExtension((IMarkupExtension) StateCommuter.Current.Instance);
            StateCommuter.Current.Instance = processedValue;

            var collection = processedValue as ICollection;
            if (collection != null)
            {
                StateCommuter.Current.Collection = collection;
            }
        }

        public override string ToString()
        {
            return "End of Object";
        }
    }
}