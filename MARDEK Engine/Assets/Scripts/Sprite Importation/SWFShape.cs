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
    public void CalculateSortingOrder()
    {
        int order = 0;
        var upperObject = GetComponentInParent<SWFPlacedObject>(includeInactive: true);
        while (upperObject != null)
        {
            //print($"{upperObject.name}: {upperObject.Depth}");
            order += upperObject.Depth;
            var parent = upperObject.transform.parent;
            if (parent == null)
                upperObject = null;
            else
                upperObject = parent.GetComponentInParent<SWFPlacedObject>(includeInactive: true);
        }
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = order;
    }
}