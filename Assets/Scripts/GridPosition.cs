using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPosition : MonoBehaviour
{
    [Header("Shape Details")]
    public bool occupied;

    public ShapeEnum occupiedShapeType;
    public ShapeSizeEnum occupiedShapeSize;

    public Shape occupiedShape;

    [Header("Debug Visuals")]
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

    // Logic for Grid Position to Occupy Shape
    private void OccupyShape(Shape shape)
    {
        occupied = true;
        occupiedShapeType = shape.shapeType;
        occupiedShapeSize = shape.shapeSize;
        occupiedShape = shape;

        // Assigning Debug Visuals
        GetComponent<MeshRenderer>().material = occupiedShapeType == ShapeEnum.X ? gvShapeX : gvShapeO;
    }

    // Updates Details of the Grid Position
    public void UpdatePosition(Shape shape)
    {
        // Not Occupied previously logic
        if (!occupied)
        {
            OccupyShape(shape);
        }
        // Already Occupied Logic
        else
        {
            // Call Take Over Logic for Existing Shape
            occupiedShape.TakeOverShape();

            OccupyShape(shape);
        }
    }

    // Checks if grid can be updated or not
    public bool CheckUpdateable(Shape selectedShape)
    {
        // Not occupied previously logic
        if (!occupied)
        {
            return true;
        }

        // Already Occupied logic

        // Fail Safe to NOT be able to occupy a slot from the same shape
        if (occupiedShapeType.Equals(selectedShape.shapeType))
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
        print("<color=yellow>DEBUG: CheckUpdateable Conditions Not Met</color>");
        return false;
    }
}