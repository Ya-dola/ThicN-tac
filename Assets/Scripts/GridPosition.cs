using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPosition : MonoBehaviour
{
    public bool occupied;

    public ShapeEnum occupiedShape;
    public ShapeSizeEnum occupiedShapeSize;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdatePosition(ShapeEnum shapeType, ShapeSizeEnum shapeSize)
    {
        occupied = true;
        occupiedShape = shapeType;
        occupiedShapeSize = shapeSize;
    }
}