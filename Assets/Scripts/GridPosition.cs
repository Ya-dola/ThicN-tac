using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPosition : MonoBehaviour
{
    [Header("Shape Details")]
    public bool occupied;

    public ShapeEnum occupiedShape;
    public ShapeSizeEnum occupiedShapeSize;

    [Header("Temporary Visuals")]
    public Material gvShapeX;

    public Material gvShapeO;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Updates Details of the Grid Position
    public void UpdatePosition(ShapeEnum shapeType, ShapeSizeEnum shapeSize)
    {
        occupied = true;
        occupiedShape = shapeType;
        occupiedShapeSize = shapeSize;

        // Temporary Visuals
        if (occupiedShape == ShapeEnum.X)
        {
            GetComponent<MeshRenderer>().material = gvShapeX;
        }
        else
        {
            GetComponent<MeshRenderer>().material = gvShapeO;
        }
    }

    // Checks if grid can be updated or not
    public bool CheckUpdateable(Shape selectedShape)
    {
        // Not occupied previously
        if (!occupied)
        {
            return true;
        }

        // Fail Safe to NOT be able to occupy a slot from the same shape
        if (occupiedShape.Equals(selectedShape.shapeType))
        {
            return false;
        }

        // Fail Safe for Large Shapes that cannot be overriden 
        if (occupiedShapeSize.Equals(ShapeSizeEnum.Large))
        {
            return false;
        }

        // Medium gets Overtaken by Large
        if (occupiedShapeSize.Equals(ShapeSizeEnum.Medium) &&
            selectedShape.shapeSize.Equals(ShapeSizeEnum.Large))
        {
            return true;
        }

        // Small gets Overtaken by Medium and Large
        if (occupiedShapeSize.Equals(ShapeSizeEnum.Small) &&
            (selectedShape.shapeSize.Equals(ShapeSizeEnum.Large) ||
             selectedShape.shapeSize.Equals(ShapeSizeEnum.Medium)))
        {
            return true;
        }

        // Fail safe if none of the conditions are met
        print("<color=yellow>DEBUG: Check Updateable Conditions Not Met</color>");
        return false;
    }
}