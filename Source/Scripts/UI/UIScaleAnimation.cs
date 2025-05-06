using UnityEngine;
using DG.Tweening;

namespace UIAnimations
{
    /// <summary>
    /// Handles scale animations for UI elements
    /// </summary>
    [AddComponentMenu("UI Animations/Scale Animation")]
    public class UIScaleAnimation : UIAnimationBase
    {
        public enum ScaleType
        {
            ScaleIn,
            ScaleOut,
            ScalePulse,
            ScaleElastic
        }
        
        [Header("Scale Settings")]
        [SerializeField] private ScaleType scaleType = ScaleType.ScaleIn;
        [SerializeField] private Vector3 startScale = new Vector3(0.5f, 0.5f, 0.5f);
        [SerializeField] private Vector3 endScale = Vector3.one;
        [SerializeField] private bool useUniformScale = true;
        
        private Vector3 originalScale;
        
        protected override void Awake()
        {
            base.Awake();
            originalScale = rectTransform.localScale;
        }
        
        protected override void OnEnable()
        {
            // Set initial scale based on scale type
            if (scaleType == ScaleType.ScaleIn)
            {
                rectTransform.localScale = startScale;
            }
            else if (scaleType == ScaleType.ScaleOut)
            {
                rectTransform.localScale = endScale;
            }
            
            base.OnEnable();
        }
        
        protected override void CreateAnimation()
        {
            Vector3 targetScale = scaleType == ScaleType.ScaleIn ? endScale : startScale;
            
            if (scaleType == ScaleType.ScalePulse)
            {
                // For pulse, we always loop
                loop = true;
                loopType = LoopType.Yoyo;
                if (loopCount == 0) loopCount = -1; // Infinite if not set
                currentTween = rectTransform.DOScale(endScale, duration);
            }
            else if (scaleType == ScaleType.ScaleElastic)
            {
                // Override ease type for elastic
                currentTween = rectTransform.DOScale(endScale, duration).SetEase(Ease.OutElastic);
            }
            else
            {
                currentTween = rectTransform.DOScale(targetScale, duration);
            }
        }
        
        /// <summary>
        /// Set the scale type at runtime
        /// </summary>
        public void SetScaleType(ScaleType type)
        {
            scaleType = type;
            
            // Reset scale based on new type
            if (scaleType == ScaleType.ScaleIn)
            {
                rectTransform.localScale = startScale;
            }
            else if (scaleType == ScaleType.ScaleOut)
            {
                rectTransform.localScale = endScale;
            }
        }
        
        /// <summary>
        /// Set uniform scale values
        /// </summary>
        public void SetUniformScale(float start, float end)
        {
            if (useUniformScale)
            {
                startScale = new Vector3(start, start, start);
                endScale = new Vector3(end, end, end);
            }
        }
        
        /// <summary>
        /// Scale in the UI element
        /// </summary>
        public void ScaleIn()
        {
            SetScaleType(ScaleType.ScaleIn);
            Play();
        }
        
        /// <summary>
        /// Scale out the UI element
        /// </summary>
        public void ScaleOut()
        {
            SetScaleType(ScaleType.ScaleOut);
            Play();
        }
        
        /// <summary>
        /// Reset to original scale
        /// </summary>
        public void ResetToOriginal()
        {
            Stop(false);
            rectTransform.localScale = originalScale;
        }
    }
}
