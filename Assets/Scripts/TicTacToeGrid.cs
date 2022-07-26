using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeGrid : MonoBehaviour
{
    public List<GameObject> gridPositions = new List<GameObject>();

    public float rayDistBack = 1f;
    public float raycastMaxDist = 10f;
    public LayerMask gridLayer;

    private void Awake()
    {
        gridLayer = LayerMask.GetMask("GridPositions");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Checks if all the dropping conditions are valid. Returns True if all are satisfied 
    public bool ValidDropConds(ref GameObject selectedShape)
    {
        return ValidEndPos(ref selectedShape);
    }

    // Checking if End Position is one of the Possible Grid Positions
    private bool ValidEndPos(ref GameObject selectedShape)
    {
        // To start the ray Behind the shape slightly
        var selectedShapePos = new Vector3(
            selectedShape.transform.position.x,
            selectedShape.transform.position.y,
            selectedShape.transform.position.z - rayDistBack);

        // Debug.DrawRay(selectedShapePos, Vector3.forward * raycastMaxDist, Color.red);

        RaycastHit hit;
        Physics.Raycast(selectedShapePos, Vector3.forward, out hit, raycastMaxDist, gridLayer);

        var tempGridPos = hit.collider.gameObject;

        if (hit.collider != null && tempGridPos.CompareTag("GridPos"))
        {
            if (tempGridPos.GetComponent<GridPosition>().CheckUpdateable())
            {
                print("<color=green>Success Hit and Placeable</color>");
                return true;
            }
        }

        // Error Message
        print("<color=red>Not Valid End Position</color>");
        return false;
    }

    // Check If Possible Win Conditions are met
    public bool CheckWin()
    {
        return CheckPositionsMatching(0, 1, 2) ||
               CheckPositionsMatching(3, 4, 5) ||
               CheckPositionsMatching(6, 7, 8) ||
               CheckPositionsMatching(0, 3, 6) ||
               CheckPositionsMatching(1, 4, 7) ||
               CheckPositionsMatching(2, 5, 8) ||
               CheckPositionsMatching(0, 4, 8) ||
               CheckPositionsMatching(2, 4, 6);
    }

    private bool CheckPositionsMatching(int x, int y, int z)
    {
        ShapeEnum xPosShape = gridPositions[x].gameObject.GetComponent<GridPosition>().occupiedShape;
        ShapeEnum yPosShape = gridPositions[y].gameObject.GetComponent<GridPosition>().occupiedShape;
        ShapeEnum zPosShape = gridPositions[z].gameObject.GetComponent<GridPosition>().occupiedShape;

        if (xPosShape.Equals(yPosShape) && xPosShape.Equals(zPosShape))
        {
            print("<color=green>Shapes Match</color>");
            return true;
        }

        print("<color=yellow>Shapes not matching in Positions: (" + x + "," + y + "," + z + ")</color>");
        return false;
    }
}