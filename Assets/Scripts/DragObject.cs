using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{

        private Vector3 mOffset;



        private float mZCoord;



        void OnMouseDown()

        {

            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;



            // Store offset = gameobject world pos - mouse world pos

            mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

        }



        private Vector3 GetMouseAsWorldPoint()

        {

            // Pixel coordinates of mouse (x,y)

            Vector3 mousePoint = Input.mousePosition;
            


            // z coordinate of game object on screen

            mousePoint.z = mZCoord;
        //mousePoint.y = gameObject.transform.position.y;



        // Convert it to world points
        Vector3 v1 = Camera.main.ScreenToWorldPoint(mousePoint);
        v1.y = 0;
        return v1;

        }



        void OnMouseDrag()

        {

            transform.position = GetMouseAsWorldPoint() + mOffset;

        }

    }