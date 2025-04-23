using LL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace ConsoleApp1
{
    public class Compiler
    {
        string sourceFile;
        string outFile;

        static EbnfDocument ebnf;
        static Cfg cfg;

        static Compiler()
        {
            prepareEbnf();
        }

        public Compiler(string sourceFile, string outFile)
        {
            this.sourceFile = sourceFile;
            this.outFile = outFile;
        }

        public void Compile() {

            ParseNode node = parse();
            Visitor v = new Visitor();
            
            IEnumerable<TokenBytes> tokens = v.visit(node);

            int tokenOffset = 0x10;
            byte[] header = new byte[tokenOffset];

            using (FileStream w = File.Create(outFile))
            {
                w.Write(header, 0, tokenOffset); //initially all zeros
                
                int offset = tokenOffset;
                foreach (TokenBytes token in tokens)
                {
                    //Debug.WriteLine(token);
                    w.WriteByte(token.type);
                    w.WriteByte(token.id);
                    w.WriteByte((byte)(token.arg>>8));
                    w.WriteByte((byte)(token.arg & 0xFF));
                    offset += 4;
                }
                int intTblOffset = offset;

                int floatTblOffset = offset;
                

                List<KeyValuePair<float, ushort>> listOfFloats = v.mapOfFloatArg.ToList();
                listOfFloats.Sort((a, b) => a.Value - b.Value);
                foreach (KeyValuePair<float, ushort> kv in listOfFloats)
                {
                    byte[] x = BitConverter.GetBytes(kv.Key);
                    foreach(byte b in x ){
                        w.WriteByte(b);
                        offset++;
                    }
                }

                int strTblOffset = offset;

                List<KeyValuePair<string, ushort>> listOfStrings = v.mapOfStringArg.ToList();
                listOfStrings.Sort((a, b) => a.Value - b.Value);
                foreach (KeyValuePair<string, ushort> kv in listOfStrings)
                {
                    foreach (char c in kv.Key)
                    {
                        byte[] x = UTF8Encoding.UTF8.GetBytes(c.ToString());
                        byte key = (byte)(kv.Value | 0x3c);
                        foreach (byte b in x)
                        {
                            byte byteToWrite = (byte)(b ^ key);
                            if (byteToWrite ==0 ) {
                                byteToWrite = key;
                            }
                            w.WriteByte(byteToWrite);
                            offset++;
                        }
                        
                    }
                    w.WriteByte(0);
                    offset++;
                }
                while((offset & 0b11) != 0)
                {
                    w.WriteByte(0);
                    offset++;
                }

                // Write headers
                
                BitConverter.GetBytes(tokenOffset).CopyTo(header, 0x0);
                BitConverter.GetBytes(intTblOffset).CopyTo(header, 0x4);
                BitConverter.GetBytes(floatTblOffset).CopyTo(header, 0x8);
                BitConverter.GetBytes(strTblOffset).CopyTo(header, 0xc);

                w.Seek(0, SeekOrigin.Begin);
                w.Write(header, 0, tokenOffset);

                

            }    
        }
        public bool NeedCompilation()
        {
            //return true;
            return checkFileIsModified();
        }

        bool checkFileIsModified()
        {
            if (!File.Exists(this.outFile))
            {
                return true;
            }
            var outFileWriteTime = File.GetLastWriteTime(this.outFile);
            var sourceFileWriteTime = File.GetLastWriteTime(this.sourceFile);

            var delta = sourceFileWriteTime.Subtract(outFileWriteTime);




            return outFileWriteTime.CompareTo(sourceFileWriteTime) < 0;

        }

        ParseNode parse()
        {

            var tokenizer = ebnf.ToTokenizer(cfg, new FileReaderEnumerable(sourceFile));

            ParseNode node = null;
            using (var parser = cfg.ToLL1Parser(tokenizer))
                while (ParserNodeType.EndDocument != parser.NodeType)
                {
                    ParseNode curNode = parser.ParseSubtree();
                    if (curNode == null)
                    {
                        continue;
                    }
                    //Debug.WriteLine(node);
                    try
                    {
                        checkError(curNode);
                    } catch (Exception e)
                    {
                        throw e;
                    }

                    if (node == null)
                    {
                        node = curNode;
                    }
                    else
                    {
                        throw new Exception("node already exists");
                    }

                }

            return node;     
        }
        static void prepareEbnf()
        {
            if (cfg!= null)
            {
                return;
            }
            string grammar = @"scripts.ebnf";
            ebnf = EbnfDocument.ReadFrom(grammar);

            foreach (var msg in ebnf.Validate(false))
            {
                if (EbnfErrorLevel.Error == msg.ErrorLevel)
                {
                    throw new Exception(msg.ToString());
                }
            }
            cfg = ebnf.ToCfg();
            // we have to prepare a CFG to be parsable by an LL(1)
            // parser. This means removing left recursion, and 
            // factoring out first-first and first-follows conflicts
            // where possible.
            // here we do that, and print any errors we encounter.
            foreach (var msg in cfg.PrepareLL1(true))
            {
                if (CfgErrorLevel.Error == msg.ErrorLevel)
                {
                    throw new Exception(msg.ToString());
                }
            }
        }

        private void checkError(ParseNode node)
        {

            if (node.Symbol == "#ERROR")
            {

                String line_p = "";
                String line_c = "";
                String line_n = "";
                using (var f = File.OpenText(sourceFile))
                {
                    for (int i = 1; i < node.Line +1; i++)
                    {
                        String line = f.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        line_p = line_c;
                        line_c = line_n;
                        line_n = line;

                    }

                }

                String contextLine = String.Join("\n", line_p, line_c, line_n);


                throw new Exception(String.Format("at line {0} col {1}\nCheck code near:\n\n{2}", new string[] { node.Line.ToString(), node.Column.ToString(), contextLine }));
            }

            foreach (ParseNode child in node.Children)
            {
                checkError(child);
            }
        }


        

        public class TokenBytes
        {
            public byte type;
            public byte id;
            public UInt16 arg;
        }
    }



}
