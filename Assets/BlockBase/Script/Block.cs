using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block
{
    public Wall[] Walls;
    public Block[] Neighbors;

    public bool IsVisited = false;

    protected GameObject BlockInstance;
    private readonly Material _blockMaterial;
    protected BlockInfo _info;

    protected Vector2Int Coordinate;

    protected Block(GameObject prefab)
    {
        BlockInstance = Object.Instantiate(prefab);
        _blockMaterial = BlockInstance.gameObject.GetComponent<Renderer>().material;
        _info = BlockInstance.gameObject.GetComponent<BlockInfo>();
    }

    public void Destroy()
    {
        foreach (var wall in Walls)
        {
            wall.Destroy();
        }

        if (BlockInstance != null)
        {
            Object.Destroy(BlockInstance);
            BlockInstance = null;
        }
        
    }

    public abstract void Reset();
    public abstract int GetNextDirection(int currentDirection);
    public abstract int GetDirectionCount();

    public abstract void UpdateBlockPosition(Vector2Int baseSize);

    public Vector3 GetBlockPosition()
    {
        return BlockInstance.transform.position;
    }

    public void SetCoordinate(int x, int y)
    {
        Coordinate = new Vector2Int(x, y);
        _info.IndexX = x;
        _info.IndexY = y;
    }

    public Vector2Int GetCoordinate()
    {
        return Coordinate;
    }

    public void MarkVisiting()
    {
        IsVisited = true;
        _blockMaterial.color = BlockConfig.VisitingColor;
    }

    public void MarkVisited()
    {
        IsVisited = true;
        _blockMaterial.color = BlockConfig.VisitedColor;
    }

    public void MarkWillVisit()
    {
        _blockMaterial.color = BlockConfig.WillVisiteColor;
    }

    public void MarkSelected()
    {
        _blockMaterial.color = BlockConfig.SelectedColor;
    }

    public void MarkNormal()
    {
        _blockMaterial.color = BlockConfig.NormalColor;
    }
}

public class BlockCube : Block
{
    public BlockCube(GameObject prefab) : base(prefab)
    {
        Neighbors = new Block[BlockConfig.DirectionCount];
        Walls = new Wall[BlockConfig.DirectionCount];
    }

    public override void Reset()
    {
        IsVisited = false;
    }

    public override int GetNextDirection(int currentDirection)
    {
        return (currentDirection + 1) % BlockConfig.DirectionCount;
    }

    public override int GetDirectionCount()
    {
        return BlockConfig.DirectionCount;
    }

    public override void UpdateBlockPosition(Vector2Int baseSize)
    {
        float x = -(BlockConfig.CubeConfig.CubeSize.x * baseSize.x) / 2.0f + (BlockConfig.CubeConfig.CubeSize.x * Coordinate.x);
        float z = (BlockConfig.CubeConfig.CubeSize.y * baseSize.y) / 2.0f - (BlockConfig.CubeConfig.CubeSize.y * Coordinate.y);
        BlockInstance.transform.position = new Vector3(x, 0, z);
    }
}
