using System;
using System.Linq;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    [Header("Raycast Tag and Layers")]
    public string selectableTag = "Selectable";

    public float raycastMaxDist = 100f;
    public LayerMask rayCastLayers;

    [Header("Enable Snapping")]
    public bool snapMvmnt = false;

    public bool snapPlcmnt = false;

    [Header("Movement Axis")]
    public MoveAxisEnum moveAxis = MoveAxisEnum.ZAxis;

    [Header("Debug")]
    public GameObject selectedObject = null;

    public Camera mainCam;

    // Movement Directions Enum
    public enum MoveAxisEnum
    {
        XAxis,
        YAxis,
        ZAxis
    }

    protected virtual void Awake()
    {
        mainCam = Camera.main;

        selectedObject = null;
    }

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
                    if (!hit.collider.CompareTag(selectableTag))
                        return;

                    // Assigning the object that was selected as the selected object until reset
                    selectedObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
            }
            // Drops Object if object already selected
            else
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

                if (snapPlcmnt)
                    dropPos = snapPos(dropPos);

                selectedObject.transform.position = dropPos;

                // Reset after Dropping the Object
                selectedObject = null;
                Cursor.visible = true;
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

            if (snapMvmnt)
                movePos = snapPos(movePos);

            selectedObject.transform.position = movePos;
        }
    }

    // Get World Pos from Mouse Pos along Z axis
    private protected Vector3 getWorldMouseZPos()
    {
        // Translate Mouse Position along Z axis
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            mainCam.WorldToScreenPoint(selectedObject.transform.position).z);

        return mainCam.ScreenToWorldPoint(position);
    }

    // Get snap positions according to direction of movement
    private protected Vector3 snapPos(Vector3 freeFormPos)
    {
        Vector3 returnVec = Vector3.zero;

        switch (moveAxis)
        {
            case MoveAxisEnum.YAxis:
                returnVec = new Vector3(Mathf.Round(freeFormPos.x), Mathf.Round(freeFormPos.y), freeFormPos.z);
                break;
            default:
                returnVec = new Vector3(Mathf.Round(freeFormPos.x), freeFormPos.y, Mathf.Round(freeFormPos.z));
                break;
        }

        return returnVec;
    }

    // Raycast from Camera 
    private protected RaycastHit CastRay()
    {
        // Raycast Hit to return
        RaycastHit hit;

        Vector3 screenMousePosFar =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.farClipPlane);

        Vector3 screenMousePosNear =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.nearClipPlane);

        Vector3 worldMousePosFar = mainCam.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = mainCam.ScreenToWorldPoint(screenMousePosNear);

        // Casting Ray from Camera in the Direction away from where the Camera is looking
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit,
            raycastMaxDist, rayCastLayers.value);

        return hit;
    }
}