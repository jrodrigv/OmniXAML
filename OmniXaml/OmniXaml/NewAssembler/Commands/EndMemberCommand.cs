namespace OmniXaml.NewAssembler.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Typing;

    public class EndMemberCommand : Command
    {
        private readonly ITypeContext typeContext;

        public EndMemberCommand(SuperObjectAssembler assembler) : base(assembler)
        {
            typeContext = Assembler.WiringContext.TypeContext;
        }

        public override void Execute()
        {
            if (PreviousMemberIsDirective)
            {
                ProcessDirective();
            }
            else if (StateCommuter.IsProcessingValuesAsCtorArguments)
            {
                AdaptCurrentCtorArgumentsToCurrentType();
            }
            else if (IsTherePendingInstanceWaitingToBeAssigned)
            {
                StateCommuter.AssociateCurrentInstanceToParent();
                StateCommuter.DecreaseLevel();
            }
        }

        private void ProcessDirective()
        {
            if (Equals(StateCommuter.PreviousMember, CoreTypes.Class))
            {
                if (StateCommuter.Level == 2)
                {
                    var associatedSubType = GetAssociatedSubType();
                    StateCommuter.PreviousXamlType = associatedSubType;
                    StateCommuter.CreateInstanceOfCurrentXamlTypeIfNotCreatedBefore();
                    StateCommuter.DecreaseLevel();
                }
            }
        }

        private XamlType GetAssociatedSubType()
        {
            var typeName = StateCommuter.Instance as string;

            if (typeName == null)
            {
                throw new InvalidCastException("Cannot get the associated type from the current instance");
            }

            var assemblyOfBase = StateCommuter.PreviousXamlType.UnderlyingType.GetTypeInfo().Assembly.GetName();
            var type = Type.GetType(typeName + ", " + assemblyOfBase, true);
        
            return typeContext.GetXamlType(type);
        }

        private bool PreviousMemberIsDirective => StateCommuter.PreviousMember?.IsDirective ?? false;

        private bool IsTherePendingInstanceWaitingToBeAssigned => StateCommuter.HasCurrentInstance && StateCommuter.Member == null;

        private void AdaptCurrentCtorArgumentsToCurrentType()
        {
            var arguments = StateCommuter.CtorArguments;
            var xamlTypes = GetTypesOfBestCtorMatch(StateCommuter.XamlType, arguments.Count);

            var i = 0;
            foreach (var ctorArg in arguments)
            {
                var targetType = xamlTypes[i];
                var compatibleValue = StateCommuter.ValuePipeline.ConvertValueIfNecessary(ctorArg.StringValue, targetType.UnderlyingType);
                ctorArg.Value = compatibleValue;
            }
        }

        private IList<XamlType> GetTypesOfBestCtorMatch(XamlType xamlType, int count)
        {
            var constructor = SelectConstructor(xamlType, count);
            return constructor.GetParameters().Select(p => typeContext.GetXamlType(p.ParameterType)).ToList();
        }

        private ConstructorInfo SelectConstructor(XamlType xamlType, int count)
        {
            return xamlType.UnderlyingType.GetTypeInfo().DeclaredConstructors.First(info => info.GetParameters().Count() == count);
        }
    }
}