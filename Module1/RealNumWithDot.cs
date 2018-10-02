using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexTasks
{
    class RealNumWithDot : Lexer
    {
        protected System.Text.StringBuilder intString;
        public String letString;
        public double doubResult;

        public RealNumWithDot(string input)
            : base(input)
        {
            intString = new System.Text.StringBuilder();
        }

        public override void Parse()
        {
            NextCh();

            if (char.IsDigit(currentCh))
            {
                letString += currentCh;
                NextCh();
            }
            else
            {
                Error();
            }

            while (char.IsDigit(currentCh))
            {
                letString += currentCh;
                NextCh();
            }

            if (currentCh == '.')
            {
                letString += currentCh;
                NextCh();
            }
            else
            {
                Error();
            }

            if (char.IsDigit(currentCh))
            {
                letString += currentCh;
                NextCh();
            }
            else
            {
                Error();
            }

            while (char.IsDigit(currentCh))
            {
                letString += currentCh;
                NextCh();
            }

            if (currentCharValue != -1) // StringReader вернет -1 в конце строки
			{
                Error();
            }
            
           // System.Console.WriteLine("Real numbers with dot is recognized " + letString);

        }

        public static void Testing()
		{
			/*string input = "11.78";
			Lexer L = new RealNumWithDot(input);
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
				{".", "error"},
                { "0.5", "0.5" },
                { "45.", "error"},
                { "0.4678", "0.4678"},
                { "125.895", "125.895"},
                { ".89", "error"},
                { "f12.4", "error"},
            };


            foreach (var test in tests)
            {
                var L = new RealNumWithDot(test.Key);
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
