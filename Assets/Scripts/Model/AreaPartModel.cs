using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class AreaPartModel : MonoBehaviour
{
    public int length;
    public int height;

    public Vector2[] exits;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private int getNumberOfTimesRotated()
    {
        float yRotation = this.transform.localEulerAngles.y;
        if (UU.IsCloseTo(0, yRotation, 0.5f))
        {
            return 0;
        }
        else if (UU.IsCloseTo(90, yRotation, 0.5f))
        {
            return 1;
        }
        else if (UU.IsCloseTo(180, yRotation, 0.5f))
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    public bool areDimensionsFlipped()
    {
        return getNumberOfTimesRotated() % 2 != 0;
    }

    public float widthAxis()
    {
        return areDimensionsFlipped() ? height : length;
    }

    public float heightAxis()
    {
        return areDimensionsFlipped() ? length : height;
    }

    public Rect getBoundingRect()
    {
        float halfWidth = (widthAxis() / 2);
        float halfHeight = (heightAxis() / 2);
        var position = this.transform.position;
        return Rect.MinMaxRect(position.x - halfWidth, position.z - halfHeight, position.x + halfWidth,
            position.z + halfHeight);
    }

    public bool isAdjacent(AreaPartModel other)
    {
        return UU.IsRectAdjacent(this.getBoundingRect(), other.getBoundingRect());
    }

    public bool bordersPoint(Vector2 point)
    {
        return UU.IsRectAdjacent(this.getBoundingRect(), point);
    }

    public bool hasEntranceAtPoint(Vector2 point)
    {
        if (!bordersPoint(point)) return false;
        else
        {
            return true;
        }

        return false;
    }

    public bool blocksEntrances(AreaPartModel other)
    {
        if (!UU.IsRectAdjacent(other.getBoundingRect(), this.getBoundingRect()))
        {
            return false;
        }
        else
        {
            Rect rect = this.getBoundingRect();
            return true;

            return false;
        }
    }
}