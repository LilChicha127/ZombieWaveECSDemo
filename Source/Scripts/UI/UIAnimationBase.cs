using UnityEngine;
using DG.Tweening;
using System;

namespace UIAnimations
{
    /// <summary>
    /// Base class for all UI animations
    /// </summary>
    public abstract class UIAnimationBase : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] protected float duration = 0.5f;
        [SerializeField] protected Ease easeType = Ease.OutQuad;
        [SerializeField] protected float delay = 0f;
        [SerializeField] protected bool playOnEnable = true;
        [SerializeField] protected bool resetOnDisable = true;
        
        [Header("Loop Settings")]
        [SerializeField] protected bool loop = false;
        [SerializeField] protected int loopCount = -1; // -1 for infinite
        [SerializeField] protected LoopType loopType = LoopType.Restart;
        
        protected Tweener currentTween;
        protected RectTransform rectTransform;
        
        protected virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                Debug.LogError($"UIAnimationBase: No RectTransform found on {gameObject.name}");
                enabled = false;
            }
        }
        
        protected virtual void OnEnable()
        {
            if (playOnEnable)
            {
                Play();
            }
        }
        
        protected virtual void OnDisable()
        {
            if (resetOnDisable)
            {
                Stop(true);
            }
            else
            {
                Stop(false);
            }
        }
        
        /// <summary>
        /// Play the animation
        /// </summary>
        public virtual void Play()
        {
            Stop(false);
            CreateAnimation();
            
            if (loop)
            {
                currentTween.SetLoops(loopCount, loopType);
            }
            
            currentTween.SetDelay(delay)
                .SetEase(easeType);
        }
        
        /// <summary>
        /// Stop the animation
        /// </summary>
        /// <param name="complete">If true, the animation will complete (go to end value)</param>
        public virtual void Stop(bool complete)
        {
            if (currentTween != null && currentTween.IsActive())
            {
                currentTween.Kill(complete);
                currentTween = null;
            }
        }
        
        /// <summary>
        /// Pause the animation
        /// </summary>
        public virtual void Pause()
        {
            if (currentTween != null && currentTween.IsActive())
            {
                currentTween.Pause();
            }
        }
        
        /// <summary>
        /// Resume the animation
        /// </summary>
        public virtual void Resume()
        {
            if (currentTween != null && currentTween.IsPlaying() == false)
            {
                currentTween.Play();
            }
        }
        
        /// <summary>
        /// Create the animation tween - to be implemented by derived classes
        /// </summary>
        protected abstract void CreateAnimation();
    }
}
