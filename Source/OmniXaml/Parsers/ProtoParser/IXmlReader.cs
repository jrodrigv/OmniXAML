using System.Xml;

namespace OmniXaml.Parsers.ProtoParser
{
    public interface IXmlReader : IXmlLineInfo
    {
        void Read();
        XmlNodeType NodeType { get; }
        bool IsEmptyElement { get; }
        string Prefix { get; }
        string LocalName { get; }
        string Name { get; }
        string Value { get; }
        string Namespace { get; }
        bool MoveToFirstAttribute();
        bool MoveToNextAttribute();
        void MoveToElement();
    }
}