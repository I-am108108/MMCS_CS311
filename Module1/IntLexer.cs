using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexTasks
{
    public class IntLexer : Lexer
    {

        protected System.Text.StringBuilder intString;
        public String numString;
        public int intResult;

        public IntLexer(string input)
            : base(input)
        {
            intString = new System.Text.StringBuilder();
        }

        public override void Parse()
        {
            NextCh();
            if (currentCh == '+' || currentCh == '-')
            {
				if (currentCh == '-')
				{
					numString += currentCh;
				}
				NextCh();
            }

            if (char.IsDigit(currentCh))
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
          //  System.Console.WriteLine("Integer is recognized " + intResult);

        }

        public static void Testing()
        {
			/*string input = "554";
			Lexer L = new IntLexer(input);
			try
			{
				L.Parse();
			}
			catch (LexerException e)
			{
				System.Console.WriteLine(e.Message);
			}*/

			var tests = new Dictionary<string, string>{
				{ "", "error"},
				{ "+245", "245" },
                { "1", "1"},
				{ "123j", "error"},
				{ "-99", "-99"},
                { "340222", "340222"},
                { "dfskh", "error"}
            };

            foreach (var test in tests)
            {
                var L = new IntLexer(test.Key);
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
