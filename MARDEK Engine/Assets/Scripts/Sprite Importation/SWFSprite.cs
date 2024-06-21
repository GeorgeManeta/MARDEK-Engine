using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FullSerializer;

public class SWFSprite : MonoBehaviour
{
    public string id = "";
    [SerializeField] List<SWFFrame> frames = new List<SWFFrame>();
    [SerializeField] List<SWFPlacedObject> placedObjects = new List<SWFPlacedObject>();
    [SerializeField] List<LabelAnimationClipPair> animations = new List<LabelAnimationClipPair>();
    public void Create(TextAsset json)
    {
        id = json.name;
        gameObject.name = id;

        fsJsonParser.Parse(json.text, out fsData data);
        fsSerializer serializer = new fsSerializer();
        serializer.TryDeserialize(data, ref frames);

        for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);
        placedObjects = new List<SWFPlacedObject>();

        foreach (var frame in frames)
        {
            foreach (var obj in frame.placeObjects)
            {
                if (GetObjectByDepth(obj.depth) == null)
                {
                    var newGameObject = new GameObject($"{obj.depth}");
                    newGameObject.transform.parent = transform;
                    var newPlaceObject = newGameObject.AddComponent<SWFPlacedObject>();
                    newPlaceObject.Create(obj.depth);
                    placedObjects.Add(newPlaceObject);
                }
            }
        }
        SetFrame(frames.Count);
        SetFrame(0);

