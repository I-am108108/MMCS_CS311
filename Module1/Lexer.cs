using System;
using System.Collections.Generic;

using LexTasks;

public class LexerException : System.Exception
{
    public LexerException(string msg)
        : base(msg)
    {
    }
}

public class Lexer
{

    protected int position;
    protected char currentCh;       // очередной считанный символ
    protected int currentCharValue; // целое значение очередного считанного символа
    protected System.IO.StringReader inputReader;
    protected string inputString;

    public Lexer(string input)
    {
        inputReader = new System.IO.StringReader(input);
        inputString = input;
    }

    public void Error()
    {
        System.Text.StringBuilder o = new System.Text.StringBuilder();
        o.Append(inputString + '\n');
        o.Append(new System.String(' ', position - 1) + "^\n");
        o.AppendFormat("Error in symbol {0}", currentCh);
        throw new LexerException(o.ToString());
    }

    protected void NextCh()
    {
        this.currentCharValue = this.inputReader.Read();
        this.currentCh = (char)currentCharValue;
        this.position += 1;
    }

    public virtual void Parse()
    {

    }
}


public class Program
{
    public static void Main()
    {
		IntLexer.Testing();
		IdentifierLexer.Testing();
		SignedIntLexer.Testing();
		LettersNumbersAlt.Testing();
		LettersSeqWithDel.Testing();

		//Дополнительные
		NumSeqWithDel.Testing();
		doubleLettersNumbers.Testing();
		RealNumWithDot.Testing();
		StringS.Testing();
		CommentString.Testing();
	}
}