using System;

namespace LinkedListLibrary
{
    public class ListItem<T>
    {
        private T _data = default(T);
        public T Data
        {
            get => _data;
            set
            {
                if (value != null)
                {
                    _data = value;
                }
                else
                {
                    throw new ArgumentNullException("You passed in uninitialized variable");
                }
            }
        }
        public ListItem<T> Next{get; set;}
        public ListItem(T data)
        {
            Data = data;
        }
    }
}
