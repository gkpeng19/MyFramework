using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GOMFrameWork.DataEntity
{
    public class EntityItem
    {
        internal EntityItem() { }

        public string Key { get; set; }
        public object Value { get; set; }
    }

    public class SearchItem : EntityItem
    {
        internal SearchItem() { }

        public string Field { get; set; }

        public SearchOperator Operator { get; set; }

        string OperatorToString()
        {
            switch (Operator)
            {
                case SearchOperator.Equal:
                    return " = ";
                case SearchOperator.Greater:
                    return " > ";
                case SearchOperator.Less:
                    return " < ";
                case SearchOperator.GreaterEqual:
                    return " >= ";
                case SearchOperator.LessEqual:
                    return " <= ";
                case SearchOperator.Like:
                    return " like ";
                case SearchOperator.NotEqual:
                    return " <> ";
                case SearchOperator.IsNull:
                    return Field + " is null";
                case SearchOperator.IsNotNull:
                    return Field + " is not null";
                case SearchOperator.In:
                    return " in ";
                default:
                    return " = ";
            }
        }

        internal string ToString(string identify)
        {
            if (Value is string)
            {
                if (Operator == SearchOperator.Like)
                {
                    return Field + OperatorToString() + "N'%" + Value + "%'";
                }
                else if (Operator == SearchOperator.In)
                {
                    return Field +OperatorToString() + "(" + Value + ")";
                }
            }
            if (Operator == SearchOperator.IsNull || Operator == SearchOperator.IsNotNull)
            {
                return OperatorToString();
            }
            else if (Operator == SearchOperator.IsNullZeroEqual)
            {
                return "isnull(" + Field + ",0) = " + Value;
            }
            return Field + OperatorToString() + identify + Key;
        }
    }
}
