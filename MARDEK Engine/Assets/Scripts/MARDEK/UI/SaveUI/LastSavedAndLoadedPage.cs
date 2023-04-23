using UnityEngine;

namespace MARDEK.UI
{
    public class LastSavedAndLoadedPage : MonoBehaviour
    {
        [SerializeField] SaveFileBox lastSavedBox;
        [SerializeField] SaveFileBox lastLoadedBox;

        void OnEnable()
        {
            string lastSavedFile = PlayerPrefs.GetString("lastSavedFile", string.Empty);
            SetSaveNumberOfSaveBoxBySaveFileString(lastSavedBox, lastSavedFile);

            string lastLoadedFile = PlayerPrefs.GetString("lastLoadedFile", string.Empty);
            SetSaveNumberOfSaveBoxBySaveFileString(lastLoadedBox, lastLoadedFile);
        }

        void SetSaveNumberOfSaveBoxBySaveFileString(SaveFileBox saveBox, string saveFile)
        {
            if (string.IsNullOrEmpty(saveFile))
                saveBox.SetSaveExists(false);
            else
            {
                string saveFileIndex = saveFile.Substring(saveFile.LastIndexOf("_") + 1);
                saveBox.SetSaveNumber(saveFileIndex);
            }
        }

    }
}