        ImportAnimations();
    }

    [ContextMenu("Import Animations")]
    void ImportAnimations()
    {
        var animator = GetComponent<Animation>();
        if (animator)
            DestroyImmediate(animator);
        animator = gameObject.AddComponent<Animation>();
        animations = new List<LabelAnimationClipPair>();

        var localPositionX = new Dictionary<string, AnimationCurve>();
        var localPositionY = new Dictionary<string, AnimationCurve>();

        var rotationX = new Dictionary<string, AnimationCurve>();
        var rotationY = new Dictionary<string, AnimationCurve>();
        var rotationZ = new Dictionary<string, AnimationCurve>();
        var rotationW = new Dictionary<string, AnimationCurve>();

        var localScaleX = new Dictionary<string, AnimationCurve>();
        var localScaleY = new Dictionary<string, AnimationCurve>();

        var isActive = new Dictionary<string, AnimationCurve>();

        bool initializeCurves = true;
        string currentLabel = "";
        int animStartFrame = 0;
        float frameRate = 30f;

        for(int i = 0; i < frames.Count; i++)
        {
            bool skipFrame = frames[i].label == ""
                && frames[i].placeObjects.Count == 0 
                && frames[i].removeObjects.Count == 0;
            if (skipFrame)
                continue;

            SetFrame(i);

            if (frames[i].label != "")
            {
                if (currentLabel != "")
                {
                    SaveAsAsset();
                }
                currentLabel = frames[i].label;
                animStartFrame = i;
                initializeCurves = true;
                localPositionX.Clear();
                localPositionY.Clear();
                rotationX.Clear();
                rotationY.Clear();
                rotationZ.Clear();
                rotationW.Clear();
                localScaleX.Clear();
                localScaleY.Clear();
                isActive.Clear();
            }
            
            foreach (var po in placedObjects)
            {
                var path = po.name;
                float timeKey = (i - animStartFrame) / frameRate;

                if (initializeCurves)
                {
                    localPositionX.Add(path, new AnimationCurve());
                    localPositionY.Add(path, new AnimationCurve());

                    rotationX.Add(path, new AnimationCurve());
                    rotationY.Add(path, new AnimationCurve());
                    rotationZ.Add(path, new AnimationCurve());
                    rotationW.Add(path, new AnimationCurve());

                    localScaleX.Add(path, new AnimationCurve());
                    localScaleY.Add(path, new AnimationCurve());
                }
                localPositionX[path].AddKey(KeyFrame(timeKey, po.transform.localPosition.x));
                localPositionY[path].AddKey(KeyFrame(timeKey, po.transform.localPosition.y));

                rotationX[path].AddKey(KeyFrame(timeKey, po.transform.localRotation.x));
                rotationY[path].AddKey(KeyFrame(timeKey, po.transform.localRotation.y));
                rotationZ[path].AddKey(KeyFrame(timeKey, po.transform.localRotation.z));
                rotationW[path].AddKey(KeyFrame(timeKey, po.transform.localRotation.w));

                localScaleX[path].AddKey(KeyFrame(timeKey, po.transform.localScale.x));
                localScaleY[path].AddKey(KeyFrame(timeKey, po.transform.localScale.y));

                var innerTransform = po.transform.GetChild(0);
                path = $"{path}/{innerTransform.name}";
                if (initializeCurves)
                {
                    rotationX.Add(path, new AnimationCurve());
                    rotationY.Add(path, new AnimationCurve());
                    rotationZ.Add(path, new AnimationCurve());
                    rotationW.Add(path, new AnimationCurve());

                    localScaleX.Add(path, new AnimationCurve());
                    localScaleY.Add(path, new AnimationCurve());
                }
                rotationX[path].AddKey(KeyFrame(timeKey, innerTransform.transform.localRotation.x));
                rotationY[path].AddKey(KeyFrame(timeKey, innerTransform.transform.localRotation.y));
                rotationZ[path].AddKey(KeyFrame(timeKey, innerTransform.transform.localRotation.z));
                rotationW[path].AddKey(KeyFrame(timeKey, innerTransform.transform.localRotation.w));

                localScaleX[path].AddKey(KeyFrame(timeKey, innerTransform.transform.localScale.x));
                localScaleY[path].AddKey(KeyFrame(timeKey, innerTransform.transform.localScale.y));

                for (int c = 0; c < innerTransform.childCount; c++)
                {
                    var obj = innerTransform.GetChild(c);
                    var innerPath = $"{path}/{obj.name}";
                    if (initializeCurves)
                    {
                        isActive.Add(innerPath, new AnimationCurve());
                    }
                    isActive[innerPath].AddKey(KeyFrame(timeKey, obj.gameObject.activeSelf ? 1f : 0f));
                }
            }
            initializeCurves = false;
        }
        SaveAsAsset();
        SetFrame(0);

        Keyframe KeyFrame(float time, float value)
        {
            var keyframe = new Keyframe(time, value);
            return keyframe;
        }

        void SaveAsAsset()
        {
            var clip = new AnimationClip();
            clip.frameRate = frameRate;
            clip.legacy = true;

            foreach (var curve in localPositionX)
                clip.SetCurve(curve.Key, typeof(Transform), "localPosition.x", curve.Value);
            foreach (var curve in localPositionY)
                clip.SetCurve(curve.Key, typeof(Transform), "localPosition.y", curve.Value);

            foreach (var curve in rotationX)
                clip.SetCurve(curve.Key, typeof(Transform), "localRotation.x", curve.Value);
            foreach (var curve in rotationY)
                clip.SetCurve(curve.Key, typeof(Transform), "localRotation.y", curve.Value);
            foreach (var curve in rotationZ)
                clip.SetCurve(curve.Key, typeof(Transform), "localRotation.z", curve.Value);
            foreach (var curve in rotationW)
                clip.SetCurve(curve.Key, typeof(Transform), "localRotation.w", curve.Value);

            foreach (var curve in localScaleX)
                clip.SetCurve(curve.Key, typeof(Transform), "localScale.x", curve.Value);
            foreach (var curve in localScaleY)
                clip.SetCurve(curve.Key, typeof(Transform), "localScale.y", curve.Value);

            foreach (var curve in isActive)
                clip.SetCurve(curve.Key, typeof(GameObject), "m_IsActive", curve.Value);

            clip.EnsureQuaternionContinuity();

            var dir = $"Assets/Animations/Battle Model Animations/{id}";
            System.IO.Directory.CreateDirectory(dir);
            var path = $"{dir}/{id}_{currentLabel}.anim";
            print($"Creating animation: {path}");
            AssetDatabase.DeleteAsset(path);
            AssetDatabase.CreateAsset(clip, path);

            animator.AddClip(clip, clip.name);
            if (animator.clip == null)
                animator.clip = clip;
            animations.Add(new LabelAnimationClipPair() { label = currentLabel, clip = clip });
        }
    }
    SWFPlacedObject GetObjectByDepth(int depth)
    {
        foreach (var obj in placedObjects)
            if (obj.Depth == depth)
                return obj;
        return null;
    }
    void SetFrame(int frameNumber)
    {
        if (frames.Count == 0)
            return;
        foreach (var obj in placedObjects)
            obj.SetObjectByID(-1);

        for (int f = 0; f <= frameNumber; f++)
        {
            if (frames.Count <= f)
                continue;

            foreach (var obj in frames[f].removeObjects)
            {
                GetObjectByDepth(obj.depth).SetObjectByID(-1);
            }
            foreach (var obj in frames[f].placeObjects)
            {
                var placedObject = GetObjectByDepth(obj.depth);
                placedObject.SetObjectByID(obj.id);
                placedObject.SetMatrix(obj.scaleX, obj.rotateSkew0, obj.rotateSkew1, obj.scaleY, obj.translateX, -obj.translateY);
                
                // Set color of objects based on the first frame
                if (f == 0 && obj.rgbaAdd?.Length > 0)
                    placedObject.SetColor(obj.rgbaAdd, obj.rgbaMult);
            }
        }
    }

    [System.Serializable]
    struct LabelAnimationClipPair
    {
        public string label;
        public AnimationClip clip;
    }

    public void PlayAnimationByLabel(string label)
    {
        AnimationClip clip = null;
        foreach(var pair in animations)
        {
            if (pair.label == label)
            {
                clip = pair.clip;
            }
        }
        if (clip != null)
        {
            var animator = GetComponent<Animation>();
            animator.Play(clip.name);
#if UNITY_EDITOR
            animator.clip = clip;
#endif
        }
    }
}