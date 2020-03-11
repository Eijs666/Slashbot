using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform cursor;
    public Transform player;
    public Vector3 offset;

    
    private void FixedUpdate()
    {
        if(player != null)
        {
            transform.position = player.position + offset;


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                cursor.position = hitInfo.point;

                cursor.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }

        }
        else
        {
            return;
        }
    }



}
