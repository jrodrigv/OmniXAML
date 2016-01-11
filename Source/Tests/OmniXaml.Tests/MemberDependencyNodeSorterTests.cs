﻿namespace OmniXaml.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Resources;

    [TestClass]
    public class MemberDependencyNodeSorterTests : GivenARuntimeTypeSourceWithNodeBuilders
    {
        private readonly MemberDependencyNodeSorter memberDependencyNodeSorter = new MemberDependencyNodeSorter();
        private readonly InstructionResources resources;

        public MemberDependencyNodeSorterTests()
        {
            resources = new InstructionResources(this);
        }

        [TestMethod]
        public void SortWholeStyle()
        {
            var input = resources.StyleUnsorted;

            var enumerator = new EnumeratorDebugWrapper<Instruction>(input.GetEnumerator());
            var actualNodes = memberDependencyNodeSorter.Sort(enumerator).ToList();
            var expectedInstructions = resources.StyleSorted;

            CollectionAssert.AreEqual(expectedInstructions, actualNodes);
        }


        [TestMethod]
        public void SortSetter()
        {
            var input = resources.SetterUnsorted;

            var enumerator = new EnumeratorDebugWrapper<Instruction>(input.GetEnumerator());
            var actualNodes = memberDependencyNodeSorter.Sort(enumerator).ToList();
            var expectedInstructions = resources.SetterSorted;

            CollectionAssert.AreEqual(expectedInstructions, actualNodes);
        }

        [TestMethod]
        public void SortComboBox()
        {
            var input = resources.ComboBoxUnsorted;

            var enumerator = new EnumeratorDebugWrapper<Instruction>(input.GetEnumerator());
            var actualNodes = memberDependencyNodeSorter.Sort(enumerator).ToList();
            var expectedInstructions = resources.ComboBoxSorted;

            CollectionAssert.AreEqual(expectedInstructions, actualNodes);
        }

        [TestMethod]
        public void SortMarkupExtension()
        {
            var input = resources.ListBoxSortedWithExtension;

            var enumerator = new EnumeratorDebugWrapper<Instruction>(input.GetEnumerator());
            var actualNodes = memberDependencyNodeSorter.Sort(enumerator).ToList();
            var expectedInstructions = resources.ListBoxSortedWithExtension;

            CollectionAssert.AreEqual(expectedInstructions, actualNodes);
        }
    }

    public class EnumeratorDebugWrapper<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> getEnumerator;

        public EnumeratorDebugWrapper(IEnumerator<T> getEnumerator)
        {
            this.getEnumerator = getEnumerator;
        }

        public void Dispose()
        {
            getEnumerator.Dispose();
        }

        public bool MoveNext()
        {
            return getEnumerator.MoveNext();
        }

        public void Reset()
        {
            getEnumerator.Reset();
        }

        public T Current => getEnumerator.Current;

        object IEnumerator.Current => Current;
    }
}
