using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilder : MonoBehaviour
{
    #region singleton
    private static MapBuilder _instance;
    public static MapBuilder Instance { get { return _instance; } }
    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        BuildTileDataFromCurrentMap();
        GameBoard.Instance.SetupGameBoard(mapSize, tileData);
    }
    #endregion

    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Vector2Int mapSize;
    public TileBase wallTile, hollowTile, rockTile;

    private Grid.TileType[,] tileData;

    public void SaveMap()
    {
        tileData = new Grid.TileType[mapSize.x,mapSize.y];
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                var checkTile = tileMap.GetTile(new Vector3Int(x,y,0));
                if(checkTile == wallTile) { tileData[x,y] = Grid.TileType.wall; }
                else if(checkTile == hollowTile) { tileData[x,y] = Grid.TileType.hollow; }
                else if(checkTile == rockTile) { tileData[x,y] = Grid.TileType.rock; }
            }
        }

        MapSaveHandler mapper = new MapSaveHandler();
        mapper.SaveGame(new MapSaveData("New Map",tileData,mapSize));
    }

    public void LoadMap()
    {
        MapSaveHandler mapper = new MapSaveHandler();
        var mapData = mapper.LoadFirstMap();
        tileData = mapData.tileData;
        mapSize = mapData.GetMapSize();
        
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                var checkTile = tileData[x,y];
                Vector3Int currentTilePosition = new Vector3Int(x,y,0);

                switch (checkTile)
                {
                case Grid.TileType.wall:
                    tileMap.SetTile(currentTilePosition, wallTile);
                    break;
                case Grid.TileType.hollow:
                    tileMap.SetTile(currentTilePosition, hollowTile);
                    break;
                case Grid.TileType.rock:
                    tileMap.SetTile(currentTilePosition, rockTile);
                    break;
                }
            }
        }

        FinishMapLoad();
    }

    private void BuildTileDataFromCurrentMap()
    {
        tileData = new Grid.TileType[mapSize.x,mapSize.y];
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                var checkTile = tileMap.GetTile(new Vector3Int(x,y,0));
                if(checkTile == wallTile) { tileData[x,y] = Grid.TileType.wall; }
                else if(checkTile == hollowTile) { tileData[x,y] = Grid.TileType.hollow; }
                else if(checkTile == rockTile) { tileData[x,y] = Grid.TileType.rock; }
            }
        }

    }

    private void FinishMapLoad()
    {
        GameBoard.Instance.SetupGameBoard(mapSize,tileData);
        Destroy(this);
    }
}
