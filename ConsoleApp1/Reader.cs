using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Reader
    {
        byte[] bin;
        

        int TokenCur = 0;
        int TokenOfs = 0;
        int TokenEnd = 0;
        int StringTblOfs = 0;
        int IntegerTblOfs = 0;
        int FloatTblOfs = 0;
        int LatestCur = 0;
        int LatestType = 0;
        public int LatestArg = 0;
        byte LatestId = 0;
        int FileSize;

        int DebugIdx = 0;

        int indent = 0;

        String nameWithoutExt;

        public Reader(String fileName)
        {
            this.nameWithoutExt = null;
            byte[] bin = File.ReadAllBytes(fileName);
            this.bin = bin;
            this.FileSize = bin.Length;
            this.Resist();
        }
        public Reader(String fileName, String nameWithoutExt)
        {
            this.nameWithoutExt = nameWithoutExt;
            byte[] bin = File.ReadAllBytes(fileName);
            this.bin = bin;
            this.FileSize = bin.Length;
            this.Resist();
        }

        public void Resist()
        {
            int startOfs = 0x04;

            this.TokenOfs = BitConverter.ToInt32(bin, startOfs);
            /* if (this.TokenOfs != 0x10)
            {
                throw new Exception("offset is not 0x10");
            }
            */
            this.IntegerTblOfs = BitConverter.ToInt32(bin, this.TokenOfs + 0x04);
            this.FloatTblOfs = BitConverter.ToInt32(bin, this.TokenOfs + 0x08);

            int x10 = this.IntegerTblOfs - 0x10;
            int x8_6 = this.IntegerTblOfs - 0xd;
            if (x10 < 0)
            {
                this.TokenEnd = x8_6;
            } else
            {
                this.TokenEnd = x10;
            }
            this.TokenEnd = this.TokenEnd >> 2;

            this.StringTblOfs = BitConverter.ToInt32(bin, this.TokenOfs + 0x0c);
            return;


        }
        public void Run()
        {
            //Analyze();
            ScriptTokens();
        }

        public IEnumerable<string> ScriptTokens()
        {
            while (fetchToken())
            {
                string tok = print();
                yield return tok;
            }

        }

        string print()
        {
            ScenarioToken.TokenType t = (ScenarioToken.TokenType)this.LatestType;
            switch (t)
            {
                case ScenarioToken.TokenType.Function:

                    int group = this.LatestId;
                    int id = this.LatestArg;
                    ScenarioFunction.Name fName = (ScenarioFunction.Name)id;
                    ScenarioFunction.Group fGroup = (ScenarioFunction.Group)group;
                    return fName.ToString();
                case ScenarioToken.TokenType.Operator:
                    ScenarioToken.Operator op = (ScenarioToken.Operator)this.LatestId;
                    return op.CodeString();
                case ScenarioToken.TokenType.Separator:
                    ScenarioToken.Separator sep = (ScenarioToken.Separator)this.LatestId;
                    return sep.CodeString();
                case ScenarioToken.TokenType.Reserved:
                    ScenarioToken.Reserved reserved = (ScenarioToken.Reserved)this.LatestId;
                    return reserved.ToString().ToLower() + " "; 
                case ScenarioToken.TokenType.Constant:
                    ScenarioToken.Constant constant = (ScenarioToken.Constant)this.LatestId;
                    return constant.CodeString(this);
                case ScenarioToken.TokenType.Identifier:
                    return " var" + this.LatestArg.ToString() + " ";
                default:
                    throw new Exception("Unimplemented");
            }
        }

        public void Analyze()
        {
            int state = 0;
            //this.TokenCur = 0x63; // Test

            while (true) {
                if (state == 0)
                {
                    if (fetchToken())
                    {
                        state = 0;

                        if (LatestType != 1)
                        {
                            continue;
                        }
                        else
                        {
                            int puf;
                            if (LatestId == 1)
                            {
                                puf = 1;
                            } else
                            {
                                puf = 0;
                            }
                            int nextState = puf << 1;
                            if (LatestId != 0) {
                                state = nextState;
                                continue;
                            } else
                            {
                                state = 1;
                                continue;
                            }
                        }
                    }

                } else {
                    if (state == 1)
                    {
                        if (fetchToken())
                        {
                            if (LatestType == 5 && LatestId == 3)
                            {
                                string name = GetString();

                                return;

                            }
                        }
                    }
                }
                throw new Exception("TODO");
                

                Debug.WriteLine("");
            }
            
        }

        bool fetchToken()
        {
            int cur = this.TokenCur;
            
            if (cur >= this.TokenEnd)
            {
                this.LatestCur = cur;
                this.LatestType = 0x901;
                return false;
            }
            
            
            int offset = this.TokenOfs + (cur << 2);

            int p = offset + 2;
            int q = offset + 3;

            byte P = this.bin[p];
            byte Q = this.bin[q];
            this.LatestCur = cur;
            int f = offset + 1;
            this.LatestType = bin[offset];

            this.DebugIdx = offset;

            byte F = this.bin[f];
            this.LatestArg = Q | P << 8;
            this.LatestId = F;
            this.TokenCur = cur + 1;

            return true;
        }

        public string GetString()
        {
            if (LatestType ==5 && LatestId == 3)
            {
                IEnumerable<byte> buf = new List<Byte>().AsEnumerable();
                int offset = this.StringTblOfs + (Int32) this.LatestArg;
                if (offset < this.FileSize)
                {
                    int idx = offset;
                    do
                    {
                        if (offset > this.FileSize)
                        {
                            break;
                        }
                        byte chr = this.bin[idx];
                        if (chr == 0)
                        {
                            break;
                        }
                        buf = buf.Append(chr);
                        offset++;
                        idx++;
                    } while (offset < this.FileSize);
                }

                byte[] chrBuf = buf.ToArray();


                if (offset >= this.FileSize || offset < this.FileSize && offset < this.bin.Length){
                    byte arg = (byte) this.LatestArg;
                    int lenOfBuff = chrBuf.Length;
                    if (lenOfBuff >= 1)
                    {
                        byte key = ((byte)(arg | 0x3c));
                        int i = 0;
                        while ( i < lenOfBuff)
                        {
                            byte newChr = (byte)(chrBuf[i] ^ key);
                            if (newChr != 0)
                            {
                                chrBuf[i] = newChr;
                            }
                            i++;
                            if (i >= lenOfBuff)
                            {
                                break;
                            }
                        }

                        String s = Encoding.UTF8.GetString(chrBuf);
                        return s;

                    }
                }
            }
            throw new IndexOutOfRangeException();
        }

        
        public UInt32 GetInteger()
        {
            if (LatestType == 5)
            {
                if (LatestId == 1 )
                {
                    UInt32 x = BitConverter.ToUInt32(this.bin, this.IntegerTblOfs + (this.LatestArg << 2));
                    return x;
                } 
                if (LatestId != 0)
                {
                    return 0;
                }
                return (uint)this.LatestArg;
            } else
            {
                throw new Exception("not integer");
            }
            
        }
        public float GetFloat()
        {
            if (LatestType == 5 && LatestId == 2)
            {
                int idx = this.FloatTblOfs + (this.LatestArg << 2);

                float f = BitConverter.ToSingle(this.bin,  idx );

                return f;
            }
            throw new Exception("not a float");
        }

        public IEnumerable<TranslationData> TranslationData()
        {
            while (fetchToken())
            {
                if (LatestType == 5 && LatestId == 3)
                {
                    String text = GetString();
                    yield return new TranslationData
                    {
                        Idx = this.DebugIdx,
                        Filename = this.nameWithoutExt,
                        Argument = this.LatestArg.ToString("X"),
                        Cur = this.LatestCur,
                        TextJa = text,
                        TextEn = text,
                    };
                }
            }
        }

    }
}
