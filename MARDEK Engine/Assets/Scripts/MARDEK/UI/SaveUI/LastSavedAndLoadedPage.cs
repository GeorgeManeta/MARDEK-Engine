using UnityEngine;

namespace MARDEK.UI
{
    /*
     * Unlike the other save file boxes, which are created/destroyed when the save/load menu opens/closes,
     * the save file boxes on the first page of the save/load menu always exist
     * but update their values every time the first page is set active.
     * 
     * This is so if the player saves to a save file and goes back to this page, the last saved/last loaded
     * boxes will be updated.
     */
    public class LastSavedAndLoadedPage : MonoBehaviour
    {
        [SerializeField] SaveFileBox lastSavedBox;
        [SerializeField] SaveFileBox lastLoadedBox;

        void OnEnable()
        {
            string lastSavedFile = PlayerPrefs.GetString("lastSavedFile", string.Empty);
            if (lastSavedFile.Length > 0)
            {
                // if lastSavedFile is "MARDEK_save_15", lastLoadedNumber is "15"
                string lastSavedNumber = lastSavedFile.Substring(
                    lastSavedFile.LastIndexOf("_") + 1);

                lastSavedBox.SetSaveNumber(lastSavedNumber);
            }
            else
            {
                lastSavedBox.SetSaveExists(false);
            }



            string lastLoadedFile = PlayerPrefs.GetString("lastLoadedFile", string.Empty);
            if (lastLoadedFile.Length > 0)
            {
                // if lastLoadedFile is "MARDEK_save_15", lastLoadedNumber is "15"
                string lastLoadedNumber = lastLoadedFile.Substring(
                    lastLoadedFile.LastIndexOf("_") + 1);

                lastLoadedBox.SetSaveNumber(lastLoadedNumber);
            }
            else
            {
                lastLoadedBox.SetSaveExists(false);
            }
        }
    }
}

