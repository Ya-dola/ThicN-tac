using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [Header("Shape Details")]
    public ShapeEnum shapeType;

    public ShapeSizeEnum shapeSize;

    [Header("Debug")]
    public LayerMask gridLayer;

    private void Awake()
    {
        gridLayer = LayerMask.GetMask("GridPositions");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlaceShape()
    {
        Collider[] hits = new Collider[1];
        int collidersHit = Physics.OverlapSphereNonAlloc(transform.position, 0.5f, hits, gridLayer);

        // Fail safe Check if Colliding Properly
        if (collidersHit > 0 && hits[0].gameObject.CompareTag("GridPos"))
        {
            hits[0].gameObject.GetComponent<GridPosition>().UpdatePosition(shapeType, shapeSize);

            // Snap the Shape to the Position of the GridPos that it is Colliding with 
            transform.position = hits[0].gameObject.transform.position;
        }
        else
        {
            print("<color=red>Shape Not Identifying Grid Position</color>");
        }
    }
}