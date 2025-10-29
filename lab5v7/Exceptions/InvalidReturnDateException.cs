namespace Lab5v7.Exceptions;


public class InvalidReturnDateException : Exception
{
    public InvalidReturnDateException(string message) : base(message) { }
}