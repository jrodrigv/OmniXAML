﻿namespace OmniXaml.Tests.Parsers.InstructionParserTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Classes;
    using Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OmniXaml.Parsers.Parser;

    [TestClass]
    public class MarkupExtensionsParsingFromProtoToXaml : GivenARuntimeTypeSourceWithNodeBuilders
    {
        private readonly IInstructionParser sut;
        
        public MarkupExtensionsParsingFromProtoToXaml()
        {            
            sut = new InstructionParser(TypeRuntimeTypeSource);
        }

        [TestMethod]
        public void SimpleExtension()
        {            
            var input = new List<ProtoInstruction>
            {
                P.NamespacePrefixDeclaration(RootNs),
                P.EmptyElement(typeof (DummyClass), RootNs),
                P.Attribute<DummyClass>(d => d.SampleProperty, "{Dummy}", RootNs.Prefix),
            };

            var expectedInstructions = new List<Instruction>
            {
                X.NamespacePrefixDeclaration(RootNs),
                X.StartObject(typeof(DummyClass)),
                X.StartMember<DummyClass>(d => d.SampleProperty),
                X.StartObject(typeof(DummyExtension)),
                X.EndObject(),
                X.EndMember(),                
                X.EndObject(),
            };

            var actualNodes = sut.Parse(input).ToList();

            CollectionAssert.AreEqual(expectedInstructions, actualNodes);
        }

        [TestMethod]
        public void ExtensionWithOption()
        {
            var input = new List<ProtoInstruction>
            {
                P.NamespacePrefixDeclaration(RootNs),
                P.EmptyElement(typeof (DummyClass), RootNs),
                P.Attribute<DummyClass>(d => d.SampleProperty, "{Dummy Option}", RootNs.Prefix),
            };

            var expectedInstructions = new List<Instruction>
            {
                X.NamespacePrefixDeclaration(RootNs),
                X.StartObject(typeof (DummyClass)),
                X.StartMember<DummyClass>(d => d.SampleProperty),
                X.StartObject(typeof (DummyExtension)),
                X.MarkupExtensionArguments(),
                X.Value("Option"),
                X.EndMember(),
                X.EndObject(),
                X.EndMember(),
                X.EndObject(),
            };

            var actualNodes = sut.Parse(input).ToList();

            CollectionAssert.AreEqual(expectedInstructions, actualNodes);
        }
    }
}
