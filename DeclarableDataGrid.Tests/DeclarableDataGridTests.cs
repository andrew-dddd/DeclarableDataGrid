using DeclarableDataGrid.PropertyDescriptors;
using FluentAssertions;
using System;
using Xunit;

namespace DeclarableDataGrid.Tests
{
    public class DeclarableDataGridTests
    {
        private readonly DeclarableDataGridObservableCollection<TestClass> TestCollection = new DeclarableDataGridObservableCollection<TestClass>();

        [Fact]
        public void DynamicColumnBuilder_ShouldBuildDynamicColumnDescriptor()
        {
            // Arrange
            TestCollection.UseDynamicPropertyAsColumn<string>("TestDynamicColumn", x => x.WithDisplayIndex(10).WithDisplayName("Test dynamic column"));

            // Assert
            var itemProperties = TestCollection.GetItemProperties(null);

            var dynamicPropertyDescriptor = itemProperties.Find("TestDynamicColumn", true) as DynamicColumnPropertyDescriptor;
            dynamicPropertyDescriptor.Should().NotBeNull("Expected dynamic property descriptor to be of specific type");

            dynamicPropertyDescriptor.PropertyType.Should().Be(typeof(string));
            dynamicPropertyDescriptor.Name.Should().Be("TestDynamicColumn");
            dynamicPropertyDescriptor.DisplayName.Should().Be("Test dynamic column");
            dynamicPropertyDescriptor.DisplayIndex.Should().Be(10);
        }

        [Fact]
        public void DynamicColumnBuilder_ShouldBuildPropertyColumnDescriptor()
        {
            // Arrange
            TestCollection.UsePropertyAsColumn(x => x.TestProperty, x => x.WithDisplayIndex(12).WithDisplayName("Test property"));

            // Assert
            var itemProperties = TestCollection.GetItemProperties(null);

            var dynamicPropertyDescriptor = itemProperties.Find("TestProperty", true) as PropertyColumnDataDescriptor;
            dynamicPropertyDescriptor.Should().NotBeNull("Expected property column descriptor to be of specific type");

            dynamicPropertyDescriptor.PropertyType.Should().Be(typeof(int));
            dynamicPropertyDescriptor.Name.Should().Be("TestProperty");
            dynamicPropertyDescriptor.DisplayName.Should().Be("Test property");
            dynamicPropertyDescriptor.DisplayIndex.Should().Be(12);
        }

        [Fact]
        public void DynamicColumnBuilder_ShouldBuildPropertyColumnDescriptor_WithoutDisplayName()
        {
            // Arrange
            TestCollection.UsePropertyAsColumn(x => x.TestProperty, x => x.WithDisplayIndex(12));

            // Assert
            var itemProperties = TestCollection.GetItemProperties(null);

            var dynamicPropertyDescriptor = itemProperties.Find("TestProperty", true) as PropertyColumnDataDescriptor;
            dynamicPropertyDescriptor.Should().NotBeNull("Expected property column descriptor to be of specific type");

            dynamicPropertyDescriptor.PropertyType.Should().Be(typeof(int));
            dynamicPropertyDescriptor.Name.Should().Be("TestProperty");
            dynamicPropertyDescriptor.DisplayName.Should().Be("TestProperty");
            dynamicPropertyDescriptor.DisplayIndex.Should().Be(12);
        }

        [Fact]
        public void DynamicColumnBuilder_ShouldValidateAddedType()
        {
            // Arrange
            TestCollection.UsePropertyAsColumn(x => x.TestProperty, x => x.WithDisplayIndex(0));
            TestCollection.UseDynamicPropertyAsColumn<TestBaseClass>("TestComplexProperty", x => x.WithDisplayIndex(1));

            // Assert
            TestCollection.Add(new TestClass
            {
                ["TestComplexProperty"] = new TestDerivedClass()
            });
        }

        [Fact]
        public void DynamicColumnBuilder_ShouldValidateAddedType_AndThrowIfTypeDoesNotMatchTypeSpecifiedInBuild()
        {
            // Arrange
            TestCollection.UsePropertyAsColumn(x => x.TestProperty, x => x.WithDisplayIndex(0));
            TestCollection.UseDynamicPropertyAsColumn<TestBaseClass>("TestComplexProperty", x => x.WithDisplayIndex(1));

            // Assert
            var ex = Assert.Throws<InvalidOperationException>(() => TestCollection.Add(new TestClass
            {
                ["TestComplexProperty"] = new TestNonDerivedClass()
            }));
        }
    }
}
