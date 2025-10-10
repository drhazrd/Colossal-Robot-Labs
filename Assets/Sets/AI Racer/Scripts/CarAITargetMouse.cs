using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarAITargetMouse : MonoBehaviour {

    [SerializeField] private Transform targetTransform;

    private bool isFollowing = false;

    private void Update() {
        if (isFollowing)
        {
            targetTransform.position = GetMouseWorldPosition();
        }

        if (Input.GetMouseButtonDown(0)) {
            isFollowing = !isFollowing;
        }
    }


    public static Vector3 GetMouseWorldPosition()
    {
        // Get mouse position from the new Input System
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 hitLocation;
        // Raycast from camera to mouse position
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hitLocation = new Vector3(hit.point.x, 0 , hit.point.y);
            return hitLocation;
        }

        return Vector3.zero; // fallback if nothing is hit
    }
}
