using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexTasks
{
    public class LettersNumbersAlt : Lexer
    {
        protected System.Text.StringBuilder mesString;
        public String numString;
		private bool previousSymbolIsLetter = true;

		public LettersNumbersAlt(string input)
            : base(input)
        {
            mesString = new System.Text.StringBuilder();
        }

        public override void Parse()
        {
			NextCh();
			if (char.IsLetter(currentCh))
			{
				numString += currentCh;
				NextCh();
				while (previousSymbolIsLetter && char.IsDigit(currentCh) || !previousSymbolIsLetter && char.IsLetter(currentCh))
				{
					numString += currentCh;
					previousSymbolIsLetter = char.IsLetter(currentCh);
					NextCh();
				}
				if (currentCharValue != -1)
				{
					Error();
				}
			}
			else
			{
				Error();
			}

			//System.Console.WriteLine("Alternate sequence of letters & digits which starts with a letter is recognized" + numString);
		}

		public static void Testing()
		{
			/*string input = "554";
			Lexer L = new LettersNumbersAlt(input);
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
                { "1", "error"},
                { "c", "c"},
                { "y3y3", "y3y3"},
                { "y33", "error"},
                { "n1n3h4k", "n1n3h4k"},
                { "b2b3b44", "error"}
            };

            foreach (var test in tests)
            {
                var L = new LettersNumbersAlt(test.Key);
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
