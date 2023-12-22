using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileData 
{
	public string name;
	public Vector3Int coord;
}


public class TileManager : MonoBehaviour
{
	public Tilemap tilemap;
	public List<TileData> tileDatas;
	public List<GameObject> tileObjs;
	public List<GameObject> prefabs;


	public void Init()
	{
		SetTileDatas();
		SpawnTileObjs();
	}

	public bool HasTileData(Vector3Int coord, string name) 
	{
		return tileDatas.Exists(x => x.coord == coord && x.name == name);
	}

	public TileData GetTileData(string name) 
	{
		return tileDatas.Find(x => x.name == name);
	}


	void SetTileDatas() 
	{
		tileDatas = new();
		foreach (Vector3Int coord in tilemap.cellBounds.allPositionsWithin)
		{
			if (!tilemap.HasTile(coord)) continue;

			TileData tileData = new();
			tileData.name = tilemap.GetTile(coord).name;
			tileData.coord = coord;
			tileDatas.Add(tileData);
		}
	}

	void SpawnTileObjs() 
	{
		tileObjs = new();

		while (transform.childCount > 0) 
		{
			DestroyImmediate(transform.GetChild(0).gameObject);
		}

		foreach (TileData tileData in tileDatas)
		{
			GameObject prefab = prefabs.Find(x => x.name == tileData.name);

			if (prefab == null) continue;

			tilemap.SetTile(tileData.coord, null);
			GameObject tileObj = Instantiate(prefab, tileData.coord, Quaternion.identity, transform);
			tileObj.name = tileData.name;
			tileObjs.Add(tileObj);
		}
	}


}
