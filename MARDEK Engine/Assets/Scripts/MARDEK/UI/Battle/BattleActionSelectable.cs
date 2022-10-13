using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.UI;

public class BattleActionSelectable : Selectable
{
    public override void Select(bool playSFX = true)
    {
        base.Select(playSFX);
        var rect = GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.rect.width + 400);
        rect.ForceUpdateRectTransforms();
    }
    public override void Deselect()
    {
        base.Deselect();
        var rect = GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.rect.width - 400);
        rect.ForceUpdateRectTransforms();
    }
}
