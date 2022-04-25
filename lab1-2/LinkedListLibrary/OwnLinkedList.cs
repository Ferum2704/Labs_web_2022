using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedListLibrary
{
    public class OwnLinkedList<T>:IEnumerable<T>
    {
        public ListItem<T> Head { get; private set; }
        public OwnLinkedList()
        {
            Head = null;
        }
        public OwnLinkedList(T data)
        {
            SetHead(data);
        }

        public void Add(T data)
        {
            if (Head != null)
            {
                ListItem<T> item = new ListItem<T>(data);
                ListItem<T> current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = item;
            }
            else
            {
                SetHead(data);
            }
        }

        public void Remove(T item)
        {
            ListItem<T> tempItem = Head, prevItem = null;
            if(Head.Data.Equals(item))
            {
                Head = Head.Next;
                return;
            }
            while (tempItem != null && !tempItem.Data.Equals(item))
            {
                prevItem = tempItem;
                tempItem = tempItem.Next;
            }
            prevItem.Next = tempItem.Next;
        }
        private void SetHead(T data)
        {
            ListItem<T> item = new ListItem<T>(data);
            Head = item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            ListItem<T> currentItem = Head;
            while(currentItem != null)
            {
                yield return currentItem.Data;
                currentItem = currentItem.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
