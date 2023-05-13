using UnityEngine;

namespace MARDEK.CharacterSystem
{
    // [RequireComponent(typeof(SpriteRenderer))]
    public class PortraitDisplay : MonoBehaviour
    {
        // [SerializeField] float animationSpeed = 1f;
        // [SerializeField] bool _isAnimating = false;
        // [SerializeField] SpriteAnimationClipList clipList = null;

        [SerializeField] AnnunakiPortrait annunaki;
        [SerializeField] HumanPortrait humanF;
        [SerializeField] HumanPortrait humanM;
        [SerializeField] HumanPortrait humanChild;
        [SerializeField] RobotPortrait robot;

        public void SetPortrait(CharacterPortrait portrait)
        {
            this.annunaki.gameObject.SetActive(false);
            this.humanF.gameObject.SetActive(false);
            this.humanM.gameObject.SetActive(false);
            this.humanChild.gameObject.SetActive(false);
            this.robot.gameObject.SetActive(false);

            if (portrait != null)
            {
                this.gameObject.SetActive(true);

                switch (portrait.Type)
                {
                    case PortraitType.annunaki:
                        this.annunaki.SetPortrait(portrait);
                        this.annunaki.gameObject.SetActive(true);
                        break;

                    case PortraitType.humanF:
                        this.humanF.SetPortrait(portrait);
                        this.humanF.gameObject.SetActive(true);
                        break;

                    case PortraitType.humanM:
                        this.humanM.SetPortrait(portrait);
                        this.humanM.gameObject.SetActive(true);
                        break;

                    case PortraitType.humanChild:
                        this.humanChild.SetPortrait(portrait);
                        this.humanChild.gameObject.SetActive(true);
                        break;

                    case PortraitType.robot:
                        this.robot.SetPortrait(portrait);
                        this.robot.gameObject.SetActive(true);
                        break;
                }
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
       
        /*
        public bool isAnimating { get { return _isAnimating; } private set { _isAnimating = value; } }
        public bool currentClipLoops
        {
            get
            {
                if (currentClip == null)
                    return false;
                return currentClip.loop;
            }
        }
        
        SpriteAnimationClip currentClip = null;
        [HideInInspector] [SerializeField] SpriteRenderer spriteRenderer = null;
        float animationRatio = 0f;

        private void OnValidate()
        {
            InitializeFields();
        }

        private void Awake()
        {
            InitializeFields();
        }

        void InitializeFields()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            currentClip = clipList?.GetClipByIndex(0);
            if (currentClip == null)
                spriteRenderer.sprite = null;
            else
                if(spriteRenderer.sprite == null)
                    UpdateSprite(0);
        }

        private void Update()
        {
            if (currentClip != null)
            {
                if (isAnimating)
                {
                    animationRatio += animationSpeed * Time.deltaTime;
                    bool endAnimation = !currentClip.loop && animationRatio > 1;
                    if (endAnimation)
                    {
                        isAnimating = false;
                        animationRatio = 0;
                    }
                    else
                    {
                        UpdateSprite(animationRatio);
                        while (animationRatio > 1)
                            animationRatio = 0;
                    }
                }                
            }
        }

        void UpdateSprite(float _animationRatio)
        {
            if(currentClip != null)
                spriteRenderer.sprite = currentClip.GetSprite(_animationRatio);
        }

        public void StopCurrentAnimation(float forceAnimationRatio)
        {
            StopCurrentAnimation();
            animationRatio = forceAnimationRatio;
            UpdateSprite(animationRatio);
        }

        public void StopCurrentAnimation()
        {
            isAnimating = false;
        }

        public void PlayClipByMoveDirectionReference(MoveDirection reference)
        {
            if(reference == null)
            {
                StopCurrentAnimation(1);
                return;
            }
            SpriteAnimationClip nextClip = clipList.GetClipByReference(reference);
            currentClip = nextClip;
            isAnimating = true;                    
            animationRatio = 0;
        }
        */
    }
}