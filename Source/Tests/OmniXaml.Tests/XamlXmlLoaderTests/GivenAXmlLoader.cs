namespace OmniXaml.Tests.XamlXmlLoaderTests
{
    using Common;
    using Common.NetCore;

    public class GivenAXmlLoader : GivenARuntimeTypeSource
    {
        protected GivenAXmlLoader()
        {
            Loader = new XmlLoader(new DummyParserFactory(TypeRuntimeTypeSource));
        }

        protected XmlLoader Loader { get; }
    }
}
