using System;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    [Header("Enable Snapping")]
    public bool snapMvmnt = false;

    public bool snapPlcmnt = false;

    [Header("Debug")]
    public GameObject selectedObject;

    public Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        // Left Click
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
                    Cursor.visible = false;
                }
            }
            // Drops Object if object already selected
            else
            {
                var worldPosition = getWorldMouseZPos();

                // Drops the Object according to Mouse Position
                var dropPos = new Vector3(worldPosition.x, 0f, worldPosition.z);

                if (snapPlcmnt)
                    dropPos = snapPos(dropPos);

                selectedObject.transform.position = dropPos;

                selectedObject = null;
                Cursor.visible = true;
            }
        }

        // Reposition Currently Selected Game Object
        if (selectedObject != null)
        {
            var worldPosition = getWorldMouseZPos();

            // Move Selected Object according to Mouse Position
            var movePos = new Vector3(worldPosition.x, .25f, worldPosition.z);

            if (snapMvmnt)
                movePos = snapPos(movePos);

            selectedObject.transform.position = movePos;
        }
    }

    // Get World Pos from Mouse Pos along Z axis
    private Vector3 getWorldMouseZPos()
    {
        // Translate Mouse Position along Z axis
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            mainCam.WorldToScreenPoint(selectedObject.transform.position).z);

        return mainCam.ScreenToWorldPoint(position);
    }

    private Vector3 snapPos(Vector3 freeFormPos)
    {
        return new Vector3(Mathf.Round(freeFormPos.x), freeFormPos.y, Mathf.Round(freeFormPos.z));
    }

    // Raycast from Camera 
    private RaycastHit CastRay()
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
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}