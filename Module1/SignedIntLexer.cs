using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexTasks
{
    public class SignedIntLexer : Lexer
    {

        protected System.Text.StringBuilder intString;
		private static char ZERO = '0';
		public String numString;
        public int intResult;

        public SignedIntLexer(string input)
            : base(input)
        {
            intString = new System.Text.StringBuilder();
        }

        public override void Parse()
        {
            NextCh();
            if (currentCh == '+' || currentCh == '-')
            {
                numString += currentCh;
                NextCh();
            }

            if (char.IsDigit(currentCh) && !currentCh.Equals(ZERO))
            {
                numString += currentCh;
                NextCh();
            }
            else
            {
                Error();
            }

            while (char.IsDigit(currentCh))
            {
                numString += currentCh;
                NextCh();
            }


            if (currentCharValue != -1) // StringReader вернет -1 в конце строки
            {
                Error();
            }

            intResult = Convert.ToInt32(numString);
            //System.Console.WriteLine("Integer without 0 recognized " + intResult);

        }

        public static void Testing()
        {
			/*string input = "554";
			Lexer L = new SignedIntLexer(input);
			try
			{
				L.Parse();
			}
			catch (LexerException e)
			{
				System.Console.WriteLine(e.Message);
			}*/

			var tests = new Dictionary<string, string>{
				{ "", "error" },
				{ "1", "1" },
                { "0", "error"},
                { "124", "124"},
                { "-2455858", "-2455858"},
                { "-01384", "error"},
                { "01384", "error"},
                { "dsv1", "error"},
				{"121sa", "error"}
            };

            foreach (var test in tests)
            {
                var L = new SignedIntLexer(test.Key);
                bool passed = false;
                try
                {
                    L.Parse();
                    passed = L.numString.Equals(test.Value);
                }
                catch (LexerException e)
                {
                    passed = test.Value.Equals("error");
                }

                if (passed)
                {
                    System.Console.WriteLine("Test is passed");
                }
                else
                {
                    System.Console.WriteLine("Test is not passed");
                }
            }
        }
    }
}
