namespace OmniXaml.Tests
{
    using Common;
    using ObjectAssembler;
    using Resources;
    using Xunit;

    public class CSharpObjectAssemblerTests : GivenARuntimeTypeSourceWithNodeBuilders
    {
        private readonly InstructionResources source;

        public CSharpObjectAssemblerTests()
        {
            source = new InstructionResources(this);
        }

        private IObjectAssembler CreateSut()
        {
            return new Extras.ObjectAssembler.ObjectAssembler(TypeRuntimeTypeSource, new TopDownValueContext(), new Settings { InstanceLifeCycleListener = new TestListener() });
        }

        [Fact]
        public void OneObject()
        {
            var sut = CreateSut();
            sut.Process(source.OneObject);

            var result = sut.Result;

            Assert.Equal("var root = new DummyClass { };", result);
        }

        [Fact]
        public void ObjectWithMember()
        {
            var sut = CreateSut();
            sut.Process(source.ObjectWithMember);

            var result = sut.Result;
            Assert.Equal("var root = new DummyClass { SampleProperty = \"Property!\", };", result);
        }

        //    [Fact]
        //    public void ObjectWithEnumMember()
        //    {
        //        var sut = CreateSut();
        //        sut.Process(source.ObjectWithEnumMember);

        //        var result = sut.Result;
        //        var property = ((DummyClass)result).EnumProperty;

        //        Xunit.Assert.IsType<DummyClass>(result);
        //        Xunit.Assert.Equal(SomeEnum.One, property);
        //    }

        //    [Fact]
        //    public void ObjectWithNullableEnumProperty()
        //    {
        //        var sut = CreateSut();
        //        sut.Process(source.ObjectWithNullableEnumProperty);

        //        var result = sut.Result;
        //        var property = ((DummyClass)result).EnumProperty;

        //        Xunit.Assert.IsType<DummyClass>(result);
        //        Xunit.Assert.Equal(SomeEnum.One, property);
        //    }

        [Fact]
        public void ObjectWithTwoMembers()
        {
            var sut = CreateSut();
            sut.Process(source.ObjectWithTwoMembers);

            var result = sut.Result;
            Assert.Equal(result, "var root = new DummyClass { SampleProperty = \"Property!\", AnotherProperty = \"Another!\", };");
        }

        //    [TestMethod]
        //    public void ObjectWithChild()
        //    {
        //        sut.Process(source.ObjectWithChild);

        //        var result = sut.Result;
        //        var property = ((DummyClass)result).Child;

        //        Assert.IsInstanceOfType(result, typeof(DummyClass));
        //        Assert.IsInstanceOfType(property, typeof(ChildClass));
        //    }

        //    [TestMethod]
        //    public void WithCollection()
        //    {
        //        sut.Process(source.CollectionWithMoreThanOneItem);

        //        var result = sut.Result;
        //        var children = ((DummyClass)result).Items;

        //        Assert.IsInstanceOfType(result, typeof(DummyClass));
        //        Assert.AreEqual(3, children.Count);
        //        CollectionAssert.AllItemsAreInstancesOfType(children, typeof(Item));
        //    }

        //    [TestMethod]
        //    public void CollectionWithInnerCollection()
        //    {
        //        sut.Process(source.CollectionWithInnerCollection);

        //        var result = sut.Result;
        //        var children = ((DummyClass)result).Items;

        //        Assert.IsInstanceOfType(result, typeof(DummyClass));
        //        Assert.AreEqual(3, children.Count);
        //        CollectionAssert.AllItemsAreInstancesOfType(children, typeof(Item));
        //        var innerCollection = children[0].Children;
        //        Assert.AreEqual(2, innerCollection.Count);
        //        CollectionAssert.AllItemsAreInstancesOfType(innerCollection, typeof(Item));
        //    }

        //    [TestMethod]
        //    public void WithCollectionAndInnerAttribute()
        //    {
        //        sut.Process(source.WithCollectionAndInnerAttribute);

        //        var result = sut.Result;
        //        var children = ((DummyClass)result).Items;
        //        var firstChild = children.First();
        //        var property = firstChild.Title;

        //        Assert.IsInstanceOfType(result, typeof(DummyClass));
        //        CollectionAssert.AllItemsAreInstancesOfType(children, typeof(Item));
        //        Assert.AreEqual("SomeText", property);
        //    }

        //    [TestMethod]
        //    public void MemberWithIncompatibleTypes()
        //    {
        //        sut.Process(source.MemberWithIncompatibleTypes);

        //        var result = sut.Result;
        //        var property = ((DummyClass)result).Number;

        //        Assert.IsInstanceOfType(result, typeof(DummyClass));
        //        Assert.AreEqual(12, property);
        //    }

        //    [TestMethod]
        //    public void ExtensionWithArgument()
        //    {
        //        sut.Process(source.ExtensionWithArgument);

        //        var result = sut.Result;
        //        var property = ((DummyClass)result).SampleProperty;

