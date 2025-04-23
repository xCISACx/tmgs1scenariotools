using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class FileException : Exception
    {
        String file;
        public FileException() { }

        public FileException(string message)
            : base(message) { }

        public FileException(string message, Exception inner)
            : base(message, inner) { }

        public FileException(string message, string file, Exception inner): this(message + " " + file, inner)
        {
            this.file = file;
        }
    }
}
