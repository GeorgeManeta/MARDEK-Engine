using System;
using UnityEngine;
using UnityEngine.SceneManagement;

using MARDEK.Core;

namespace MARDEK.Save
{
    public class GeneralProgressData : AddressableMonoBehaviour
    {
        public static GeneralProgressData currentGeneralProgressData { get; set; }

        [SerializeField] public string currentScene = default;
        [SerializeField] string _gameName = string.Empty;

        [SerializeField] public string sceneName { get; private set; } = string.Empty;
        [SerializeField] public DateTime savedTime { get; private set; } = new DateTime();

        override protected void Awake()
        {
            base.Awake();
            currentGeneralProgressData = this;
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
        
        public override void Save()
        {
            Scene scene = SceneManager.GetActiveScene();

            currentScene = scene.path;

            GameObject sceneInfo = GameObject.Find("SceneInfo");
            if (sceneInfo != null)
            {
                sceneName = sceneInfo.GetComponent<SceneInfo>().displayName;
            }
            else
            {
                Debug.Log("Warning - saving GeneralProgressData from scene with no sceneInfo");
                sceneName = "NO SCENE NAME";
            }

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
