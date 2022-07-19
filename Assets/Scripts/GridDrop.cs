using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDrop : DragDrop
{
    [Header("Inherited Properties")]
    public float snapDistance = 1f;

    public List<Transform> gridPositions = new List<Transform>();

    [Header("Debug")]
    public Vector3 startingPos;

    // Update is called once per frame
    private void Update()
    {
        // On Left Click
        if (Input.GetMouseButtonDown(0))
        {
            // If object not yet Selected
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();

                // If Raycast is Hitting an object's collider
                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Selectable"))
                        return;

                    // Assigning the object that was selected as the selected object until reset
                    selectedObject = hit.collider.gameObject;

                    // Setting the Starting Position to Return if Selected Object was dropped incorrectly
                    startingPos = selectedObject.transform.position;

                    // Hide Cursor on Selecting Object
                    Cursor.visible = false;
                }
            }
            // Drops Object if object already selected
            else
            {
                // Check to see if object can be dropped in location
                if (ValidDropConds())
                {
                    var worldPosition = getWorldMouseZPos();
                    var dropPos = Vector3.zero;

                    // Drops the Object according to Mouse Position
                    switch (moveAxis)
                    {
                        // Y Axis
                        case MoveAxisEnum.YAxis:
                            dropPos = new Vector3(worldPosition.x, worldPosition.y, 0f);
                            break;
                        // z Axis
                        default:
                            dropPos = new Vector3(worldPosition.x, 0f, worldPosition.z);
                            break;
                    }

                    selectedObject.transform.position = dropPos;

                    // Snapping Object to Grid Points
                    SnapToGridPoint(ref dropPos);

                    // Reset after Dropping the Object
                    selectedObject = null;
                    startingPos = Vector3.zero;
                    Cursor.visible = true;
                }
                else
                {
                    // Dropping Object back to Original position
                    selectedObject.transform.position = startingPos;

                    // Reset after Dropping the Object
                    selectedObject = null;
                    startingPos = Vector3.zero;
                    Cursor.visible = true;
                }
            }
        }

        // Reposition Currently Selected Game Object
        if (selectedObject != null)
        {
            var worldPosition = getWorldMouseZPos();
            var movePos = Vector3.zero;

            // Move Selected Object according to Mouse Position
            switch (moveAxis)
            {
                // Y Axis
                case MoveAxisEnum.YAxis:
                    movePos = new Vector3(worldPosition.x, worldPosition.y, -0.25f);
                    break;
                // z Axis
                default:
                    movePos = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                    break;
            }

            selectedObject.transform.position = movePos;

            // Snapping Object when close to Grid Points
            SnapToGridPoint(ref movePos);
        }
    }

    // Checks if all the dropping conditions are valid. Returns True if all are satisfied 
    private bool ValidDropConds()
    {
        return ValidEndPos();
    }

    // Checking if End Position is one of the Possible Grid Positions
    private bool ValidEndPos()
    {
        foreach (Transform gridPos in gridPositions)
        {
            if (gridPos.position.Equals(selectedObject.transform.position))
            {
                return true;
            }
        }

        return false;
    }

    public void SnapToGridPoint(ref Vector3 pos)
    {
        // Snapping Object when close to Grid Points
        float smallestDistanceSquared = snapDistance * snapDistance;

        foreach (Transform gridPos in gridPositions)
        {
            if ((gridPos.position - pos).sqrMagnitude < smallestDistanceSquared)
            {
                selectedObject.transform.position = gridPos.position;
                smallestDistanceSquared = (gridPos.position - pos).sqrMagnitude;
            }
        }
    }
}