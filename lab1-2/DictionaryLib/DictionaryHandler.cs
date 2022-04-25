using System;

namespace DictionaryLib
{
    public delegate void DictionaryHandler(object o, DictEventArgs e);
    public class DictEventArgs:EventArgs
    {
        public string Message { get; private set; }
        public DictEventArgs(string message)
        {
            Message = message;
        }
    }
}
