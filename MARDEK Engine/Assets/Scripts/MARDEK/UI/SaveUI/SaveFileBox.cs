using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MARDEK.Save;

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
                SaveSystem.CurrentSaveStateName = saveFileName;
                sceneTransitionCommand.Trigger();
            }
        }

        public void SetSaveNumber(string saveNumber)
        {
            saveNumberLabel.text = saveNumber;
            saveFileName = "MARDEK_save_" + saveNumber;
            UpdateFromSaveFile(saveFileName);
        }

        public void SetSaveExists(bool saveExists)
        {
            this.saveExists = saveExists;
            saveFileInfo.SetActive(saveExists);
        }

        private void UpdateFromSaveFile(string saveFileName)
        {
            SaveState save = SaveSystem.GetSaveFromFile(saveFileName);
            SaveSystem.SaveObjectToCurrentSave(GeneralProgressData.currentGeneralProgressData);

            SetSaveExists(save != null);
            if (saveExists)
            {
                saveFileInfo.SetActive(true);
                this.saveExists = true;

                /* You must use GeneralProgressData.currentGeneralProgressData
                because the save system uses an object's GUID to fetch its data,
                meaning all the saved GeneralProgressData info
                is tied to the GeneralProgressData.currentGeneralProgressData instance
                and you need to use that specific instance with LoadObjectFromGivenSave.
                */
                GeneralProgressData gpd = GeneralProgressData.currentGeneralProgressData;
                SaveSystem.LoadObjectFromGivenSave(gpd, save);
                UpdateFromGeneralProgressData(gpd);
                SaveSystem.LoadObjectFromCurrentSave(gpd);
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
