using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkewMatrixDecompositor;
using log4net.Util;

public class SWFPlacedObject : MonoBehaviour
{
    [SerializeField, HideInInspector] GameObject innerTransform;
    [SerializeField] List<GameObject> innerSprites = new();
    [field: SerializeField] public int Depth { get; private set; }
    [SerializeField] Color32 color = Color.white;
    public void Create(int depth)
    {
        Depth = depth;
        for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);
        innerTransform = new GameObject("Inner Transform");
        innerTransform.transform.parent = transform;
        innerSprites = new();
    }
    public void SetMatrix(float scaleX, float rotateSkew0, float rotateSkew1, float scaleY, float translateX, float translateY)
    {
        var matrix = new System.Numerics.Matrix3x2(scaleX, rotateSkew0, rotateSkew1, scaleY, translateX, translateY);
        var result = MatrixDecompositor.Decompose(matrix);

        transform.localScale = Vector3.one;
        if (result.Scale2 != null)
            transform.localScale = result.Scale2.Value;
        
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        if (result.Rotate2 != null)
            transform.localRotation = Quaternion.Euler(0f, 0f, -result.Rotate2.Value * Mathf.Rad2Deg);

        transform.localPosition = Vector3.zero;
        if (result.Translate != null)
            transform.localPosition = result.Translate.Value;

        innerTransform.transform.localScale = Vector3.one;
        if (result.Scale1 != null)
            innerTransform.transform.localScale = result.Scale1.Value;

        innerTransform.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        if (result.Rotate1 != null)
            innerTransform.transform.localRotation = Quaternion.Euler(0f, 0f, -result.Rotate1.Value * Mathf.Rad2Deg);
    }
    public void SetObjectByID(int id)
    {
        if (id == 0)
            return;

        if (id == -1)
        {
            foreach (var inner in innerSprites)
                inner.gameObject.SetActive(false);
            return;
        }

        var obj = GetObjectByID(id);
        if (obj == null)
            obj = CreateObjectByID(id);
        foreach (var inner in innerSprites)
            inner.gameObject.SetActive(inner == obj);
    }
    GameObject GetObjectByID(int id)
    {
        foreach (var obj in innerSprites)
            if (string.Equals(obj.name, id.ToString()))
                return obj;
        return null;
    }
    GameObject CreateObjectByID(int id)
    {
        var obj = SWFSpriteImporter.CreateOrInstantiateByID(id, innerTransform.transform);
        innerSprites.Add(obj);
        return obj;
    }

    public void SetColor(int[] add, int[] mult)
    {
        color = new Color32(
            (byte)(mult[0] + add[0]),
            (byte)(mult[1] + add[1]), 
            (byte)(mult[2] + add[2]), 
            (byte)(mult[3] + add[3] - 1));
    }

    public Color GetColor()
    {
        return color;
    }
}