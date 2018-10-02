using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexTasks
{
    class NumSeqWithDel : Lexer
    {
        protected System.Text.StringBuilder mesString;
		public String letString;

		public NumSeqWithDel(string input)
            : base(input)
        {
            mesString = new System.Text.StringBuilder();
			letString = "";
		}

        public override void Parse()
        {
            NextCh();

			if (Char.IsDigit(currentCh))
			{
				letString += currentCh;
				NextCh();
			}

			while (true)
			{
				if (currentCh == ' ')
				{
					while (currentCh == ' ')
						NextCh();

					if (Char.IsDigit(currentCh))
					{
						letString += currentCh;
						NextCh();
					}
				}
				else
					break;	
			}

            if (currentCharValue != -1)
            {
                Error();
            }

           // System.Console.WriteLine("String with separated chars by " " spaces recognised " + letString);
        }

        public static void Testing()
        {
			/*string input = "5 5  4";
			Lexer L = new NumSeqWithDel(input);
			try
			{
				L.Parse();
			}
			catch (LexerException e)
			{
				System.Console.WriteLine(e.Message);
			}*/

			var tests = new Dictionary<string, string>{
				{ "1 23   4", "error" },
                { "1 2", "12"},
                { "3 2   2", "322"},
                { "32          1", "error"},
				{ "4", "4"},
                { "42 ", "error"},
                { "1131 ;", "error"},
                { "12, 45", "error"},
                { "56     S", "error"},
            };

            int passedTest = 0;
            foreach (var test in tests)
            {
                var L = new NumSeqWithDel(test.Key);
                bool passed = false;
                try
                {
                    L.Parse();
                    passed = L.letString.Equals(test.Value);
                }
                catch (LexerException e)
                {
                    passed = test.Value.Equals("error");
                }

                if (passed)
                {
                    System.Console.WriteLine("Test is passed");
                    passedTest++;
                }
                else
                {
                    System.Console.WriteLine("Test is not passed");
                }
            }
        }
    }
}
