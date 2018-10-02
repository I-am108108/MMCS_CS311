using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexTasks
{
    class StringS : Lexer
    {
        protected System.Text.StringBuilder intString;
        public System.String letString;

        public StringS(string input)
            : base(input)
        {
            intString = new System.Text.StringBuilder();
        }

        public override void Parse()
        {
            NextCh();

            if (currentCh == '\'')
            {
                letString += currentCh;
                NextCh();
            }
            else
            {
                Error();
            }

            while (currentCh != '\'' && currentCharValue != -1)
            {
                letString += currentCh;
                NextCh();
            }

            if (currentCh == '\'')
            {
                letString += currentCh;
                NextCh();
            }
            else
            {
                Error();
            }

            if (currentCharValue != -1)
            {
                Error();
            }
            
          //  System.Console.WriteLine("String is recognized " + letString);

        }

        public static void Testing()
        {
			/*string input = "11.78";
			Lexer L = new StringS(input);
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
				{ "''", "''" },
                { "'1'", "'1'"},
                { "'gfefe '", "'gfefe '"},
                { "'tl;dr'", "'tl;dr'"},
                { "fgb'", "error"},
                { "'dsvs", "error"},
                { "dvs'gf", "error"},
            };

            foreach (var test in tests)
            {
                var L = new StringS(test.Key);
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
                }
                else
                {
                    System.Console.WriteLine("Test is not passed");
                }
            }
		}
    }
}
