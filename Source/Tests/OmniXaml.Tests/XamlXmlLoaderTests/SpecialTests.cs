namespace OmniXaml.Tests.XamlXmlLoaderTests
{
    using Classes;
    using Common;
    using Common.NetCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public abstract class SpecialTests : GivenARuntimeTypeSource
    {
        [TestMethod]
        public void LoadWithRootInstance()
        {
            var dummy = new DummyClass
            {
                AnotherProperty = "Other value",
                SampleProperty = "Will be overwritten"
            };

            var loader = new XmlLoader(new DummyParserFactory(TypeRuntimeTypeSource));

            var actual = loader.FromString("<DummyClass xmlns=\"root\" SampleProperty=\"Value\" />", dummy);

            Assert.IsInstanceOfType(actual, dummy.GetType());
            Assert.AreEqual("Value", ((DummyClass)actual).SampleProperty);
            Assert.AreEqual("Other value", ((DummyClass)actual).AnotherProperty);
        }
    }
}