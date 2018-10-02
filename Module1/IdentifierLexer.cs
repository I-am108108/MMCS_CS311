using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexTasks
{
    public class IdentifierLexer : Lexer
    {

        protected System.Text.StringBuilder IdentifierString;
        public String Identifier;

        public IdentifierLexer(string input)
            : base(input)
        {
			IdentifierString = new System.Text.StringBuilder();
        }

        public override void Parse()
        {
            NextCh();

            if (char.IsLetter(currentCh))
            {
				Identifier += currentCh;
                NextCh();
            }
            else
            {
                Error();
            }

            while (char.IsDigit(currentCh) || char.IsLetter(currentCh))
            {
				Identifier += currentCh;
                NextCh();
            }

            if (currentCharValue != -1)
            {
                Error();
            }

           // System.Console.WriteLine("Identifier is recognized " + Identifier);

        }

        public static void Testing()
		{
			/*string input = "vfsdu";
			Lexer L = new IdentifierLexer(input);
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
				{ "vb", "vb" },
                { "vf4", "vf4"},
                { "abop4s", "abop4s"},
                { "vnfj4;dj", "error"},
                { ".hj", "error"}
            };

            foreach (var test in tests)
            {
                var L = new IdentifierLexer(test.Key);
                bool passed = false;
                try
                {
                    L.Parse();
                    passed = L.Identifier.Equals(test.Value);
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
