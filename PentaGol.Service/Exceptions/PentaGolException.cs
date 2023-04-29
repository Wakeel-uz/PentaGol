namespace PentaGol.Service.Exceptions;

public class PentaGolException : Exception
{
    public int Code { get; set; }

	/// <summary>
	/// Exception code with a custom message
	/// </summary>
	/// <param name="code"></param>
	/// <param name="message"></param>
	
	public PentaGolException(int code, string message)
		:base(message)
	{
		this.Code = code;
	}
}
