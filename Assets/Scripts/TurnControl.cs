using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControl : MonoBehaviour
{
    [Header("Debug")]
    public ShapeEnum plyrShapeType;


    // Start is called before the first frame update
    void Start()
    {
        RandomizeActivePlayer();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Randomly chooses which player is going to start 
    private void RandomizeActivePlayer()
    {
        plyrShapeType = Random.Range(0f, 1f) >= 0.5f ? ShapeEnum.X : ShapeEnum.O;
    }

    public void ChangeActivePlayer()
    {
        plyrShapeType = plyrShapeType.Equals(ShapeEnum.X) ? ShapeEnum.O : ShapeEnum.X;
    }
}