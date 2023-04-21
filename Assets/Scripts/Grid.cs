using System;
using UnityEngine;

public class Grid
{
    private TileData[,] position;

    public Grid(int xSize, int ySize, TileType[,] tileData)
    {
        position = new TileData[xSize,ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                position[x,y] = new TileData(tileData[x,y]);
            }
        }
    }

    public bool IsWalkable(int x, int y, Ant callingUnit)
    {
        return position[x,y].IsTileWalkable(callingUnit);
    }

    public bool GetIsOccupied(int x, int y)
    {
        bool occupied = (position[x,y].Occupier);
        return occupied;
    }
    public bool GetIsOccupied(Vector3Int pos)
    {
        bool occupied = (position[pos.x,pos.y].Occupier);
        return occupied;
    }

    public void SetIsOccupied(Ant occupier)
    {
        position[occupier.origin.x,occupier.origin.y].Occupier = occupier;
    }

    public void SetIsOccupied(int x, int y, Ant occupier)
    {
        position[x,y].Occupier = occupier;
    }
    public Ant GetOccupier(int x, int y){
        return position[x,y].Occupier;
    }

    public Ant GetOccupier(Vector3Int pos){
        return position[pos.x,pos.y].Occupier;
    }

    public TileData GetTileData(int x, int y)
    {
        return position[x,y];
    }

    public struct TileData
    {
        public TileType tile;
        public Building building;
        public Ant Occupier;

        public bool IsTileWalkable(Ant unitCaller)
        {
            if(tile == TileType.hollow)
            {
                return true;
            }else{return false;}
        }

        public TileData(TileType typeOfTile)
        {
            tile = typeOfTile;
            building = null;
            Occupier = null;
        }
    }

    [Serializable]
    public enum TileType
    {
        wall,
        hollow,
        rock
    }
}
