using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MARDEK.CharacterSystem;

namespace MARDEK.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        static DialogueManager instance;
        static bool _isOngoing = false;
        public static bool isOngoing
        {
            get
            {
                if (instance == null)
                    return false;
                else
                    return _isOngoing;
            }
            private set
            {
                _isOngoing = value;
            }
        }

        [SerializeField] GameObject canvas = null;
        [SerializeField] TextMeshProUGUI dialogueText = null;
        [SerializeField] TextMeshProUGUI characterNameText = null;
        [SerializeField] float characterNameTextXPositionWithPortrait = 450;
        [SerializeField] float characterNameTextXPositionWithoutPortrait = 50;
        [SerializeField] Image characterElementImage = null;
        [SerializeField] PortraitDisplay characterPortrait = null;
        [SerializeField] float dialogueSpeed = 5;

        Dialogue dialogue;
        int dialogueIndex = 0;
        int lineIndex = 0;
        float letterIndex = 0;
        bool isSkipping = false;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                ResetManager();
            }
        }

        private void Update()
        {
            if (isOngoing)
            {
                if (letterIndex >= 0)
                    letterIndex += Time.deltaTime * dialogueSpeed;
                UpdateText();
                if (isSkipping)
                    OnGoToNextLine();
            }
        }

        private void ResetManager()
        {
            isOngoing = false;
            isSkipping = false;
            dialogueIndex = 0;
            lineIndex = 0;
            dialogue = null;
        }

        [ContextMenu("OnGoToNextLine")]
        public void OnGoToNextLine()
        {
            if (isOngoing == false)
                return;

            if (letterIndex < 0)
            {
                if (AdvanceLine() == false)
                    EndDialogue();
            }
            else
                letterIndex = -1;
        }

        public void SetSkipping(bool value)
        {
            isSkipping = value;
        }

        void TryStartDialogues()
        {
            dialogueIndex = -1;
            if (AdvanceDialogueList())
            {
                isOngoing = true;
                canvas.SetActive(true);
                UpdateText();
            }
        }

        string CurrentLine()
        {
            if (dialogue == null)
                return string.Empty;

            var characterDialogueLines = dialogue.CharacterLines[dialogueIndex];
            string line = characterDialogueLines.WrappedLines[lineIndex].line;

            int lengthToShow = line.Length;
            if (letterIndex >= line.Length || letterIndex < 0)
                letterIndex = -1;
            else
                lengthToShow = Mathf.FloorToInt(letterIndex);

            string lineToShow = line.Substring(0, lengthToShow) + "<color=#00000000>";
            lineToShow += line.Substring(lengthToShow, line.Length - lengthToShow) + "</color>";
            return lineToShow;
        }

        bool AdvanceLine()
        {
            lineIndex++;
            var characterDialogueLines = dialogue.CharacterLines[dialogueIndex];
            if (lineIndex >= characterDialogueLines.WrappedLines.Count)
            {
                return AdvanceDialogueList();
            }
            letterIndex = 0;
            UpdatePortrait();
            return true;
        }

        bool AdvanceDialogueList()
        {
            dialogueIndex++;
            if (dialogueIndex >= dialogue.CharacterLines.Count)
            {
                return false;
            }

            lineIndex = -1;
            if (AdvanceLine())
            {
                OnUpdateLineEntry();
                return true;
            }
            return false;
        }

        private void OnUpdateLineEntry()
        {
            CharacterProfile profile = dialogue?.CharacterLines[dialogueIndex].Character;
            characterNameText.text = profile != null ? profile.displayName : string.Empty;
            SetElementIcon(profile);
        }

        private void UpdatePortrait()
        {
            var profile = dialogue?.CharacterLines[dialogueIndex].Character;
            if (profile?.portrait != null)
            {
                characterPortrait.gameObject.SetActive(true);
                characterPortrait.SetPortrait(profile.portrait);

                var nameTextPosition = characterNameText.rectTransform.localPosition;
                nameTextPosition.x = characterNameTextXPositionWithPortrait;
                characterNameText.rectTransform.SetLocalPositionAndRotation(nameTextPosition, Quaternion.identity);

                var currentExpression = dialogue.CharacterLines[dialogueIndex].WrappedLines[lineIndex].expression;
                characterPortrait.SetExpression(currentExpression);
            }
            else
            {
                characterPortrait.gameObject.SetActive(false);

                var nameTextPosition = characterNameText.rectTransform.localPosition;
                nameTextPosition.x = characterNameTextXPositionWithoutPortrait;
                characterNameText.rectTransform.SetLocalPositionAndRotation(nameTextPosition, Quaternion.identity);
            }
        }

        void SetElementIcon(CharacterProfile profile)
        {
            if (profile?.element != null)
            {
                characterElementImage.enabled = true;
                characterElementImage.sprite = profile.element.thinSprite;
            }
            else
                characterElementImage.enabled = false;
        }

        void EndDialogue()
        {
            ResetManager();
            OnUpdateLineEntry();
            UpdateText();
            canvas.SetActive(false);
        }

        void UpdateText()
        {
            dialogueText.text = CurrentLine();
        }

        public static void EnqueueDialogue(Dialogue _dialogue)
        {
            if (instance == null)
            {
                Debug.LogWarning("No DialogueManager instance found");
            }
            else
            {
                instance.dialogue = _dialogue;
                if (isOngoing == false)
                    instance.TryStartDialogues();
            }
        }
    }
}