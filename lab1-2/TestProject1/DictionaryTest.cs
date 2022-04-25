using Xunit;
using System;
using DictionaryLib;

namespace TestProject1
{
    public class DictionaryTest
    {
        OwnDictionary<int?, string> MyDictionary = new OwnDictionary<int?, string>(5);
        [Fact]
        public void InitializeDictionaryWithIncorrectCapacity()
        {
            Assert.Throws<FormatException>(() => { OwnDictionary<double?, string> MyDictionary = new OwnDictionary<double?, string>(-5); });
        }
        [Theory]
        [InlineData(2, "הגא")]
        [InlineData(3,"ענט")]
        [InlineData(4,"קמעטנט")]
        [InlineData(5, "ן'ÿעü")]
        [InlineData(6, "ר³סעü")]
        [InlineData(7, "ס³ל")]
        [InlineData(8, "ג³ס³ל")]
        public void AddWithoutException(int key, string value)
        {
            MyDictionary.Add(key, value);
            Assert.True(MyDictionary.ContainsKey(key));
        }
        [Theory]
        [InlineData(null, null)]
        [InlineData(15, null)]
        [InlineData(null, "קמעטנט")]
        [InlineData(2, "הגא")]
        public void AddWithException(int? key, string value)
        {
            FillDictionary();
            Assert.Throws<ArgumentNullException>(() => MyDictionary.Add(key, value));
        }
        [Theory]
        [InlineData(2)]
        [InlineData(100)]
        [InlineData(169)]
        public void RemoveExistingItems(int key)
        {
            FillDictionary();
            Assert.True(MyDictionary.Remove(key));
        }
        [Fact]
        public void RemoveNotExistingItems()
        {
            FillDictionary();
            Assert.True(!MyDictionary.Remove(15));
        }
        [Fact]
        public void RemoveWithException()
        {
            Assert.Throws<ArgumentNullException>(() => MyDictionary.Remove(null));
        }
        [Fact]
        public void ContainsValue_True()
        {
            FillDictionary();
            Assert.True(MyDictionary.ContainsValue("169"));
        }
        [Fact]
        public void ContainsValue_False()
        {
            FillDictionary();
            Assert.True(!MyDictionary.ContainsValue("168"));
        }
        [Fact]
        public void ContainsValue_Exception()
        {
            FillDictionary();
            Assert.Throws<ArgumentNullException>(() => MyDictionary.ContainsValue(null));
        }
        [Fact]
        public void Indexer_SetCorrect()
        {
            FillDictionary();
            MyDictionary[27] = "הנ";
            Assert.True(MyDictionary.ContainsValue("הנ"));
        }
        [Theory]
        [InlineData(null, "dr")]
        [InlineData(20, "dr")]
        [InlineData(27, null)]
        public void Indexer_SetInCorrect(int? key, string newValue)
        {
            FillDictionary();
            Assert.Throws<ArgumentNullException>(() => MyDictionary[key] = newValue);
        }
        [Fact]
        public void Enumerator()
        {
            FillDictionary();
            foreach (var item in MyDictionary)
            {
                Assert.True(MyDictionary.ContainsValue(item.Value));
            }
        }
        [Fact]
        public void Indexer_GetCorrect()
        {
            FillDictionary();
            var value = MyDictionary[2];
            Assert.True(MyDictionary.ContainsValue(value));
        }
        [Theory]
        [InlineData(null)]
        [InlineData(8)]
        public void Indexer_GetInCorrect(int? key)
        {
            FillDictionary();
            Assert.Throws<ArgumentNullException>(() => MyDictionary[key]);
        }
        public void FillDictionary()
        {
            MyDictionary.Add(222, "222");
            MyDictionary.Add(100, "100");
            MyDictionary.Add(154, "154");
            MyDictionary.Add(169, "169");
            MyDictionary.Add(27, "27");
            MyDictionary.Add(2, "2");
            MyDictionary.Add(13, "13");
            MyDictionary.Add(96, "96");
            MyDictionary.Add(999, "999");
        }
    }
}