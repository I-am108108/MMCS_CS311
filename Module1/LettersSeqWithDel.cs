using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexTasks
{
    public class LettersSeqWithDel : Lexer
    {
        protected System.Text.StringBuilder mesString;
        public String letString;

		public LettersSeqWithDel(string input)
            : base(input)
        {
			mesString = new System.Text.StringBuilder();
            letString = "";
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

            while (true)
            {
                if (char.IsLetter(currentCh))
                {
                    letString += currentCh;
                    NextCh();
                }
                else if (currentCh == ',' || currentCh == ';')
                {
                    NextCh();
                    break;
                }
                else
                {
                    Error();
                }
            }

            if (char.IsLetter(currentCh))
            {
                letString += currentCh;
                NextCh();
            }
            else
            {
                Error();
            }

            while (char.IsLetter(currentCh))
            {
                letString += currentCh;
                NextCh();
            }

            if (currentCharValue != -1)
            {
                Error();
            }

         // System.Console.WriteLine("string with separated chars by ; or , is recognised " + letString);
        }

        public static void Testing()
		{
			/*string input = "554";
			Lexer L = new LettersSeqWithDel(input);
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
                { "n,", "error"},
                { "asd,asd", "asdasd"},
                { "asd;", "error"},
                { ";fd,", "error"},
                { "asd;asd", "asdasd"},
                { ",glO", "error"},
            };

            foreach (var test in tests)
            {
                var L = new LettersSeqWithDel(test.Key);
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
