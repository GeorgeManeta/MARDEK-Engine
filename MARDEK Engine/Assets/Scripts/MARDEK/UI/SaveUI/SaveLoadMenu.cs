using System.Collections.Generic;
using UnityEngine;
using MARDEK.Core;
using TMPro;

namespace MARDEK.UI
{
    public class SaveLoadMenu : MonoBehaviour
    {
        public static bool IsSaveNotLoad { get; private set; } = false;

        [SerializeField] TextMeshProUGUI MenuTitle;

        [SerializeField] SaveFileBox lastSavedBox;
        [SerializeField] SaveFileBox lastLoadedBox;

        [SerializeField] List<GameObject> allSaveFilePages;
        [SerializeField] GameObject SaveFileBoxPrefab;

        void OnDisable()
        {
            PlayerLocks.UISystemLock -= 1;
            ClearSaveBoxPages();
        }

        void ClearSaveBoxPages()
        {
            foreach (GameObject page in allSaveFilePages)
            {
                while (page.transform.childCount > 0)
                {
                    DestroyImmediate(page.transform.GetChild(0).gameObject);
                }
            }
        }

        public void EnableAsSaveOrLoad(bool IsSaveMenu)
        {
            IsSaveNotLoad = IsSaveMenu;

            if(IsSaveMenu)
                MenuTitle.text = "Save";
            else
                MenuTitle.text = "Load";

            gameObject.SetActive(true);
            PlayerLocks.UISystemLock += 1;
            InstantiateSaveBoxOnPages();
        }

        void InstantiateSaveBoxOnPages()
        {
            for (int pageI = 0; pageI < allSaveFilePages.Count; pageI++)
            {
                for (int i = 0; i < 7; i++)
                {
                    GameObject newSaveBox = Instantiate(SaveFileBoxPrefab, allSaveFilePages[pageI].transform);

                    int saveNumber = pageI * 7 + i + 1;
                    SaveFileBox newSaveBoxScript = newSaveBox.GetComponent<SaveFileBox>();
                    newSaveBoxScript.SetSaveNumber(saveNumber.ToString());
                }
            }
        }
    }
}