        //        Assert.IsInstanceOfType(result, typeof(DummyClass));
        //        Assert.AreEqual("Option", property);
        //    }

        //    [TestMethod]
        //    public void ExtensionWithTwoArguments()
        //    {
        //        sut.Process(source.ExtensionWithTwoArguments);

        //        var result = sut.Result;
        //        var property = ((DummyClass)result).SampleProperty;

        //        Assert.IsInstanceOfType(result, typeof(DummyClass));
        //        Assert.AreEqual("OneSecond", property);
        //    }

        //    [TestMethod]
        //    public void ExtensionThatReturnsNull()
        //    {
        //        sut.Process(source.ExtensionThatReturnsNull);

        //        var result = sut.Result;
        //        var property = ((DummyClass)result).SampleProperty;

        //        Assert.IsInstanceOfType(result, typeof(DummyClass));
        //        Assert.IsNull(property);
        //    }

        //    [TestMethod]
        //    public void ExtensionWithNonStringArgument()
        //    {
        //        sut.Process(source.ExtensionWithNonStringArgument);

        //        var result = sut.Result;
        //        var property = ((DummyClass)result).Number;

        //        Assert.IsInstanceOfType(result, typeof(DummyClass));
        //        Assert.AreEqual(123, property);
        //    }

        //    [TestMethod]
        //    public void KeyDirective()
        //    {
        //        sut.Process(source.KeyDirective);

        //        var actual = sut.Result;
        //        Assert.IsInstanceOfType(actual, typeof(DummyClass));
        //        var dictionary = (IDictionary)((DummyClass)actual).Resources;
        //        Assert.IsTrue(dictionary.Count > 0);
        //    }

        //    [TestMethod]
        //    public void String()
        //    {
        //        var sysNs = new NamespaceDeclaration("clr-namespace:System;assembly=mscorlib", "sys");

        //        sut.Process(source.GetString(sysNs));

        //        var actual = sut.Result;
        //        Assert.IsInstanceOfType(actual, typeof(string));
        //        Assert.AreEqual("Text", actual);
        //    }


        //    [TestMethod]
        //    public void TopDownContainsOuterObject()
        //    {
        //        sut.Process(source.InstanceWithChild);

        //        var dummyClassXamlType = TypeRuntimeTypeSource.GetByType(typeof(DummyClass));
        //        var lastInstance = sut.TopDownValueContext.GetLastInstance(dummyClassXamlType);

        //        Assert.IsInstanceOfType(lastInstance, typeof(DummyClass));
        //    }

        //    [TestMethod]
        //    [ExpectedException(typeof(ParseException))]
        //    public void AttemptToAssignItemsToNonCollectionMember()
        //    {
        //        sut.Process(source.AttemptToAssignItemsToNonCollectionMember);
        //    }

        //    [TestMethod]
        //    [ExpectedException(typeof(ParseException))]
        //    public void TwoChildrenWithNoRoot_ShouldThrow()
        //    {
        //        sut.Process(source.TwoRoots);
        //    }

        //    [TestMethod]
        //    public void PropertyShouldBeAssignedBeforeChildIsAssociatedToItsParent()
        //    {
        //        sut.Process(source.ParentShouldReceiveInitializedChild);
        //        var parent = (SpyingParent)sut.Result;
        //        Assert.IsTrue(parent.ChildHadNamePriorToBeingAssigned);
        //    }

        //    [TestMethod]
        //    public void MixedCollection()
        //    {
        //        sut.Process(source.MixedCollection);
        //        var result = sut.Result;
        //        Assert.IsInstanceOfType(result, typeof(ArrayList));
        //        var arrayList = (ArrayList)result;
        //        Assert.IsTrue(arrayList.Count > 0);
        //    }

        //    [TestMethod]
        //    public void MixedCollectionWithRootInstance()
        //    {
        //        var root = new ArrayList();
        //        var assembler = CreateSutForLoadingSpecificInstance(root);
        //        assembler.Process(source.MixedCollection);
        //        var result = assembler.Result;
        //        Assert.IsInstanceOfType(result, typeof(ArrayList));
        //        var arrayList = (ArrayList)result;
        //        Assert.IsTrue(arrayList.Count > 0);
        //    }

        //    [Fact]
        //    public void RootInstanceWithAttachableMember()
        //    {
        //        var root = new DummyClass();
        //        var sut = CreateSutForLoadingSpecificInstance(root);
        //        sut.Process(source.RootInstanceWithAttachableMember);
        //        var result = sut.Result;
        //        var attachedProperty = Container.GetProperty(result);
        //        Xunit.Assert.Equal("Value", attachedProperty);
        //    }

        //    [Fact]
        //    public void ExpandedAttachablePropertyAndItemBelow()
        //    {
        //        var sut = CreateSut();
        //        sut.Process(source.ExpandedAttachablePropertyAndItemBelow);

        //        var items = ((DummyClass)sut.Result).Items;

