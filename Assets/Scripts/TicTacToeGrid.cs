using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeGrid : MonoBehaviour
{
    public List<GameObject> gridPositions = new List<GameObject>();

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
        foreach (GameObject gridPos in gridPositions)
        {
            if (gridPos.transform.position.Equals(selectedShape.transform.position))
            {
                return true;
            }
        }

        print("<color=red>Shape is not placed</color>");
        return false;
    }
}