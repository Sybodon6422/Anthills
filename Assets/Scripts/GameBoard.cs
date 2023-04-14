using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameBoard : MonoBehaviour
{
    #region Singleton

    private static GameBoard _instance;
    public static GameBoard Instance { get { return _instance; } }
    private void Awake()
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
    }

    public void SetupGameBoard(Vector2Int newMapSize, Grid.TileType[,] savedTileGrid)
    {
        mapSize = newMapSize;
        grid = new Grid(mapSize.x,mapSize.y,savedTileGrid);
    }
    #endregion

    #region MapManagement 
    
    private Vector2Int mapSize;
    public Grid grid;
    public Tilemap worldTileMap, highlightTileMap;

    public Vector3 GetWorldPositionFromCell(Vector3Int cellPos)
    {
        return worldTileMap.CellToWorld(new Vector3Int(cellPos.x,cellPos.z,0));
    }
    public Vector3Int GetCellPositionFromWorld(Vector3 cellPos)
    {
        return worldTileMap.WorldToCell(cellPos);
    }

    public void ClearTileMap()
    {
        highlightTileMap.ClearAllTiles();
    }

    public bool IsWithinMapBounds(Vector3Int tilePos)
    {
    return tilePos.x >= 0 && tilePos.x < mapSize.x && tilePos.y >= 0 && tilePos.y < mapSize.y;
    }

    public Vector3Int GetRandomSpawnPoint(Ant starterUnit)
    {
        for (int i = 0; i < mapSize.x*mapSize.y; i++)
        {
            int randomXPos = Random.Range(1,mapSize.x);
            int randomYPos = Random.Range(1,mapSize.y);

            if(grid.IsWalkable(randomXPos,randomYPos, starterUnit))
            {
            return new Vector3Int(randomXPos,0,randomYPos);
            } 
        }
        Debug.Log("Could not find any valid spawn positions, this shouldnt be possible");
        return Vector3Int.zero;
    }

    public TileBase highlightTile, combatTile;

    #endregion
   
    #region Movement

    private void SetHighlightTile(TileBase tile, Vector3Int pos)
    {
        highlightTileMap.SetTile(pos, tile);
    }

    private void SetHighlightTiles(TileBase tile, List<Vector3Int> positions)
    {
        foreach (var pos in positions)
        {
        highlightTileMap.SetTile(pos, tile);
        }
    }

    public void RoundPiecePosition(Ant piece){
        Vector3Int tilePos = worldTileMap.WorldToCell(piece.transform.position);
        piece.origin = tilePos;
        piece.transform.position = worldTileMap.CellToWorld(tilePos);
    }


    public bool AttemptMove(Ant pieceForMoveAttempt, Vector3Int targetTilePos)
    {
        if (IsWithinMapBounds(targetTilePos))
        {
            return true;
        }
        
        return false;
    }
    
    public void SelectNewPiece(Ant newPiece)
    {
        if(newPiece.player)
        {
            
        }
    }

    #endregion

    #region Building

    public void AddCityTile(int x, int y, Ant unit)
    {
        grid.GetTileData(x,y);
    }

    #endregion
}
