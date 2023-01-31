using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MARDEK.Save
{
    public class GeneralProgressData : AddressableMonoBehaviour
    {
        public static GeneralProgressData instance { get; private set; }

        [SerializeField] string currentScene = default;
        [SerializeField] string _gameName = string.Empty;
        [SerializeField] HashSet<string> openedChests = new HashSet<string>();
        
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
        
        public override void Save()
        {
            currentScene = SceneManager.GetActiveScene().path;
            base.Save();
        }

        public void LoadScene()
        {
            if (string.IsNullOrEmpty(currentScene))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(currentScene);
        }

        public bool HasOpenedChest(ChestID chestID)
        {
            return openedChests.Contains(chestID.name);
        }

        public void MarkChestAsOpened(ChestID chestID)
        {
            openedChests.Add(chestID.name);
        }
    }
}
