using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDrop : DragDrop
{
    // [Header("Inherited Properties")]
    // public float snapDistance = 1f;

    [Header("Debug")]
    public Vector3 startingPos;

    public TicTacToeGrid ticTacToeGrid;

    public TurnControl turnControl;

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
                    // Check if colliding with a selectable and playable selectable
                    if (!hit.collider.CompareTag(selectableTag) ||
                        !hit.collider.GetComponent<Shape>().shapeType.Equals(turnControl.plyrShapeType))
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
                // Check to see if object in Valid location to be dropped
                if (ticTacToeGrid.ValidDropConds(ref selectedObject))
                {
                    // Old Method
                    // var worldPosition = getWorldMouseZPos();
                    // var dropPos = Vector3.zero;

                    // // Drops the Object according to Mouse Position
                    // switch (moveAxis)
                    // {
                    //     // Y Axis
                    //     case MoveAxisEnum.YAxis:
                    //         dropPos = new Vector3(worldPosition.x, worldPosition.y, 0f);
                    //         break;
                    //     // z Axis
                    //     default:
                    //         dropPos = new Vector3(worldPosition.x, 0f, worldPosition.z);
                    //         break;
                    // }
                    //
                    // selectedObject.transform.position = dropPos;

                    // Snapping Object to Grid Points
                    // SnapToGridPoint(ref dropPos);

                    // Update Grid Position

                    var selectedShape = selectedObject.GetComponent<Shape>();

                    // Fail Safe Check if Shape is selected or not
                    if (selectedShape == null)
                    {
                        print("<color=red>SelectedShape is NULL</color>");
                        return;
                    }

                    // Placing Shape onto Board
                    selectedShape.PlaceShape();

                    // Reset after Dropping the Object to Correct Position
                    selectedObject.tag = "Untagged"; // Changing Tag To not be picked up again
                    selectedObject = null;
                    startingPos = Vector3.zero;
                    Cursor.visible = true;

                    // Check if Possible Win Conditions are met
                    if (ticTacToeGrid.CheckWin())
                    {
                        print("<color=blue>Game WON!</color>");
                    }

                    // Changes Turn of Player to the other Shape
                    turnControl.ChangeActivePlayer();
                }
                else
                {
                    // Dropping Object back to Original position
                    selectedObject.transform.position = startingPos;

                    // Reset after Dropping the Object to Incorrect Position
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
            // SnapToGridPoint(ref movePos);
        }
    }

    // Snaps Selected Shape to Grid Positions when close enough
    // public void SnapToGridPoint(ref Vector3 pos)
    // {
    //     // Snapping Object when close to Grid Points
    //     float smallestDistanceSquared = snapDistance * snapDistance;
    //
    //     foreach (GameObject gridPos in ticTacToeGrid.gridPositions)
    //     {
    //         if ((gridPos.transform.position - pos).sqrMagnitude < smallestDistanceSquared)
    //         {
    //             selectedObject.transform.position = gridPos.transform.position;
    //             smallestDistanceSquared = (gridPos.transform.position - pos).sqrMagnitude;
    //         }
    //     }
    // }
}