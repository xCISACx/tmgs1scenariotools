using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class ErrorLevelExtensions
    {
        public static string CodeString(this ScenarioToken.Separator me)
        {
            switch (me)
            {
                case ScenarioToken.Separator.Comma:
                    return ",";
                case ScenarioToken.Separator.Colon:
                    return ":";
                case ScenarioToken.Separator.Semicolon:
                    return ";";
                case ScenarioToken.Separator.LeftBraces:
                    return "{";
                case ScenarioToken.Separator.RightBraces:
                    return "}";
                default:
                    throw new Exception("Unimplemented");

            }
        }

        public static string CodeString(this ScenarioToken.Constant me, Reader reader)
        {
            switch (me)
            {
                case ScenarioToken.Constant.Direct:
                    return reader.LatestArg.ToString();
                case ScenarioToken.Constant.Integer:
                    return reader.GetInteger().ToString();
                case ScenarioToken.Constant.Float:
                    return reader.GetFloat().ToString("F1");
                case ScenarioToken.Constant.String:
                    return "\"" + reader.GetString() + "\"";
                case ScenarioToken.Constant.NullString:
                    return "\"\"";
                default:
                    throw new Exception("Unimplemented");
            }
        }
        public static string CodeString(this ScenarioToken.Operator me)
        {
            switch(me)
            {
                case ScenarioToken.Operator.Equal:
                    return "==";
                case ScenarioToken.Operator.Assign:
                    return "=";
                case ScenarioToken.Operator.NotEqual:
                    return "!=";
                case ScenarioToken.Operator.GreaterEqual:
                    return ">=";
                case ScenarioToken.Operator.Greater:
                    return ">";
                case ScenarioToken.Operator.LessEqual:
                    return "<=";
                case ScenarioToken.Operator.Less:
                    return "<";
                case ScenarioToken.Operator.PlusEqual:
                    return "+=";
                case ScenarioToken.Operator.Plus:
                    return "+";
                case ScenarioToken.Operator.MinusEqual:
                    return "-=";
                case ScenarioToken.Operator.Minus:
                    return "-";
                case ScenarioToken.Operator.MultiplyEqual:
                    return "*=";
                case ScenarioToken.Operator.Multiply:
                    return "*";
                case ScenarioToken.Operator.DivideEqual:
                    return "/=";
                case ScenarioToken.Operator.Divide:
                    return "/";
                case ScenarioToken.Operator.ModuloEqual:
                    return "%=";
                case ScenarioToken.Operator.Modulo:
                    return "%";
                case ScenarioToken.Operator.BooleanAnd:
                    return "&&";
                case ScenarioToken.Operator.BitAnd:
                    return "&";
                case ScenarioToken.Operator.BooleanOr:
                    return "||";
                case ScenarioToken.Operator.BitOr:
                    return "|";
                case ScenarioToken.Operator.ExclusiveOr:
                    return "^";
                case ScenarioToken.Operator.LeftParentheses:
                    return "(";
                case ScenarioToken.Operator.RightParentheses:
                    return ")";
                case ScenarioToken.Operator.Increment:
                    return "++";
                case ScenarioToken.Operator.Decrement:
                    return "--";
                case ScenarioToken.Operator.BooleanNot:
                    return "!";
                case ScenarioToken.Operator.BitNot:
                    return "~";
                case ScenarioToken.Operator.SingleMinus:
                    return "-";
                case ScenarioToken.Operator.End:
                    return "END";
                default:
                    throw new Exception("Unimplemented");

            }
        }
    }
}
