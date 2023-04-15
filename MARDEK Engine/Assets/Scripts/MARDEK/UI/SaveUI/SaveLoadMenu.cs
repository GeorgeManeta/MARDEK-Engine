using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MARDEK.Core;

namespace MARDEK.UI
{
    public class SaveLoadMenu : MonoBehaviour
    {
        [SerializeField] Text MenuTitle;

        [SerializeField] SaveFileBox lastSavedBox;
        [SerializeField] SaveFileBox lastLoadedBox;

        [SerializeField] List<GameObject> allSaveFilePages;
        [SerializeField] GameObject SaveFileBoxPrefab;

        void OnDisable()
        {
            PlayerLocks.UISystemLock -= 1;

            // Delete save file boxes dynamically
            foreach(GameObject page in allSaveFilePages)
            {
                while (page.transform.childCount > 0)
                {
                    DestroyImmediate(page.transform.GetChild(0).gameObject);
                }
            }
        }

        void CustomEnable(bool IsSaveMenu)
        {
            PlayerLocks.UISystemLock += 1;
            gameObject.SetActive(true);

            // Update SaveWhenClick of last saved/last loaded boxes
            // LastSavedAndLoadedPage takes care of setting info for those boxes
            lastSavedBox.SaveWhenClick = IsSaveMenu;
            lastLoadedBox.SaveWhenClick = IsSaveMenu;

            // Update the pages of save file boxes after the last saved/last loaded page
            for (int pageI = 0; pageI < allSaveFilePages.Count; pageI++)
            {
                // Load save file data - these save file boxes are created/destroyed when menu is opened/closed

                for (int i = 0; i < 7; i++)
                {
                    // make new SaveFileBox prefab as child of allSaveFilePages[pageI]
                    GameObject newSaveBox = Instantiate(SaveFileBoxPrefab, allSaveFilePages[pageI].transform, true);

                    int saveNumber = pageI * 7 + i + 1;
                    SaveFileBox newSaveBoxScript = newSaveBox.GetComponent<SaveFileBox>();
                    newSaveBoxScript.SetSaveNumber(saveNumber.ToString());
                    newSaveBoxScript.SaveWhenClick = IsSaveMenu;
                }

            }
        }

        public void EnableAsSaveMenu()
        {
            MenuTitle.text = "Save";
            CustomEnable(true);
        }
        public void EnableAsLoadMenu()
        {
            MenuTitle.text = "Load";
            CustomEnable(false);
        }
    }
}

