using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.Stats;

namespace MARDEK.UI
{
    public class UIStatBar : MonoBehaviour
    {
        [SerializeField] CharacterUI characterUI = null;
        [SerializeField] IntegerStat stat;
        [SerializeField] IntegerStat maxStat;
        [SerializeField] RectTransform barTransform;
        [SerializeField] TMPro.TMP_Text statText;
        [SerializeField] TMPro.TMP_Text maxStatText;

        private void OnEnable()
        {
            UpdateBar();
        }

        private void LateUpdate()
        {
            UpdateBar();
        }

        [ContextMenu("Update Bar")]
        void UpdateBar()
        {
            if (characterUI.character == null)
                return;

            var statValue = (float)characterUI.character.GetStat(stat);
            var maxStatValue = (float)characterUI.character.GetStat(maxStat);
            if (statText)
                statText.text = statValue.ToString();
            if (maxStatText) 
                maxStatText.text = maxStatValue.ToString();
            if (barTransform)
            {
                float xScale = Mathf.Clamp(statValue / maxStatValue , 0f, 1f);
                if(float.IsFinite(xScale))
                    barTransform.localScale = new Vector3(xScale, 1, 1);
            }
        }
    }
}