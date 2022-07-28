using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockConfig
{
    public enum BlockType
    {
        Cube,
        Hex
    }
    
    public static readonly Color VisitingColor = Color.red;
    public static readonly Color VisitedColor = Color.black;
    public static readonly Color WillVisiteColor = Color.blue;
    public static readonly Color SelectedColor = Color.yellow;
    public static readonly Color NormalColor = Color.white;
    
    public static class CubeConfig
    {
        public static readonly Vector2Int CubeSize = new Vector2Int(1, 1);

        public static int GetOppositeDirection(int direction)
        {
            return (direction + 2) % DirectionCount;
        }
    }

    public const int DirectionLeft = 0;
    public const int DirectionUp = 1;
    public const int DirectionRight = 2;
    public const int DirectionDown = 3;
    public const int DirectionCount = 4;
}