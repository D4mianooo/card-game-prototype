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
    }
}