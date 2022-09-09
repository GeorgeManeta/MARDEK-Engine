using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MARDEK.Core;
using MARDEK.Save;
using MARDEK.Movement;

namespace MARDEK.Progress
{
    public class ExploredAreas : AddressableMonoBehaviour
    {
        [SerializeField] Dictionary<string, ExploredArea> exploredAreas = new Dictionary<string, ExploredArea>();
        [SerializeField] PlayerController player;

        int counter = 0;

        public void MarkDiscovered(string sceneID, int tileX, int tileY)
        {
            if (!exploredAreas.ContainsKey(sceneID))
            {
                exploredAreas.Add(sceneID, new ExploredArea());
            }
            exploredAreas[sceneID].MarkDiscovered(tileX, tileY);
        }

        public bool IsDiscovered(string sceneID, int tileX, int tileY)
        {
            exploredAreas.TryGetValue(sceneID, out ExploredArea area);
            return area != null && area.IsDiscovered(tileX, tileY);
        }

        void FixedUpdate()
        {
            counter += 1;
            if (counter % 50 == 0)
            {
                MarkAreaAroundPlayerAsDiscovered();
                counter = 0;
            }
        }

        void MarkAreaAroundPlayerAsDiscovered()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneInfo sceneInfo = null;

            foreach (GameObject gameObject in activeScene.GetRootGameObjects())
            {
                sceneInfo = gameObject.GetComponent<SceneInfo>();
                if (sceneInfo != null)
                {
                    break;
                }
            }

            int playerX = (int) player.transform.position.x;
            int playerY = (int) player.transform.position.y;

            // The original game used also used a discover radius of 9 tiles
            int discoverRadius = 9;
            for (int x = playerX - discoverRadius; x <= playerX + discoverRadius; x++)
            {
                for (int y = playerY - discoverRadius; y <= playerY + discoverRadius; y++)
                {
                    MarkDiscovered(sceneInfo.id, x, y);
                }
            }
        }
    }
}