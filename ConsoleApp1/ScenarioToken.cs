using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

	public static class ScenarioToken
	{
		public static readonly int[] LeftPriority;

		// Token: 0x040009EE RID: 2542
		public static readonly int[] RightPriority;

		// Token: 0x02000200 RID: 512
		public enum TokenType : byte
		{
			// Token: 0x040009F0 RID: 2544
			Function,
			// Token: 0x040009F1 RID: 2545
			Reserved,
			// Token: 0x040009F2 RID: 2546
			Identifier,
			// Token: 0x040009F3 RID: 2547
			Operator,
			// Token: 0x040009F4 RID: 2548
			Separator,
			// Token: 0x040009F5 RID: 2549
			Constant
		}

		// Token: 0x02000201 RID: 513
		public enum Reserved : byte
		{
			// Token: 0x040009F7 RID: 2551
			Section,
			// Token: 0x040009F8 RID: 2552
			Table,
			// Token: 0x040009F9 RID: 2553
			If,
			// Token: 0x040009FA RID: 2554
			Else,
			// Token: 0x040009FB RID: 2555
			Switch,
			// Token: 0x040009FC RID: 2556
			Case,
			// Token: 0x040009FD RID: 2557
			Default,
			// Token: 0x040009FE RID: 2558
			While,
			// Token: 0x040009FF RID: 2559
			Break,
			// Token: 0x04000A00 RID: 2560
			Return,
			// Token: 0x04000A01 RID: 2561
			Var,
			// Token: 0x04000A02 RID: 2562
			Int,
			// Token: 0x04000A03 RID: 2563
			Float,
			// Token: 0x04000A04 RID: 2564
			Pause,
			// Token: 0x04000A05 RID: 2565
			Run,
			// Token: 0x4000725
			Label,
			// Token: 0x4000726
			GoTo
		}

		// Token: 0x02000202 RID: 514
		public enum Operator : byte
		{
			// Token: 0x04000A07 RID: 2567
			Equal,
			// Token: 0x04000A08 RID: 2568
			Assign,
			// Token: 0x04000A09 RID: 2569
			NotEqual,
			// Token: 0x04000A0A RID: 2570
			GreaterEqual,
			// Token: 0x04000A0B RID: 2571
			Greater,
			// Token: 0x04000A0C RID: 2572
			LessEqual,
			// Token: 0x04000A0D RID: 2573
			Less,
			// Token: 0x04000A0E RID: 2574
			PlusEqual,
			// Token: 0x04000A0F RID: 2575
			Plus,
			// Token: 0x04000A10 RID: 2576
			MinusEqual,
			// Token: 0x04000A11 RID: 2577
			Minus,
			// Token: 0x04000A12 RID: 2578
			MultiplyEqual,
			// Token: 0x04000A13 RID: 2579
			Multiply,
			// Token: 0x04000A14 RID: 2580
			DivideEqual,
			// Token: 0x04000A15 RID: 2581
			Divide,
			// Token: 0x04000A16 RID: 2582
			ModuloEqual,
			// Token: 0x04000A17 RID: 2583
			Modulo,
			// Token: 0x04000A18 RID: 2584
			BooleanAnd,
			// Token: 0x04000A19 RID: 2585
			BitAnd,
			// Token: 0x04000A1A RID: 2586
			BooleanOr,
			// Token: 0x04000A1B RID: 2587
			BitOr,
			// Token: 0x04000A1C RID: 2588
			ExclusiveOr,
			// Token: 0x04000A1D RID: 2589
			LeftParentheses,
			// Token: 0x04000A1E RID: 2590
			RightParentheses,
			// Token: 0x04000A1F RID: 2591
			Increment,
			// Token: 0x04000A20 RID: 2592
			Decrement,
			// Token: 0x04000A21 RID: 2593
			BooleanNot,
			// Token: 0x04000A22 RID: 2594
			BitNot,
			// Token: 0x04000A23 RID: 2595
			SingleMinus,
			// Token: 0x04000A24 RID: 2596
			End
		}

		// Token: 0x02000203 RID: 515
		public enum Separator : byte
		{
			// Token: 0x04000A26 RID: 2598
			Comma,
			// Token: 0x04000A27 RID: 2599
			Colon,
			// Token: 0x04000A28 RID: 2600
			Semicolon,
			// Token: 0x04000A29 RID: 2601
			LeftBraces,
			// Token: 0x04000A2A RID: 2602
			RightBraces
		}

		// Token: 0x02000204 RID: 516
		public enum Constant : byte
		{
			// Token: 0x04000A2C RID: 2604
			Direct,
			// Token: 0x04000A2D RID: 2605
			Integer,
			// Token: 0x04000A2E RID: 2606
			Float,
			// Token: 0x04000A2F RID: 2607
			String,
			// Token: 0x04000A30 RID: 2608
			NullString
		}

		// Token: 0x02000205 RID: 517
		public enum Header
		{
			// Token: 0x04000A32 RID: 2610
			TokenOfs,
			// Token: 0x04000A33 RID: 2611
			IntegerTblOfs,
			// Token: 0x04000A34 RID: 2612
			FloatTblOfs,
			// Token: 0x04000A35 RID: 2613
			StringTblOfs,
			// Token: 0x04000A36 RID: 2614
			ElementCnt
		}

		public static Operator ParseOperator(string str)
        {
			foreach(Operator o in Enum.GetValues(typeof (Operator)))
            {
				if (o.CodeString() == str)
                {
					return o;
                }
            }
			throw new Exception("unknown operator");
        }
	}
	

}
