using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {
    public class Vectors{
        public static Vector3 Round(Vector3 postion) {
            float x = Mathf.Round(postion.x);
            float y = Mathf.Round(postion.y);
            float z = Mathf.Round(postion.z);


            return new Vector3(x, y, z);
        }
        
        public static Vector2 Round(Vector2 postion) {
            float x = Mathf.Round(postion.x);
            float y = Mathf.Round(postion.y);

            return new Vector2(x, y);
        }
    }
    public class Board{
        public static Vector3 CoordinateTo3DPosition(Vector2 coordinates) {
            return new Vector3(coordinates.x, 0f, coordinates.y);
        }
    }
}