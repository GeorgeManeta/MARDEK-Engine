using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SWFFrame
{
    public List<PlaceObjectJSON> placeObjects = new List<PlaceObjectJSON>();
    public List<RemoveObjectJSON> removeObjects = new List<RemoveObjectJSON>();
    public string label = "";

    [System.Serializable]
    public class PlaceObjectJSON
    {
        public int depth;
        public int id;
        public string skin;

        public float translateX = 0;
        public float translateY = 0;
        public float scaleX = 1;
        public float scaleY = 1;
        public float rotateSkew0 = 0;
        public float rotateSkew1 = 0;

        public int[] rgbaAdd;
        public int[] rgbaMult;

        public float glowBlurXandY = 0;
        public float glowStrengh = 0;
        public int[] glowColorRGBA;
    }
    [System.Serializable]
    public class RemoveObjectJSON
    {
        public int depth;
    }
}
