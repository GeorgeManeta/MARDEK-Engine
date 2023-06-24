using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MARDEK
{
    using UnityEditor;
    using UnityEngine.UI;

    public class SwapComponents
    {
        [MenuItem("Swap/Transform")]
        static void SwapTransformForRectTransform()
        {
            var selectedObjects = Selection.gameObjects;
            foreach(var obj in selectedObjects)
            {
                obj.AddComponent<RectTransform>();
            }
        }

        [MenuItem("Swap/Renderer")]
        static void SwapRenderer()
        {
            var selectedObjects = Selection.gameObjects;
            foreach (var obj in selectedObjects)
            {
                var renderer = obj.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    var prevImage = obj.GetComponent<Image>();
                    if(prevImage)
                        UnityEngine.Object.DestroyImmediate(prevImage);

                    var image = obj.AddComponent<Image>();
                    image.sprite = renderer.sprite;

                    if (renderer.flipX)
                    {
                        var transform = obj.GetComponent<RectTransform>();
                        var scale = transform.localScale;
                        scale.x *= -1;
                        transform.localScale = scale;
                    }
                    UnityEngine.Object.DestroyImmediate(renderer);
                }
            }

        }
    }
}
