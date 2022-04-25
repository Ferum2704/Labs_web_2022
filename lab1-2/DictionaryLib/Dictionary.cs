using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using LinkedListLibrary;

namespace DictionaryLib
{
    public class OwnDictionary<TKey, TValue>:IEnumerable<DictItem<TKey, TValue>>
    {
        public readonly int size = 100;
        OwnLinkedList<DictItem<TKey, TValue>>[] items;
        public event DictionaryHandler Added;
        public event DictionaryHandler Removed;
        public event DictionaryHandler Cleared;
        public OwnDictionary() { items = new OwnLinkedList<DictItem<TKey, TValue>>[size]; }
        public OwnDictionary(int size)
        {
            if (size>0)
            {
                this.size = size;
                items = new OwnLinkedList<DictItem<TKey, TValue>>[size];
            }
            else
            {
                throw new FormatException("You can`t initialized dictionary with such capacity");
            }
        }
        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                throw new ArgumentNullException("Item with key " + key + " is already exists!");
            }
            DictItem<TKey, TValue> dictItem = new DictItem<TKey, TValue>(key, value);
            int hashCode = dictItem.GetHashCode() & 0x7FFFFFFF;
            int place = hashCode % size;
            if (items[place] == null)
            {
                items[place] = new OwnLinkedList<DictItem<TKey, TValue>>();
                items[place].Add(dictItem);
            }
            else
            {
                items[place].Add(dictItem);
            }
            Added?.Invoke(this, new DictEventArgs("Item with " + key + " key was successfully added!"));
        }
        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("You passed in uninitialized variable");
            }
            if (ContainsKey(key, true))
            {
                Removed?.Invoke(this, new DictEventArgs("Item with " + key + " key was successfully removed!"));
                return true;
            }
            return false;
        }
        public bool ContainsKey(TKey key, bool IsForRemoving = false)
        {
            TValue defaultValue = default(TValue);
            return FindItemByKey(key, false, ref defaultValue, IsForRemoving);
        }
        private bool FindItemByKey(TKey key, bool isForSearching, ref TValue valueFromOrForIndexer, bool IsForRemoving = false)
        {
            foreach (var itemList in items)
            {
                if (itemList != null)
                {
                    foreach (var currentItem in itemList)
                    {
                        if (currentItem.Key.Equals(key))
                        {
                            if (IsForRemoving)
                            {
                                itemList.Remove(currentItem);
                            }
                            if (isForSearching)
                            {
                                valueFromOrForIndexer = currentItem.Value;
                            }
                            else
                            {
                                currentItem.Value = valueFromOrForIndexer;
                            }
                            return true;
                        }
                    }
                }
            }
            return false ;
        }
        public bool ContainsValue(TValue value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("You passed in uninitialized variable");
            }
            foreach (var itemList in items)
            {
                if (itemList != null)
                {
                    foreach (var currentItem in itemList)
                    {
                        if (currentItem.Value.Equals(value))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public TValue this[TKey key]
        {
            get {
                if (key == null)
                {
                    throw new ArgumentNullException("You passed in uninitialized variable");
                }
                TValue searchedValue = default(TValue);
                if (FindItemByKey(key, true, ref searchedValue))
                {
                    return searchedValue;
                }
                else
                {
                    throw new ArgumentNullException("There is no item with such key in dictionary");
                }
            }
            set
            {
                if (value != null)
                {
                    if(!FindItemByKey(key, false, ref value))
                    {
                        throw new ArgumentNullException("There is no item with such key in dictionary");
                    }
                }
                else
                {
                    throw new ArgumentNullException("You can't assign null as value");
                }
            }
        }
        public IEnumerator<DictItem<TKey, TValue>> GetEnumerator()
        {
            foreach (var itemList in items)
            {
                if(itemList != null)
                {
                    foreach (var currentItem in itemList)
                    {
                        yield return currentItem;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            items = null;
            Cleared?.Invoke(this, new DictEventArgs("Dictionary was successfully cleared!"));
        }
    }
}
