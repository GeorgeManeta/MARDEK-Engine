using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWFShape : MonoBehaviour
{
    public void Create(Sprite sprite)
    {
        var sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;

        int order = 0;
        var upperObject = GetComponentInParent<SWFPlacedObject>(includeInactive: true);
        while (upperObject != null)
        {
            order += upperObject.Depth;
            var parent = upperObject.transform.parent;
            if (parent == null)
                upperObject = null;
            else
                upperObject = parent.GetComponentInParent<SWFPlacedObject>(includeInactive: true);
        }
        sr.sortingOrder = order;
    }
}