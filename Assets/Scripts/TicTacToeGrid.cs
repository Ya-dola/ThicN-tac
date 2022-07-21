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

        if (hit.collider != null && hit.collider.gameObject.CompareTag("GridPos"))
        {
            print("<color=green>Success Hit</color>");
            return true;
        }

        // Error Message
        print("<color=red>Not Valid End Position</color>");
        return false;
    }
}