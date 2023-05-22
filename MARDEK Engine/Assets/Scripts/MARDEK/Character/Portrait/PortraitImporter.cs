using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

using System;
using Newtonsoft.Json;
using System.Linq;

namespace MARDEK.CharacterSystem
{
    // To make this work:
    // 1. Comment the denoted line (currently line 10) in AddressableScriptableObject.cs
    // 2. Uncomment the setter for listed attributes and setters in Item.cs
    // 3. Attach to empty gameObject in a scene and run the scene.
    public class PortraitImporter : MonoBehaviour
    {
        // Should contain all possible portrait types for access
        public PortraitType[] categories;

        // The JSON should be formatted as follows:
        // The JSON file must start with [  and end with ], with an array of PortraitJSON info between them.
        public TextAsset jsonFile;

        void Start()
        {
            DeserializeItems();
        }

        void DeserializeItems()
        {
            Debug.Log("START DESERIALIZING");

            // Store the portrait types in a dictionary for quick access later.
            Dictionary<string, PortraitType> categoryDict = new();
            foreach (PortraitType cat in categories)
            {
                categoryDict[cat.name] = cat;
            }

            /*
            // NOTE: I ditched fsSerializer and installed Newtonsoft.Json
            // because I kept getting null reference exceptions with fsSerializer

            ImportPortraits list = new ImportPortraits();
            fsSerializer serializer = new();
            fsJsonParser.Parse(jsonFile.text, out fsData data);
            serializer.TryDeserialize(data, ref list);
            */

            List<PortraitJSON> list = JsonConvert.DeserializeObject<List<PortraitJSON>>(jsonFile.text);

            foreach (PortraitJSON p in list)
            {
                PortraitType portraitType = null;
                switch (p.t)
                {
                    case "huM":
                        portraitType = categoryDict["HumanM"];
                        break;

                    case "huF":
                        portraitType = categoryDict["HumanF"];
                        break;

                    case "Anu":
                        portraitType = categoryDict["Annunaki"];
                        break;

                    case "Rep":
                        portraitType = categoryDict["Reptoid"];
                        break;

                    case "child":
                        portraitType = categoryDict["HumanChild"];
                        break;

                    case "wlf":
                        // portraitType = categoryDict["Aruan"];
                        break;

                    case "robot":
                        // portraitType = categoryDict["Robot"];
                        break;

                    default:
                        Debug.Log(p.displayName  + " has invalid portraitType");
                        break;
                }

                if (portraitType != null)
                {
                    // the face name of annunaki comes from their robe name
                    string face_name = pickFirstNotNull(p.face, p.robe);
                    Sprite face = SearchPortraitSprite(p.t + "_face_" + face_name, portraitType.name);

                    Sprite eye, neck;
                    string eye_name = pickFirstNotNull(p.eyes, p.eye);
                    string neck_name = pickFirstNotNull(p.neck, p.face);
                    if (portraitType.name.Contains("Human"))
                    {
                        eye = SearchPortraitSprite("human_eye_" + eye_name, "Human");
                        neck = SearchPortraitSprite("human_neck_" + neck_name, "Human");

                        // default to basic white neck
                        if (neck == null)
                        {
                            neck = SearchPortraitSprite("human_neck_1", "Human");
                        }
                    }
                    else
                    {
                        eye = SearchPortraitSprite(p.t + "_eye_" + eye_name, portraitType.name);
                        neck = SearchPortraitSprite(p.t + "_neck_" + neck_name, portraitType.name);

                        if (neck == null && portraitType.name == "Reptoid")
                        {
                            // default to first reptoid neck
                            neck = SearchPortraitSprite("Rep_neck_1", "Reptoid");
                        }
                    }

                    Sprite brow = null;
                    if (portraitType.name != "Reptoid")
                    {
                        string brow_name = pickFirstNotNull(p.eyebrows, p.eyes, p.eye, p.hair, p.face);

                        if (portraitType.name != "HumanChild")
                        {
                            brow = SearchPortraitSprite(p.t + "_brow_" + brow_name, portraitType.name);
                        }
                        else
                        {
                            brow = SearchPortraitSprite("huM" + "_brow_" + brow_name, "HumanM");
                        }
                    }

                    Sprite hair;
                    string hair_name = pickFirstNotNull(p.hair, p.face);
                    hair = SearchPortraitSprite(p.t + "_hair_" + hair_name, portraitType.name);

                    Sprite torso;
                    string torso_name = pickFirstNotNull(p.armour, p.robe);
                    if (portraitType.name != "HumanChild")
                    {
                        torso = SearchPortraitSprite(p.t + "_torso_" + torso_name, portraitType.name);
                    }
                    else
                    {
                        torso = SearchPortraitSprite("huM" + "_torso_" + torso_name, "HumanM");
                    }

                    // Note: Human mouths are either zombie or none, and that will be handled separately;
                    // however, this is used to set the jaw of non-human races (reptoids/aruans)
                    Sprite mouth;
                    string mouth_name = p.face;
                    mouth = SearchPortraitSprite(p.t + "_mouth_" + mouth_name, portraitType.name);
                    if (mouth == null && portraitType.name == "Reptoid")
                    {
                        // default to first reptoid mouth
                        mouth = SearchPortraitSprite("Rep_mouth_1", "Reptoid");
                    }

                    // these can be added later
                    /*
                    Sprite voice = null;
                    eye = SearchPortraitSprite(p.t + "_eye_" + p.face, portraitType.name);

                    Sprite elembg = null;
                    eye = SearchPortraitSprite(p.t + "_eye_" + p.face, portraitType.name);
                    */

                    int facemask;
                    if (p.facemask == null || ! Int32.TryParse(p.facemask, out facemask))
                    {
                        facemask = -1;
                    }

                    // check no sprites are null when they shouldn't be
                    Sprite[] check = new Sprite[] { face, hair, eye, neck, torso, mouth };
                    for (int i = 0; i < check.Length; i++)
                    {
                        if (check[i] == null)
                        {
                            // reptoids don't have hair, annuaki don't have mouth/hair/neck,
                            // humans don't have mouths (see above)
                            if (! ((portraitType.name == "Reptoid" && i == 1)
                                    || (portraitType.name == "Annunaki" && (i == 1 || i == 5 || i == 3))
                                    || (portraitType.name.Contains("Human") && i == 5)
                                ))
                            {
                                Debug.Log(p.displayName + " has null " + i);
                            }
                        }
                    }

                    CharacterPortrait temp = new CharacterPortrait(
                        portraitType,
                        face, hair, eye, brow, neck, torso, mouth, null,
                        p.ethnicity, facemask
                        );

                    string realDisplayName = p.displayName.Substring(2);
                    AssetDatabase.CreateAsset(
                        temp,
                        "Assets/ScriptableObjects/Characters/Portraits/Imported/" + realDisplayName + ".asset"
                        );
                }
            }
        }

