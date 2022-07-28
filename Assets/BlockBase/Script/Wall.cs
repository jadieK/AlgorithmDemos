using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall
{

    private GameObject _wallInstance;
    public Wall(GameObject prefab)
    {
        _wallInstance = Object.Instantiate(prefab);
    }

    public void UpdatePosition(Vector3 blockPosition, int direction)
    {
        Vector3 position;
        Quaternion rotation;
        switch (direction)
        {
            case BlockConfig.DirectionLeft:
                position = new Vector3(blockPosition.x - 0.5f, 0.5f, blockPosition.z);
                rotation = Quaternion.identity;
                break;
            case BlockConfig.DirectionUp:
                position = new Vector3(blockPosition.x, 0.5f, blockPosition.z + 0.5f);
                rotation = Quaternion.Euler(0, 90, 0);
                break;
            case BlockConfig.DirectionRight:
                position = new Vector3(blockPosition.x + 0.5f, 0.5f, blockPosition.z);
                rotation = Quaternion.identity;
                break;
            case BlockConfig.DirectionDown:
                position = new Vector3(blockPosition.x, 0.5f, blockPosition.z - 0.5f);
                rotation = Quaternion.Euler(0, 90, 0);
                break;
            default:
                position = new Vector3();
                rotation = Quaternion.identity;
                break;
        }
        _wallInstance.transform.SetPositionAndRotation(position, rotation);
    }

    public void Destroy()
    {
        if (_wallInstance != null)
        {
            Object.Destroy(_wallInstance);
            _wallInstance = null;
        }
    }
}
