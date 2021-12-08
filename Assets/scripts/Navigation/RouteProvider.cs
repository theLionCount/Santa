using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RouteProvider
{
	MapModule map;

	List<Vector2Int> route;
	RouteResponse response;

	public RouteProvider()
	{
		map = GameObject.Find("GamePlay").GetComponent<MapModule>();
		route = new List<Vector2Int>();
		response = new RouteResponse();
	}

	void drawRoute()
	{
		if (route.Count > 1)
		{
			for (int i = 1; i < route.Count; i++)
			{
				var ri = map.toWorldCoordinates(route[i]);
				var ri1 = map.toWorldCoordinates(route[i - 1]);
				Debug.DrawLine(new Vector3(ri1.x + 0.5f, ri1.y + 0.5f, 5), new Vector3(ri.x + 0.5f, ri.y + 0.5f, 5), Color.green);


			}

			for (int i = 0; i < route.Count; i++)
			{
				var ri = map.toWorldCoordinates(route[i]);
				Debug.DrawLine(new Vector3(ri.x, ri.y, 0), new Vector3(ri.x + 1, ri.y, 0), Color.red);
				Debug.DrawLine(new Vector3(ri.x + 1, ri.y, 0), new Vector3(ri.x + 1, ri.y + 1, 0), Color.red);
				Debug.DrawLine(new Vector3(ri.x + 1, ri.y + 1, 0), new Vector3(ri.x, ri.y + 1, 0), Color.red);
				Debug.DrawLine(new Vector3(ri.x, ri.y + 1, 0), new Vector3(ri.x, ri.y, 0), Color.red);
			}
		}
	}

	public bool isPointOk(Vector3 pos)
	{
		return map.tileOK(map.toTileCoordinates(pos));
	}

	public Vector2 nextTarget(Vector2 pos, Vector2 target, bool pause = false, bool recurse = false)
	{
		var tilePosS = map.toTileCoordinates(pos);
		var tilePosT = map.toTileCoordinates(target);
		Vector2Int s = new Vector2Int((int)tilePosS.x, (int)tilePosS.y);
		Vector2Int t = new Vector2Int((int)tilePosT.x, (int)tilePosT.y);
		if (!recurse) System.Diagnostics.Debug.WriteLine("---------------------------------");
		if (!recurse) System.Diagnostics.Debug.WriteLine("NextTarget called. Pos/target: " + pos.x  + ", " + pos.y + " / " + target.x + ", " + target.y);
		if (!recurse) System.Diagnostics.Debug.WriteLine("tile pos/target: " + tilePosS.x + ", " + tilePosS.y + " / " + tilePosT.x + ", " + tilePosT.y);
		if (!recurse) System.Diagnostics.Debug.WriteLine("Int tile pos/target " + s.x + ", " + s.y + ", " + t.x + ", " + t.y);
		
		if (s == t) return Vector2Int.zero;

		drawRoute();

		if (route.Contains(s) && route.Contains(t))
		{
			int si = route.IndexOf(s);
			int ti = route.IndexOf(t);
			if (ti > si) return map.toWorldCoordinates(route[si + 1]);
			else return map.toWorldCoordinates(route[si - 1]);
		}
		else
		{
			if (response.start == s && response.end == t)
			{
				lock (response.lck)
				{
					if (response.rdy)
					{
						System.Diagnostics.Debug.WriteLine("using route from provider");
						route = response.route.Select(v => v).ToList();
						return map.toWorldCoordinates(nextTarget(pos, target, pause, true));
					}
					else if (pause) return map.toWorldCoordinates(s);
					else if (route.Contains(s) && route.Last() != s) return map.toWorldCoordinates(route[route.IndexOf(s) + 1]);
					else return map.toWorldCoordinates(s);
				}
			}
			else 
				lock (response.lck)
				{
					if (!response.started)
					{
						System.Diagnostics.Debug.WriteLine("modifying route from provider");
						response.start = s;
						response.end = t;
						map.queRouteRequest(response);
					}
				}

			if (pause) return map.toWorldCoordinates(s);
			else if (route.Contains(s) && route.Last() != s) return map.toWorldCoordinates(route[route.IndexOf(s) + 1]);
		}
		return map.toWorldCoordinates(s);
	}
}
