using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MARDEK.Save;
using System.IO;

namespace MARDEK.UI
{
    public class SaveFileBox : MonoBehaviour
    {
        // For loading GameLoaderFile scene
        [SerializeField] Movement.SceneTransitionCommand sceneTransitionCommand;

        [SerializeField] GameObject saveFileInfo;

        [SerializeField] List<Image> portraitImages;
        [SerializeField] Text saveNameLabel;
        [SerializeField] Text savedTimeLabel;
        [SerializeField] Text sceneNameLabel;
        [SerializeField] Text saveNumberLabel;

        bool saveExists;
        string saveFileName = string.Empty;

        public bool SaveWhenClick { get; set; }

        public void OnClick()
        {
            if (saveFileName == string.Empty)
            {
                Debug.Log("Warning: clicked save slot without save number/name.");
                return;
            }

            if (SaveWhenClick)
            {
                // save to this save slot
                SaveSystem.SaveToFile(saveFileName);
                UpdateFromSaveFile(saveFileName);
            }
            else if (saveExists)
            {
                // load from this save slot
                // SaveSystem.LoadIntoCurrentSave(saveFileName);
                SaveSystem.currentSaveFile = saveFileName;

                // trigger transition to GameFileLoader which handles the rest of the loading
                sceneTransitionCommand.Trigger();
            }
        }

        public void SetSaveNumber(string saveNumber)
        {
            saveNumberLabel.text = saveNumber;
            saveFileName = "MARDEK_save_" + saveNumber;
            UpdateFromSaveFile(saveFileName);
        }

        public void CheckFileExists(string fileName)
        {
            string filePath = System.IO.Path.Combine(SaveSystem.persistentPath, $"{fileName}.json");
            SetSaveExists(File.Exists(filePath));
        }

        public void SetSaveExists(bool saveExists)
        {
            this.saveExists = saveExists;
            saveFileInfo.SetActive(saveExists);
        }

        private void UpdateFromSaveFile(string saveFileName)
        {
            Debug.Log("Updating from save file " + saveFileName + " " + GeneralProgressData.instance.sceneName + " " + (GeneralProgressData.instance.sceneInfo == null ? "null sceneInfo" : GeneralProgressData.instance.sceneInfo.displayName));

            CheckFileExists(saveFileName);
            if (saveExists)
            {
                // Save the actual GeneralProgressData of the current save so it doesn't get overwritten by searching through all save files
                SaveSystem.SaveObjectToCurrentSave(GeneralProgressData.instance);

                /* You must use GeneralProgressData.instance
                because the save system uses an object's GUID to fetch its data,
                meaning all the saved GeneralProgressData info
                is tied to the GeneralProgressData.instance
                and you need to use that specific instance with LoadObjectFromGivenSave.
                */
                SaveState save = SaveSystem.GetSaveFromFile(saveFileName);
                GeneralProgressData gpd = GeneralProgressData.instance;
                SaveSystem.LoadObjectFromGivenSave(gpd, save);
                UpdateFromGeneralProgressData(gpd);

                // Load the actual GeneralProgressData back
                SaveSystem.LoadObjectFromCurrentSave(GeneralProgressData.instance);

                Debug.Log("Finished updating from save file " + saveFileName + " " + GeneralProgressData.instance.sceneName + " " + (GeneralProgressData.instance.sceneInfo == null ? "null sceneInfo" : GeneralProgressData.instance.sceneInfo.displayName));
            }
        }

        private void UpdateFromGeneralProgressData(GeneralProgressData gpd)
        {
            if (gpd == null)
            {
                Debug.Log("Warning: Attempting to update SaveFileBox from null GeneralProgressData");
                return;
            }

            saveNameLabel.text = gpd.GameName;
            savedTimeLabel.text = gpd.savedTime.ToString("ddd dd/MMM/yyyy - HH:mmtt", System.Globalization.CultureInfo.InvariantCulture);
            sceneNameLabel.text = gpd.sceneName;
        }
    }
}
