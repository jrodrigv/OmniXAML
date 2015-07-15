namespace OmniXaml.Parsers.ProtoParser.SuperProtoParser
{
    using System.Collections.ObjectModel;
    using System.Xml;
    using Glass;
    using Typing;

    internal class AttributeReader
    {
        private readonly IXmlReader reader;

        public AttributeReader(IXmlReader reader)
        {
            this.reader = reader;
        }

        public AttributeFeed Load()
        {
            var prefixDefinitions = new Collection<NsPrefix>();
            var attributes = new Collection<UnprocessedAttribute>();

            RawDirective classDirective = null;
            if (reader.MoveToFirstAttribute())
            {
                do
                {
                    var longDescriptor = reader.Name;

                    if (longDescriptor.Contains("xmlns"))
                    {
                        prefixDefinitions.Add(GetPrefixDefinition());
                    }
                    else if (reader.Name == "x:Class")
                    {
                        classDirective = GetClassDirective();
                    }
                    else
                    {
                        attributes.Add(GetAttribute());
                    }

                } while (reader.MoveToNextAttribute());

                reader.MoveToElement();
            }

            return new AttributeFeed(prefixDefinitions, attributes) { Class = classDirective };
        }

        private RawDirective GetClassDirective()
        {
            return new RawDirective("Class", reader.Value);
        }

        private UnprocessedAttribute GetAttribute()
        {
            return new UnprocessedAttribute(PropertyLocator.Parse(reader.Name), reader.Value);
        }

        private NsPrefix GetPrefixDefinition()
        {
            var dicotomize = reader.Name.Dicotomize(':');
            return new NsPrefix(dicotomize.Item2 ?? string.Empty, reader.Value);
        }
    }

    internal class RawDirective
    {
        public RawDirective(string identifier, string value)
        {
            Identifier = identifier;
            Value = value;
        }

        public string Identifier { get; set; }
        public string Value { get; set; }
    }
}