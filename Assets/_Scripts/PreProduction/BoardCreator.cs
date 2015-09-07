using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class BoardCreator : MonoBehaviour {

	[SerializeField] private GameObject tileViewPrefab;
	
	private Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();

	[SerializeField] private int width = 10;
	[SerializeField] private int depth = 10;
	[SerializeField] private int height = 1;

	[SerializeField] private Point pos;

	[SerializeField] private LevelData levelData;

	public void CreateBoard()
	{
		Clear ();

		for(int i=0; i < width; i++)
		{
			for(int j=0; j < depth; j++)
			{
				for(int k=0; k < height; k++)
				{
					Tile t = GetOrCreate(new Point(i, j));

					while(t.height < height)
					{
						t.Grow();
					}
				}
			}
		}
	}

	private Tile Create ()
	{
		GameObject instance = Instantiate(tileViewPrefab) as GameObject;
		instance.transform.parent = transform;
		return instance.GetComponent<Tile>();
	}
	
	private Tile GetOrCreate (Point p)
	{
		if (tiles.ContainsKey(p))
			return tiles[p];
		
		Tile t = Create();
		t.Load(p, 0);
		tiles.Add(p, t);
		
		return t;
	}

	public void Clear ()
	{
		for (int i = transform.childCount - 1; i >= 0; --i)
			DestroyImmediate(transform.GetChild(i).gameObject);
		tiles.Clear();
	}

	public void Save ()
	{
		string filePath = Application.dataPath + "/Resources/Levels";
		if (!Directory.Exists(filePath))
			CreateSaveDirectory ();
		
		LevelData board = ScriptableObject.CreateInstance<LevelData>();
		board.tiles = new List<Vector3>( tiles.Count );
		foreach (Tile t in tiles.Values)
			board.tiles.Add( new Vector3(t.pos.x, t.height, t.pos.y) );
		
		string fileName = string.Format("Assets/Resources/Levels/{1}.asset", filePath, name);
		AssetDatabase.CreateAsset(board, fileName);
	}
	
	void CreateSaveDirectory ()
	{
		string filePath = Application.dataPath + "/Resources";
		if (!Directory.Exists(filePath))
			AssetDatabase.CreateFolder("Assets", "Resources");
		filePath += "/Levels";
		if (!Directory.Exists(filePath))
			AssetDatabase.CreateFolder("Assets/Resources", "Levels");
		AssetDatabase.Refresh();
	}

	public void Load ()
	{
		Clear();
		if (levelData == null)
			return;
		
		foreach (Vector3 v in levelData.tiles)
		{
			Tile t = Create();
			t.Load(v);
			tiles.Add(t.pos, t);
		}
	}
}
