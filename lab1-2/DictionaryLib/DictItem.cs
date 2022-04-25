using System;
using System.Collections.Generic;
namespace DictionaryLib
{
    public class DictItem<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public DictItem(TKey key, TValue value)
        {
            if (key != null && value != null)
            {
                Key = key;
                Value = value;
            }
            else
            {
                throw new ArgumentNullException("You passed in uninitialized variable");
            }

        }
        public override int GetHashCode()
        {
            return Key.GetHashCode() ^ Value.GetHashCode();
        }
        public override string ToString()
        {
            return Key + " - " + Value;
        }
        public override bool Equals(object obj)
        {
            return obj.ToString() == this.ToString();
        }
    }
}
