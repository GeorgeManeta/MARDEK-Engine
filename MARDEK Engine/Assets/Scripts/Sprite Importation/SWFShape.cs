using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWFShape : MonoBehaviour
{
    public void Create(Sprite sprite)
    {
        gameObject.name = sprite.name;

        var sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
    }
    [ContextMenu("CalculateSortingOrder")]
    public void CalculateSortingOrderAndColor()
    {
        int order = 0;
        var upperObject = GetComponentInParent<SWFPlacedObject>(includeInactive: true);
        Color color = Color.white;
        while (upperObject != null)
        {
            color = upperObject.GetColor();
            order += upperObject.Depth;
            var parent = upperObject.transform.parent;
            if (parent == null)
                upperObject = null;
            else
                upperObject = parent.GetComponentInParent<SWFPlacedObject>(includeInactive: true);
        }
        var r = gameObject.GetComponent<SpriteRenderer>();
        r.sortingOrder = order;
        r.color = color;
    }
}