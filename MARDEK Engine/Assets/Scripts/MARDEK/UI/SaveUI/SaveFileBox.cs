using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MARDEK.Save;
using System.IO;
using TMPro;

namespace MARDEK.UI
{
    public class SaveFileBox : MonoBehaviour
    {
        [SerializeField] GeneralProgressData dummyGeneralProgressData;

        [SerializeField] GameObject saveFileInfo;

        [SerializeField] List<Image> portraitImages;
        [SerializeField] TextMeshProUGUI saveNameLabel;
        [SerializeField] TextMeshProUGUI savedTimeLabel;
        [SerializeField] TextMeshProUGUI sceneNameLabel;
        [SerializeField] TextMeshProUGUI saveNumberLabel;

        bool saveFileExists;
        string saveFileName = string.Empty;
        SaveState saveState;

        public void OnClick()
        {
            if (saveFileName == string.Empty)
            {
                Debug.Log("Warning: clicked save slot without save number/expressionName.");
                return;
            }

            if (SaveLoadMenu.IsSaveNotLoad)
            {
                // Save
                SaveSystem.SaveToFile(saveFileName);
                UpdateFromSaveFile(saveFileName);
            }
            else if (saveFileExists)
            {
                // Load
                SaveSystem.CallGameFileLoaderScene(saveFileName);
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
            saveFileExists = saveExists;
            saveFileInfo.SetActive(saveExists);
        }

        private void UpdateFromSaveFile(string saveFileName)
        {
            CheckFileExists(saveFileName);
            if (saveFileExists)
            {
                saveState = SaveSystem.GetSaveStateFromFile(saveFileName);
                dummyGeneralProgressData.Load(saveState);
                UpdateFromGeneralProgressData(dummyGeneralProgressData);
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
