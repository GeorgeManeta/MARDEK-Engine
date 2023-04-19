using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using FullSerializer;

using MARDEK.Core;

namespace MARDEK.Save
{
    public class GeneralProgressData : AddressableMonoBehaviour
    {
        /* GeneralProgressData of the current save state */
        public static GeneralProgressData instance { get; set; }

        [SerializeField] public string currentScene = default;
        [SerializeField] string _gameName = string.Empty;
        [SerializeField] public string sceneName { get; private set; } = string.Empty;
        [SerializeField] public DateTime savedTime { get; private set; } = new DateTime();

        /* Ignore sceneInfo when saving/loading since it's specific to each GeneralProgressData and won't be changed */
        [SerializeField] [fsIgnore] public SceneInfo sceneInfo;

        override protected void Awake()
        {
            base.Awake();
            instance = this;
        }
        
        public string GameName
        {
            get
            {
                return _gameName;
            }
            set
            {
                if (string.IsNullOrEmpty(_gameName))
                    _gameName = value;
                return;
            }
        }

        // Automatically link the GeneralProgressData prefab to the sceneInfo prefab in the scene it's in
        // They should almost always accompany each other when used in a scene, unless saving is impossible
        private void OnValidate()
        {
            if (sceneInfo == null)
            {
                GameObject sceneInfoObj = GameObject.Find("SceneInfo");
                if ((sceneInfoObj) != null)
                {
                    sceneInfo = sceneInfoObj.GetComponent<SceneInfo>();
                }
            }
        }

        public override void Save()
        {
            Scene scene = SceneManager.GetActiveScene();

            if (sceneInfo != null)
            {
                sceneName = sceneInfo.displayName;
            }
            else
            {
                Debug.Log("Warning - GeneralProgressData without accompanying sceneInfo in: " + gameObject.scene.name);
                sceneName = "NO SCENE NAME";
            }
            currentScene = scene.path;
            savedTime = DateTime.Now;

            base.Save();
        }

        public void LoadScene()
        {
            if (string.IsNullOrEmpty(currentScene))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(currentScene);
        }
    }
}
