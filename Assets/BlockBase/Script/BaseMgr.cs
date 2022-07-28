using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMgr
{

    private static BaseMgr _instance;
    public static BaseMgr Instance()
    {
        return _instance ??= new BaseMgr();
    }

    private BaseMgr()
    {
    }

    private Vector2Int _baseSize;
    private BlockConfig.BlockType _blockType = BlockConfig.BlockType.Cube;

    private Block[,] _baseBlocks;
    private Block _currentSelectedBlock;

    public delegate void OnBlockSelected(Block selectedBlock);

    public OnBlockSelected BlockSelected;

    public void SetBaseSize(int x, int y)
    {
        _baseSize.x = x;
        _baseSize.y = y;
    }

    public Vector2Int GetBaseSize()
    {
        return _baseSize;
    }

    private Block GetNeighbor(int xIndex, int yIndex, int direction)
    {
        switch (direction)
        {
            case BlockConfig.DirectionLeft:
                return xIndex - 1 < 0 ? null : _baseBlocks[xIndex - 1, yIndex];
            case BlockConfig.DirectionUp:
                return yIndex - 1 < 0 ? null : _baseBlocks[xIndex, yIndex - 1];
            case BlockConfig.DirectionRight:
                return xIndex + 1 >= _baseSize.x ? null : _baseBlocks[xIndex + 1, yIndex];
            case BlockConfig.DirectionDown:
                return yIndex + 1 >= _baseSize.y ? null : _baseBlocks[xIndex, yIndex + 1];
        }

        return null;
    }

    public void SetSelectedBlock(int x, int y)
    {
        if (_currentSelectedBlock != null)
        {
            _currentSelectedBlock.MarkNormal();
        }

        _currentSelectedBlock = _baseBlocks[x, y];
        if (BlockSelected != null)
        {
            BlockSelected(_currentSelectedBlock);
        }
    }

    public void DestroyBaseCube()
    {
        if (_baseBlocks == null)
        {
            return;
        }
        foreach (var block in _baseBlocks)
        {
            block.Destroy();
        }

        _baseBlocks = null;
    }
    
    public void GenerateBaseCube(GameObject cubePrefab, GameObject wallPrefab, bool generateWalls)
    {
        _baseBlocks = new Block[_baseSize.x, _baseSize.y];

        for (int xIndex = 0; xIndex < _baseSize.x; xIndex++)
        {
            for (int yIndex = 0; yIndex < _baseSize.y; yIndex++)
            {
                Block newBlock = new BlockCube(cubePrefab);
                newBlock.SetCoordinate(xIndex, yIndex);
                newBlock.UpdateBlockPosition(_baseSize);
                _baseBlocks[xIndex, yIndex] = newBlock;
            }
        }

        for (int xIndex = 0; generateWalls && xIndex < _baseSize.x; xIndex++)
        {
            for (int yIndex = 0; yIndex < _baseSize.y; yIndex++)
            {
                for (int direction = 0; direction < BlockConfig.DirectionCount; direction++)
                {
                    var neighbor = GetNeighbor(xIndex, yIndex, direction);
                    _baseBlocks[xIndex, yIndex].Neighbors[direction] = neighbor;
                    if (neighbor != null && neighbor.Walls[BlockConfig.CubeConfig.GetOppositeDirection(direction)] != null)
                    {
                        _baseBlocks[xIndex, yIndex].Walls[direction] =
                            neighbor.Walls[BlockConfig.CubeConfig.GetOppositeDirection(direction)];
                    }
                    else
                    {
                        var newWall = new Wall(wallPrefab);
                        _baseBlocks[xIndex, yIndex].Walls[direction] = newWall;
                        newWall.UpdatePosition(_baseBlocks[xIndex,yIndex].GetBlockPosition(), direction);
                    }
                }
            }
        }
    }
}
