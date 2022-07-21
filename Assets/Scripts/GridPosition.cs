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
    public bool CheckUpdateable()
    {
        if (occupied)
        {
            // TODO - Logic for size difference 
            return false;
        }

        return true;
    }
}