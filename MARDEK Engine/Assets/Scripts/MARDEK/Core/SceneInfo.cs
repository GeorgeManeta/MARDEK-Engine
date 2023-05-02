using UnityEngine;

namespace MARDEK.Core
{
    public class SceneInfo : MonoBehaviour
    {
        static SceneInfo instance;
        [field: SerializeField] string displayName { get; set; }

        public static string CurrentSceneInfoDisplayName {
            get
            {
                if (instance)
                    return instance.displayName;
                return "Undefined Scene Name";
            }
        }
        [field: SerializeField] string id { get; set; }

        public static string CurrentSceneInfoID
        {
            get
            {
                if (instance)
                    return instance.id;
                return "Undefined Scene ID";
            }
        }

        private void Awake()
        {
            instance = this;
        }

        public void OnValidate()
        {
            SceneInfo checkDuplicate = SceneInfo.FindObjectOfType<SceneInfo>();
            if (checkDuplicate != this)
            {
                throw new System.ApplicationException("Duplicated SceneInfo: " + this.id + " vs " + checkDuplicate.id);
            }
        }
    }
}
