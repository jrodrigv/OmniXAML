namespace OmniXaml.Tests
{
    using System;
    using System.Text;
    using Common;
    using ObjectAssembler;
    using ObjectAssembler.Commands;

    internal class CSharpObjectAssembler : IObjectAssembler
    {
        private readonly IRuntimeTypeSource typeRuntimeTypeSource;
        private readonly ITopDownValueContext topDownValueContext;
        private readonly Settings settings;
        private readonly StringBuilder builder;

        public CSharpObjectAssembler(IRuntimeTypeSource typeRuntimeTypeSource, ITopDownValueContext topDownValueContext, Settings settings)
        {
            this.typeRuntimeTypeSource = typeRuntimeTypeSource;
            this.topDownValueContext = topDownValueContext;
            this.settings = settings;
            builder = new StringBuilder();
        }

        public object Result => builder.ToString();

        public EventHandler<XamlSetValueEventArgs> XamlSetValueHandler { get; set; }

        public IRuntimeTypeSource TypeSource => typeRuntimeTypeSource;

        public ITopDownValueContext TopDownValueContext => topDownValueContext;

        public IInstanceLifeCycleListener LifecycleListener => settings?.InstanceLifeCycleListener;

        public void Process(Instruction instruction)
        {
            switch (instruction.InstructionType)
            {
                case InstructionType.None:
                    break;
                case InstructionType.StartObject:
                    builder.Append($"var root = new {instruction.XamlType.Name} {{ ");
                    break;
                case InstructionType.EndObject:
                    builder.Append($"}};");
                    break;
                case InstructionType.StartMember:
                    builder.Append($"{instruction.Member.Name} = ");
                    break;
                case InstructionType.EndMember:
                    builder.Append($", ");
                    break;
                case InstructionType.Value:
                    builder.Append($"\"{instruction.Value}\"");
                    break;
                case InstructionType.NamespaceDeclaration:
                    break;
                case InstructionType.GetObject:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OverrideInstance(object instance)
        {            
        }
    }
}