using System;
using System.Net;

namespace EMFPlanner;

public class MyWebClient : WebClient
{
	private int _timeout;

	public int timeout
	{
		get
		{
			return _timeout;
		}
		set
		{
			_timeout = value;
		}
	}

	public void MyWebClient()
	{
		timeout = 60000;
	}

	protected override WebRequest GetWebRequest(Uri address)
	{
		WebRequest webRequest = base.GetWebRequest(address);
		webRequest.Timeout = _timeout;
		return webRequest;
	}
}
