using UnityEngine;
using DG.Tweening;

namespace UIAnimations
{
    /// <summary>
    /// Handles shake animations for UI elements
    /// </summary>
    [AddComponentMenu("UI Animations/Shake Animation")]
    public class UIShakeAnimation : UIAnimationBase
    {
        public enum ShakeType
        {
            Position,
            Rotation,
            Scale
        }
        
        [Header("Shake Settings")]
        [SerializeField] private ShakeType shakeType = ShakeType.Position;
        [SerializeField] private float strength = 10f;
        [SerializeField] private int vibrato = 10;
        [SerializeField] private float randomness = 90f;
        [SerializeField] private bool fadeOut = true;
        [SerializeField] private bool snapping = false;
        
        private Vector3 originalPosition;
        private Vector3 originalRotation;
        private Vector3 originalScale;
        
        protected override void Awake()
        {
            base.Awake();
            
            // Store original values
            originalPosition = rectTransform.anchoredPosition;
            originalRotation = rectTransform.localEulerAngles;
            originalScale = rectTransform.localScale;
        }
        
        protected override void CreateAnimation()
        {
            switch (shakeType)
            {
                case ShakeType.Position:
                    currentTween = rectTransform.DOShakeAnchorPos(duration, strength, vibrato, randomness, snapping, fadeOut);
                    break;
                case ShakeType.Rotation:
                    currentTween = rectTransform.DOShakeRotation(duration, strength, vibrato, randomness, fadeOut);
                    break;
                case ShakeType.Scale:
                    currentTween = rectTransform.DOShakeScale(duration, strength, vibrato, randomness, fadeOut);
                    break;
            }
        }
        
        /// <summary>
        /// Set the shake type at runtime
        /// </summary>
        public void SetShakeType(ShakeType type)
        {
            shakeType = type;
        }
        
        /// <summary>
        /// Set shake parameters at runtime
        /// </summary>
        public void SetShakeParameters(float newStrength, int newVibrato, float newRandomness)
        {
            strength = newStrength;
            vibrato = newVibrato;
            randomness = newRandomness;
        }
        
        /// <summary>
        /// Reset to original values
        /// </summary>
        public void ResetToOriginal()
        {
            Stop(false);
            
            rectTransform.anchoredPosition = originalPosition;
            rectTransform.localEulerAngles = originalRotation;
            rectTransform.localScale = originalScale;
        }
    }
}
