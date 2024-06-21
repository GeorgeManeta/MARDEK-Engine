using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Animation
{ 
    public class TransitionEffectManager : MonoBehaviour
    {
        static TransitionEffectManager instance;
        public enum TransitionEffectType
        {
            None,
            Fade,
        }
        TransitionEffectType currentTransitionEffect = TransitionEffectType.None;
        float animationTimer = 0f;
        [SerializeField] UnityEngine.UI.Image fadeImage;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 1f;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                gameObject.transform.parent = null;
                DontDestroyOnLoad(this);
            }
            else
                Destroy(this);
        }

        private void LateUpdate()
        {
            if (currentTransitionEffect == TransitionEffectType.Fade)
            {
                animationTimer += Time.deltaTime;
                float fadeAmount;
                if (animationTimer < fadeOutTime)
                    fadeAmount = animationTimer / fadeOutTime;
                else if (animationTimer < fadeOutTime + fadeInTime)
                    fadeAmount = 1f - (animationTimer - fadeOutTime) / fadeInTime;
                else
                {
                    fadeAmount = 0;
                    currentTransitionEffect = TransitionEffectType.None;
                }
                if (fadeAmount > 0.9f)
                    fadeAmount = 1f;
                fadeImage.color = new Color(0, 0, 0, fadeAmount);
            }
        }

        public static void PlayEffect(TransitionEffectType type)
        {
            if (instance)
                instance.StartAnimation(type);
        }

        void StartAnimation(TransitionEffectType type)
        {
            currentTransitionEffect = type;
            animationTimer = 0f;
        }

        public static bool Wait()
        {
            if (instance)
                return instance.WaitInternal();
            else
                return false;
        }

        bool WaitInternal()
        {
            switch (currentTransitionEffect)
            {
                case TransitionEffectType.Fade:
                    return animationTimer < fadeOutTime;
                default:
                    return false;
            }
        }
    }
}