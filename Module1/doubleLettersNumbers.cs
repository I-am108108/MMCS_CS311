using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexTasks
{
    class doubleLettersNumbers : Lexer
    {
        protected System.Text.StringBuilder intString;
        public String letString;

        public doubleLettersNumbers(string input)
            : base(input)
        {
            intString = new System.Text.StringBuilder();
        }

        public override void Parse()
        {
            NextCh();

            if (char.IsLetter(currentCh))
            {
                letString += currentCh;
                NextCh();
            }
            else
            {
                Error();
            }

            if (char.IsLetter(currentCh))
            {
                letString += currentCh;
                NextCh();
            }
            
            while (char.IsLetter(currentCh) || char.IsDigit(currentCh))
            {
                if (char.IsDigit(currentCh))
                {
                    letString += currentCh;
                    NextCh();
                }
                else
                {
                    Error();
                }
                if (currentCharValue == -1)
                    break;

                if (char.IsDigit(currentCh))
                {
                    letString += currentCh;
                    NextCh();
                }
                if (currentCharValue == -1)
                    break;

                if (char.IsLetter(currentCh))
                {
                    letString += currentCh;
                    NextCh();
                }
                else
                {
                    Error();
                }
                if (currentCharValue == -1)
                    break;
                if (char.IsLetter(currentCh))
                {
                    letString += currentCh;
                    NextCh();
                }
            }

            if (currentCharValue != -1) // StringReader вернет -1 в конце строки
            {
                Error();
            }
            
           // System.Console.WriteLine("Alternate Digits and Letters with groups no more than 2 recognized " + letString);

        }

        public static void Testing()
        {
			/*string input = "nn78";
			Lexer L = new doubleLettersNumbers(input);
			try
			{
				L.Parse();
			}
			catch (LexerException e)
			{
				System.Console.WriteLine(e.Message);
			}*/

			var tests = new Dictionary<string, string>
			{
				{ "", "error"},
				{ "aa12c23dd1", "aa12c23dd1" },
                { "b", "b"},
                { "v6r", "v6r"},
                { "v66", "v66"},
                { "fd65f", "fd65f"},
                { "ddd65", "error"},
                { "nb76f444", "error"},
                { "4vg", "error" },
                { "b6fff;", "error" },
            };

            foreach (var t in tests)
            {
                var L = new doubleLettersNumbers(t.Key);
                bool passed = false;
                try
                {
                    L.Parse();
                    passed = L.letString.Equals(t.Value);
                }
                catch (LexerException e)
                {
                    passed = t.Value.Equals("error");
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
