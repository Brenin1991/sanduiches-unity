using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Marker : MonoBehaviour
{
    public Image img;
    public Transform target;
    public Vector3 offset;
    public Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        //cam = camManager.camSel;
        // Giving limits to the icon so it sticks on the screen
        // Below calculations witht the assumption that the icon anchor point is in the middle
        // Minimum X position: half of the icon width
        float minX = img.GetPixelAdjustedRect().width / 2;
        // Maximum X position: screen width - half of the icon width
        float maxX = Screen.width - minX;

        // Minimum Y position: half of the height
        float minY = img.GetPixelAdjustedRect().height / 2;
        // Maximum Y position: screen height - half of the icon height
        float maxY = Screen.height - minY;

        // Temporary variable to store the converted position from 3D world point to 2D screen point
        Vector2 pos = cam.WorldToScreenPoint(target.position + offset);

        // Check if the target is behind us, to only show the icon once the target is in front
        if(Vector3.Dot((target.position - cam.transform.position), cam.transform.forward) < 0)
        {
            // Check if the target is on the left side of the screen
            if(pos.x < Screen.width / 2)
            {
                // Place it on the right (Since it's behind the player, it's the opposite)
                pos.x = maxX;
            }
            else
            {
                // Place it on the left side
                pos.x = minX;
            }
        }

        // Limit the X and Y positions
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Update the marker's position
        img.transform.position = pos;
        
    }
}