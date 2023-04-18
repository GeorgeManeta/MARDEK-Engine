using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
using FullSerializer;
using MARDEK.Core;

#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

namespace MARDEK.Save
{
    public class SaveSystem : MonoBehaviour
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void SyncDB();
#endif

        public static string persistentPath
        {
            get
            {
                string path;

#if UNITY_WEBGL
                path = System.IO.Path.Combine("/idbfs", Application.productName);
#else
                path = Application.persistentDataPath;
#endif

                if (System.IO.Directory.Exists(path) == false)
                    System.IO.Directory.CreateDirectory(path);
                return path;
            }
        }
        static bool formatSaveFiles = true;
        const string formatterDataFieldName = "\"jsonData\": ";
        static fsSerializer serializer = new fsSerializer();
        public delegate void SaveCallback();
        public static event SaveCallback OnBeforeSave = delegate { };

        static SaveState currentSaveState;
        public static string currentSaveFile;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialization()
        {
            currentSaveState = new SaveState();
        }

        public static void SaveObjectToCurrentSave(IAddressableGuid addressable)
        {
            if (Application.isPlaying == false)
                throw new Exception("Don't Save while outside playmode");
            currentSaveState.SaveObject(addressable, serializer);

        }
        public static bool LoadObjectFromCurrentSave(IAddressableGuid addressable)
        {
            if (Application.isPlaying == false)
                throw new Exception("Don't Load while outside playmode");
            return currentSaveState.LoadObject(addressable, serializer);
        }

        public static bool LoadObjectFromGivenSave(IAddressableGuid addressable, SaveState saveState) {
            return saveState.LoadObject(addressable, serializer);
        }

        public static void SaveToFile(string fileName)
        {
            /* you don't need to manually save any AddressableMonoBehavior here;
            if they have AutoSave on they will be saved to currentSaveState automatically
            before it saves to the file */

            PlayerPrefs.SetString("lastSavedFile", fileName);

            OnBeforeSave.Invoke();
            serializer.TrySerialize(currentSaveState.addressableState, out fsData data);
            string json = fsJsonPrinter.PrettyJson(data);
            if (formatSaveFiles)
                json = FormatSaveFile(json, true);
            string filePath = System.IO.Path.Combine(persistentPath, $"{fileName}.json");
            System.IO.File.WriteAllText(filePath, json);

#if UNITY_WEBGL && !UNITY_EDITOR
            //flush our changes to IndexedDB
            SyncDB();
#endif

            Debug.Log($"Game file saved to {filePath}");
        }

        /*
         * Loads save file at into currentSaveState.
         * Called by SaveFileBox.
         * Afterwards, the active scene becomes GameFileLoader which takes care of the rest of the loading.
         */
        public static void LoadIntoCurrentSave()
        {
            PlayerPrefs.SetString("lastLoadedFile", currentSaveFile);

            currentSaveState = GetSaveFromFile(currentSaveFile);
            // LoadObjectFromGivenSave(GeneralProgressData.instance, currentSaveState);
        }

        public static SaveState? GetSaveFromFile(string fileName)
        {
            SaveState returnedSaveState = new SaveState();
            string filePath = System.IO.Path.Combine(persistentPath, $"{fileName}.json");
            string json = System.IO.File.ReadAllText(filePath);
            if (formatSaveFiles)
                json = FormatSaveFile(json, false);
            fsJsonParser.Parse(json, out fsData data);
            serializer.TryDeserialize(data, ref returnedSaveState.addressableState);
            Debug.Log($"GetSaveData: Game file loaded from {filePath}");
            return returnedSaveState;
        }

        static string FormatSaveFile(string content, bool isSaving)
        {
            string result = default;
            while (true)
            {
                var Separatorindex = content.IndexOf(formatterDataFieldName);
                if (Separatorindex == -1)
                {
                    result += content; // append the rest of the string
                    break;
                }

                string beforeSeparator = content.Substring(0, Separatorindex + formatterDataFieldName.Length);
                result += beforeSeparator;

                // get json object by outmost pair of balanced curly braces
                if (isSaving) Separatorindex++; // skip first '\"'
                string AfterSeparator = content.Substring(Separatorindex + formatterDataFieldName.Length);
                ParseCurlyBraces(AfterSeparator, out int startIndex, out int endIndex);
                string json = AfterSeparator.Substring(startIndex, endIndex - startIndex + 1);
                if (isSaving)
                    result += Regex.Unescape(json); // remove escape characters
                else
                    result += "\"" + json.Replace("\"", "\\\"") + "\""; // undo Regex.Unescape()

                // update content for next iteration
                if (isSaving) endIndex++; // skip last '\"'
                content = AfterSeparator.Substring(endIndex+1); 
            }
            return result;
        }
        static void ParseCurlyBraces(string content, out int startIndex, out int endIndex)
        {
            startIndex = 0;
            endIndex = content.Length - 1;
            bool isInsideQuotes = false;
            int curlyBracesDepth = 0;
            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] == '\"')
                {
                    isInsideQuotes = !isInsideQuotes;
                    continue;
                }
                if (isInsideQuotes)
                    continue;

                if (content[i] == '{')
                {
                    if (curlyBracesDepth == 0)
                        startIndex = i;
                    curlyBracesDepth++;
                }
                else if (content[i] == '}')
                {
                    curlyBracesDepth--;
                    if (curlyBracesDepth == 0)
                    {
                        endIndex = i;
                        break;
                    }
                }
            }
        }
    }
}