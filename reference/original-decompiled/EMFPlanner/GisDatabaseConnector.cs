using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using Newtonsoft.Json;

namespace EMFPlanner;

public class GisDatabaseConnector
{
	private WebClient Client;

	public GisDatabaseConnector()
	{
		Client = new WebClient();
		Client.UseDefaultCredentials = true;
		new WebProxy();
		Client.Proxy = WebRequest.GetSystemWebProxy();
	}

	public List<Building> GetBuildings(string Geometry)
	{
		NameValueCollection queryStringCollection = new NameValueCollection
		{
			{ "get", "building_sz" },
			{ "q", Geometry }
		};
		return JsonConvert.DeserializeObject<List<Building>>(Query(queryStringCollection));
	}

	public List<Building> GetHighway(string Geometry)
	{
		NameValueCollection queryStringCollection = new NameValueCollection
		{
			{ "get", "highway_sz" },
			{ "q", Geometry }
		};
		return JsonConvert.DeserializeObject<List<Building>>(Query(queryStringCollection));
	}

	public List<Building> GetBarrier(string Geometry)
	{
		NameValueCollection queryStringCollection = new NameValueCollection
		{
			{ "get", "barrier_sz" },
			{ "q", Geometry }
		};
		return JsonConvert.DeserializeObject<List<Building>>(Query(queryStringCollection));
	}

	public List<Building> GetLeisure(string Geometry)
	{
		NameValueCollection queryStringCollection = new NameValueCollection
		{
			{ "get", "leisure_sz" },
			{ "q", Geometry }
		};
		return JsonConvert.DeserializeObject<List<Building>>(Query(queryStringCollection));
	}

	public List<Building> GetAmenity(string Geometry)
	{
		NameValueCollection queryStringCollection = new NameValueCollection
		{
			{ "get", "amenity_sz" },
			{ "q", Geometry }
		};
		return JsonConvert.DeserializeObject<List<Building>>(Query(queryStringCollection));
	}

	private string Query(NameValueCollection QueryStringCollection)
	{
		Client.QueryString = QueryStringCollection;
		return Client.DownloadString(Constants.UrlROOT + "/index.php");
	}
}