        private string pickFirstNotNull(params string[] strings)
        {
            foreach (string s in strings)
            {
                if (s != null)
                {
                    return s;
                }
            }
            return null;
        }

        private Sprite SearchPortraitSprite(string spriteName, string spriteFolder)
        {
            string[] guids = AssetDatabase.FindAssets(
                "t:Sprite " + spriteName,
                new string[] { "Assets/Sprites/Characters/Portraits/" + spriteFolder }
            );
            if (guids.Length > 1)
            {
                // take matching asset with shortest name
                // since AssetDatabase.FindAssets gets all assets that contain the search string

                string[] match_names = guids.Select(
                    id => AssetDatabase.GUIDToAssetPath(id)).ToArray();
                int minLength = match_names.Min(n => n.Length);
                string final_name = match_names.FirstOrDefault(x => x.Length == minLength);

                return AssetDatabase.LoadAssetAtPath<Sprite>(final_name);
            }
            else if (guids.Length == 0)
            {
                return null;
            }
            else
            {
                return AssetDatabase.LoadAssetAtPath<Sprite>(
                    AssetDatabase.GUIDToAssetPath(guids[0])
                );
            }
        }

        // Type that represents how the data is formatted in portraits.json
        // Used to create the actual CharacterPortrait ScriptableObject.
        private class PortraitJSON
        {
            [SerializeField] public string displayName;
            [SerializeField] public string t;

            [SerializeField] public string face;
            [SerializeField] public string eyes;
            [SerializeField] public string hair;
            [SerializeField] public string neck;
            [SerializeField] public string armour;
            [SerializeField] public string eyebrows;
            [SerializeField] public string mouth;
            [SerializeField] public string ethnicity;
            [SerializeField] public string facemask; 

            [SerializeField] public string eye;
            [SerializeField] public string robe;
            [SerializeField] public string voice;
            [SerializeField] public string elembg;
        }
    }
}
