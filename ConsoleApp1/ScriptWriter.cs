using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ConsoleApp1
{
    public class ScriptWriter
    {
        string filename;
        public ScriptWriter(String filename)
        {
            this.filename = filename;
        }

        public void write(IEnumerable<string> scriptTokens)
        {
            int indent = 0;
            bool writeIndent = false;
            using (StreamWriter writer = new StreamWriter(filename))
            {
                
                foreach (string tok in scriptTokens)
                {
                    try
                    {
                        if (indent > 0 && writeIndent) {
                            writer.Write(String.Concat(Enumerable.Repeat(" ", 4 * indent)));
                        }
                        writer.Write(tok);
                        writeIndent = false;
                        if (tok == ";")
                        {
                            writer.Write("\n");
                            writeIndent = true;
                        }
                        if (tok == "{")
                        {
                            writer.Write("\n");
                            indent++;
                            writeIndent = true;
                        }
                        if (tok == "}")
                        {
                            writer.Write("\n");
                            indent--;
                            writeIndent = true;
                            if (indent < 0)
                            {
                                throw new Exception("Unbalanced curly brace");
                            }
                        }
                        if (tok == ":")
                        {
                            writer.Write("\n");
                            writeIndent = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(this.filename + "\n  -> " + e.Message);
                        indent = 0;
                    }
                } 
            }
        }
    }
}
