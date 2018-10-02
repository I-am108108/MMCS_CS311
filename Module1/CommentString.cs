using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexTasks
{
    class CommentString : Lexer
    {
        protected System.Text.StringBuilder intString;
        public String letString;

        public CommentString(string input)
            : base(input)
        {
            intString = new System.Text.StringBuilder();
        }

        public override void Parse()
        {
            NextCh();
            if (currentCh == '/' )
            {
                letString += currentCh;
                NextCh();
            }
            else
            {
                Error();
            }

            if (currentCh == '*')
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
				if (currentCh == '\n')
				{
					Error();
					break;
				}
				if (currentCh == '*')
				{
					NextCh();
					if (currentCh == '/')
					{
						NextCh();
						break;
					}
					if (currentCh == '\n')
					{
						Error();
						break;
					}
				}
				NextCh();
			}

			if (currentCharValue != -1) // StringReader вернет -1 в конце строки
            {
                Error();
            }

        //    System.Console.WriteLine("Comment is recognized " + letString);

        }

        public static void Testing()
        {
			/*string input = "/*Привет";
			Lexer L = new CommentString(input);
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
				{ "/*/", "error"},
				{ "/**/", "/**/" },
                { "/*a*/", "/*a*/"},
                { "/****/", "/****/"},
				{ "/*abcaad*/", "/*abcaad*/"},
                { "abc", "error"},
                { "/ * abc*/", "error"},
                { "/* * /", "error"},
                { "/*", "error"},
            };

            foreach (var t in tests)
            {
                var L = new CommentString(t.Key);
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