        //        var firstChild = items.First();
        //        var attachedProperty = Container.GetProperty(firstChild);
        //        Xunit.Assert.Equal(2, items.Count);
        //        Xunit.Assert.Equal("Value", attachedProperty);
        //    }

        //    [Fact]
        //    public void AttachableMemberThatIsCollection()
        //    {
        //        var sut = CreateSut();
        //        sut.Process(source.AttachableMemberThatIsCollection);
        //        var instance = sut.Result;
        //        var col = Container.GetCollection(instance);

        //        Xunit.Assert.NotEmpty(col);
        //    }

        //    [Fact]
        //    public void CustomCollection()
        //    {
        //        var sut = CreateSut();
        //        sut.Process(source.CustomCollection);
        //        Xunit.Assert.NotEmpty((IEnumerable)sut.Result);
        //    }

        //    [Fact]
        //    public void PureCollection()
        //    {
        //        var sut = CreateSut();
        //        sut.Process(source.PureCollection);
        //        var actual = (ArrayList)sut.Result;
        //        Xunit.Assert.NotEmpty(actual);
        //    }

        //    [Fact]
        //    public void ImplicitCollection_ShouldHaveItems()
        //    {
        //        var sut = CreateSut();
        //        sut.Process(source.ImplicitCollection);
        //        var actual = (RootObject)sut.Result;

        //        var customCollection = actual.Collection;

        //        Xunit.Assert.NotEmpty(customCollection);
        //    }

        //    [Fact]
        //    public void ExplicitCollection_ShouldHaveItems()
        //    {
        //        var sut = CreateSut();
        //        sut.Process(source.ExplicitCollection);
        //        var actual = (RootObject)sut.Result;

        //        var customCollection = actual.Collection;

        //        Xunit.Assert.NotEmpty(customCollection);
        //    }

        //    [Fact]
        //    public void ImplicitCollection_ShouldKeepSameCollectionInstance()
        //    {
        //        var sut = CreateSut();
        //        sut.Process(source.ImplicitCollection);
        //        var actual = (RootObject)sut.Result;

        //        Xunit.Assert.False(actual.CollectionWasReplaced);
        //    }

        //    [Fact]
        //    public void ExplicitCollection_ShouldReplaceCollectionInstance()
        //    {
        //        var sut = CreateSut();
        //        sut.Process(source.ExplicitCollection);
        //        var actual = (RootObject)sut.Result;

        //        Xunit.Assert.True(actual.CollectionWasReplaced);
        //    }

        //    [TestMethod]
        //    public void NamedObject_HasCorrectName()
        //    {
        //        sut.Process(source.NamedObject);
        //        var result = sut.Result;
        //        var tb = (TextBlock)result;

        //        Assert.AreEqual("MyTextBlock", tb.Name);
        //    }

        //    [TestMethod]
        //    public void TwoNestedNamedObjects_HaveCorrectNames()
        //    {
        //        sut.Process(source.TwoNestedNamedObjects);
        //        var result = sut.Result;
        //        var lbi = (ListBoxItem)result;
        //        var textBlock = (TextBlock)lbi.Content;

        //        Assert.AreEqual("MyListBoxItem", lbi.Name);
        //        Assert.AreEqual("MyTextBlock", textBlock.Name);
        //    }

        //    [TestMethod]
        //    public void ListBoxWithItemAndTextBlockNoNames()
        //    {
        //        sut.Process(source.ListBoxWithItemAndTextBlockNoNames);

        //        var w = (Window)sut.Result;
        //        var lb = (ListBox)w.Content;
        //        var lvi = (ListBoxItem)lb.Items.First();
        //        var tb = lvi.Content;

        //        Assert.IsInstanceOfType(tb, typeof(TextBlock));
        //    }

        //    [TestMethod]
        //    public void ListBoxWithItemAndTextBlockWithNames_HaveCorrectNames()
        //    {
        //        sut.Process(source.ListBoxWithItemAndTextBlockWithNames);

        //        var w = (Window)sut.Result;
        //        var lb = (ListBox)w.Content;
        //        var lvi = (ListBoxItem)lb.Items.First();
        //        var tb = (TextBlock)lvi.Content;

        //        Assert.AreEqual("MyListBox", lb.Name);
        //        Assert.AreEqual("MyListBoxItem", lvi.Name);
        //        Assert.AreEqual("MyTextBlock", tb.Name);
        //    }

        //    [TestMethod]
        //    public void DirectContentForOneToMany()
        //    {
        //        sut.Process(source.DirectContentForOneToMany);
        //    }

        //    [TestMethod]
        //    public void CorrectInstanceSetupSequence()
        //    {
        //        var expectedSequence = new[] { SetupSequence.Begin, SetupSequence.AfterSetProperties, SetupSequence.AfterAssociatedToParent, SetupSequence.End };
        //        sut.Process(source.InstanceWithChild);

        //        var listener = (TestListener)sut.LifecycleListener;
        //        CollectionAssert.AreEqual(expectedSequence, listener.InvocationOrder);
        //    }        
    }
}