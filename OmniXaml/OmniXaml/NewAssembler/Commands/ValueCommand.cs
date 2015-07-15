namespace OmniXaml.NewAssembler.Commands
{
    public class ValueCommand : Command
    {
        private readonly string value;

        public ValueCommand(SuperObjectAssembler superObjectAssembler, string value) : base(superObjectAssembler)
        {
            this.value = value;
        }

        public override void Execute()
        {
            if (!StateCommuter.IsProcessingValuesAsCtorArguments)
            {
                ProcessAsMemberValue();
            }
            else
            {
                ProcessAsCtorArgument();
            }
        }

        private void ProcessAsCtorArgument()
        {
            StateCommuter.AddCtorArgument(value);
        }

        private void ProcessAsMemberValue()
        {
            StateCommuter.RaiseLevel();
            StateCommuter.Instance = value;

            AssignValueDirectlyWhenMemberIsNotDirective();
        }

        private void AssignValueDirectlyWhenMemberIsNotDirective()
        {
            if (StateCommuter.PreviousMember.IsDirective)
            {
                return;
            }

            StateCommuter.AssignChildToParentProperty();
            StateCommuter.DecreaseLevel();
        }
    }
}