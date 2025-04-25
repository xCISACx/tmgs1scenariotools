using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ConsoleApp1.Compiler;
using LL;

namespace ConsoleApp1
{
    class Visitor
    {
        public Dictionary<string, ushort> mapOfStringArg = new Dictionary<string, ushort>();
        ushort stringIdx = 0;

        public Dictionary<float, ushort> mapOfFloatArg = new Dictionary<float, ushort>();
        ushort floatIdx = 0;

        bool parseAsFloat = false;

        /*
         * ChCustomLayout is so far, the only function that takes multiple type in the arguments:
         * ChCustomLayout(direct, float, float, float)
         * Therefore, we mark flagChCustomLayout as "parseAsFloat", but handle the first argument specially
         */
        bool flagChCustomLayout = false;


        public IEnumerable<TokenBytes> visit(ParseNode node)
        {
            ushort arg;

            if (node.Value != null)
            {
                TokenBytes resultToken = new TokenBytes();
                switch (node.Symbol)
                {
                    case "functionName":
                        ScenarioFunction.Name functionName;
                        string[] parts = node.Value.Split(new[] { '_' }, 2);
                        string groupString = parts[0];
                        string functionString = parts[1];
                        if (!Enum.TryParse<ScenarioFunction.Name>(functionString, out functionName))
                        {
                            throw new Exception("unknown function");
                        }
                        resultToken.type = (byte)ScenarioToken.TokenType.Function;
			
                        if (Enum.TryParse<ScenarioFunction.Group>(groupString, out var parsedGroup))
                        {
                            //Console.WriteLine($"Parsed group: {parsedGroup}");
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid group name: {groupString}");
                        }
                        
                        resultToken.id = (byte)parsedGroup;
                        resultToken.arg = (ushort)functionName;


                        ISet<ScenarioFunction.Group> parseAsFloatGroups = new HashSet<ScenarioFunction.Group>()
                            {
                                /*ScenarioFunction.Group.SoundEffect ,ScenarioFunction.Group.Music ,ScenarioFunction.Group.Sound*/
                            };

                        ISet<ScenarioFunction.Name> parseAsFloatFuncs = new HashSet<ScenarioFunction.Name>() {
                                /*ScenarioFunction.Name.EnvPlay,
                                ScenarioFunction.Name.EnvStop,
                                ScenarioFunction.Name.SEPlay,
                                ScenarioFunction.Name.ChCustomLayout,*/
                            };

                        if (parseAsFloatGroups.Contains(parsedGroup) || parseAsFloatFuncs.Contains(functionName))
                        {
                            parseAsFloat = true;
                            /*if (functionName == ScenarioFunction.Name.ChCustomLayout)
                            {
                                flagChCustomLayout = true;
                            }*/
                        }
                        else
                        {
                            parseAsFloat = false;
                        }


                        break;
                    case "lparen":
                        resultToken.type = (byte)ScenarioToken.TokenType.Operator;
                        resultToken.id = (byte)ScenarioToken.Operator.LeftParentheses;
                        break;
                    case "rparen":
                        resultToken.type = (byte)ScenarioToken.TokenType.Operator;
                        resultToken.id = (byte)ScenarioToken.Operator.RightParentheses;
                        parseAsFloat = false;
                        flagChCustomLayout = false;
                        break;

                    case "comma":
                        resultToken.type = (byte)ScenarioToken.TokenType.Separator;
                        resultToken.id = (byte)ScenarioToken.Separator.Comma;
                        break;
                    case "semicolon":
                        resultToken.type = (byte)ScenarioToken.TokenType.Separator;
                        resultToken.id = (byte)ScenarioToken.Separator.Semicolon;
                        break;
                    case "colon":
                        resultToken.type = (byte)ScenarioToken.TokenType.Separator;
                        resultToken.id = (byte)ScenarioToken.Separator.Colon;
                        break;
                    case "integer":
                        if (parseAsFloat && !flagChCustomLayout)
                        {
                            goto doParseFloat;
                        }
                        if (flagChCustomLayout)
                        {
                            // We just need to handle this flag once.
                            flagChCustomLayout = false;
                        }
                        int intVal = int.Parse(node.Value);

                        if(intVal < 0)
                        {
                            intVal *= -1;
                            TokenBytes negIntToken = new TokenBytes();
                            negIntToken.type = (byte)ScenarioToken.TokenType.Operator;
                            negIntToken.id = (byte)ScenarioToken.Operator.SingleMinus;
                            yield return negIntToken;
                        }

                        if (intVal > 0xffff)
                        {
                            throw new Exception("Require Int table, not implemented");
                        }
                        resultToken.type = (byte)ScenarioToken.TokenType.Constant;
                        resultToken.id = (byte)ScenarioToken.Constant.Direct;
                        resultToken.arg = (ushort)intVal;
                        break;
                    case "string":
                        String stringVal = node.Value.Substring(1, node.Value.Length - 2);

                        stringVal = stringVal.Replace("\\\"", "\"");

                        byte[] stringAsBytes = UTF8Encoding.UTF8.GetBytes(stringVal);

                        if (mapOfStringArg.ContainsKey(stringVal))
                        {
                            arg = mapOfStringArg[stringVal];
                        }
                        else
                        {
                            arg = stringIdx;
                            mapOfStringArg.Add(stringVal, arg);
                            var prevVal = stringIdx;
                             stringIdx += (ushort)(stringAsBytes.Length + 1);
                            if (stringIdx < prevVal)
                            {
                                throw new Exception("Overflow");
                            }
                        }
                        resultToken.type = (byte)ScenarioToken.TokenType.Constant;
                        resultToken.id = (byte)ScenarioToken.Constant.String;
                        resultToken.arg = arg;
                        break;
                    case "float":
                    doParseFloat:
                        float floatVal = float.Parse(node.Value);

                        if (floatVal < 0)
                        {
                            floatVal *= -1;
                            TokenBytes negFloatToken = new TokenBytes();
                            negFloatToken.type = (byte)ScenarioToken.TokenType.Operator;
                            negFloatToken.id = (byte)ScenarioToken.Operator.SingleMinus;
                            yield return negFloatToken;
                        }

                        byte[] floatAsBytes = BitConverter.GetBytes(floatVal);


                        if (mapOfFloatArg.ContainsKey(floatVal))
                        {
                            arg = mapOfFloatArg[floatVal];
                        }
                        else
                        {
                            arg = floatIdx;
                            mapOfFloatArg.Add(floatVal, arg);
                            var prevVal = floatIdx;
                            floatIdx++;
                            if (floatIdx < prevVal)
                            {
                                throw new Exception("Overflow");
                            }
                        }
                        resultToken.type = (byte)ScenarioToken.TokenType.Constant;
                        resultToken.id = (byte)ScenarioToken.Constant.Float;
                        resultToken.arg = arg;
                        break;
                    case "unaryMinus":
                        resultToken.type = (byte)ScenarioToken.TokenType.Operator;
                        resultToken.id = (byte)ScenarioToken.Operator.SingleMinus;
                        break;
                    case "emptyString":
                        resultToken.type = (byte)ScenarioToken.TokenType.Constant;
                        resultToken.id = (byte)ScenarioToken.Constant.NullString;
                        break;
                    case "section":
                        resultToken.type = (byte)ScenarioToken.TokenType.Reserved;
                        resultToken.id = (byte)ScenarioToken.Reserved.Section;
                        break;
                    case "switch":
                        resultToken.type = (byte)ScenarioToken.TokenType.Reserved;
                        resultToken.id = (byte)ScenarioToken.Reserved.Switch;
                        break;
                    case "case":
                        resultToken.type = (byte)ScenarioToken.TokenType.Reserved;
                        resultToken.id = (byte)ScenarioToken.Reserved.Case;
                        break;
                    case "break":
                        resultToken.type = (byte)ScenarioToken.TokenType.Reserved;
                        resultToken.id = (byte)ScenarioToken.Reserved.Break;
                        break;
                    case "default":
                        resultToken.type = (byte)ScenarioToken.TokenType.Reserved;
                        resultToken.id = (byte)ScenarioToken.Reserved.Default;
                        break;
                    case "lbrace":
                        resultToken.type = (byte)ScenarioToken.TokenType.Separator;
                        resultToken.id = (byte)ScenarioToken.Separator.LeftBraces;
                        break;
                    case "rbrace":
                        resultToken.type = (byte)ScenarioToken.TokenType.Separator;
                        resultToken.id = (byte)ScenarioToken.Separator.RightBraces;
                        break;
                    case "varWithType":

                        Match varWithTypeMatch = Regex.Match(node.Value, "([a-z]+)\\s+var([0-9]+)");
                        if (!varWithTypeMatch.Success)
                        {
                            throw new Exception("malformed variable declaration");
                        }
                        string varTypeStr = varWithTypeMatch.Groups[1].Value;
                        string varIdStr = varWithTypeMatch.Groups[2].Value;

                        ushort varID = ushort.Parse(varIdStr);

                        TokenBytes typeToken = new TokenBytes();
                        ScenarioToken.Reserved tokenID;

                        if (!Enum.TryParse(varTypeStr, true, out tokenID))
                        {
                            throw new Exception("Fail to parse variable type declaration");
                        }
                        typeToken.type = (byte)ScenarioToken.TokenType.Reserved;
                        typeToken.id = (byte)tokenID;
                        yield return typeToken;

                        resultToken.type = (byte)ScenarioToken.TokenType.Identifier;
                        resultToken.arg = varID;
                        break;
                    case "assign":
                        resultToken.type = (byte)ScenarioToken.TokenType.Operator;
                        resultToken.id = (byte)ScenarioToken.Operator.Assign;
                        break;

                    case "var":
                        Match varMatch = Regex.Match(node.Value, "var([0-9]+)");
                        if (!varMatch.Success)
                        {
                            throw new Exception("malformed variable declaration");
                        }
                        ushort varNo = ushort.Parse(varMatch.Groups[1].ToString());
                        resultToken.type = (byte)ScenarioToken.TokenType.Identifier;
                        resultToken.arg = varNo;
                        break;
                    case "if":
                        resultToken.type = (byte)ScenarioToken.TokenType.Reserved;
                        resultToken.id = (byte)ScenarioToken.Reserved.If;
                        break;
                    case "else":
                        resultToken.type = (byte)ScenarioToken.TokenType.Reserved;
                        resultToken.id = (byte)ScenarioToken.Reserved.Else;
                        break;
                    case "return":
                        resultToken.type = (byte)ScenarioToken.TokenType.Reserved;
                        resultToken.id = (byte)ScenarioToken.Reserved.Return;
                        break;
                    case "ops":
                        resultToken.type = (byte)ScenarioToken.TokenType.Operator;
                        resultToken.id = (byte) ScenarioToken.ParseOperator(node.Value);
                        break;
                    case "while":
                        resultToken.type = (byte)ScenarioToken.TokenType.Reserved;
                        resultToken.id = (byte)ScenarioToken.Reserved.While;
                        break;
                    case "label":
                        resultToken.type = (byte)ScenarioToken.TokenType.Reserved;
                        resultToken.id = (byte)ScenarioToken.Reserved.Label;
                        break;
                    case "goto":
                        resultToken.type = (byte)ScenarioToken.TokenType.Reserved;
                        resultToken.id = (byte)ScenarioToken.Reserved.GoTo;
                        break;
                    default:
                        throw new Exception("unhandled");
                }
                //Debug.WriteLine(node.Symbol + " " + node.Value);
                yield return resultToken;
            }

            foreach (ParseNode child in node.Children)
            {
                foreach (TokenBytes token in visit(child))
                {
                    yield return token;
                }
            }


            //return;

        }
    }
}
