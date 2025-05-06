using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace UIAnimations
{
    /// <summary>
    /// Handles sequence animations for UI elements by combining multiple animations
    /// </summary>
    [AddComponentMenu("UI Animations/Sequence Animation")]
    public class UISequenceAnimation : MonoBehaviour
    {
        [Serializable]
        public class AnimationStep
        {
            public UIAnimationBase animation;
            public float delay = 0f;
            public bool waitForCompletion = true;
        }
        
        [Header("Sequence Settings")]
        [SerializeField] private List<AnimationStep> animationSequence = new List<AnimationStep>();
        [SerializeField] private bool playOnEnable = true;
        [SerializeField] private bool loop = false;
        [SerializeField] private float loopDelay = 0f;
        
        private Sequence currentSequence;
        
        private void OnEnable()
        {
            if (playOnEnable)
            {
                Play();
            }
        }
        
        private void OnDisable()
        {
            Stop(false);
        }
        
        /// <summary>
        /// Play the animation sequence
        /// </summary>
        public void Play()
        {
            Stop(false);
            
            if (animationSequence.Count == 0)
            {
                Debug.LogWarning("UISequenceAnimation: No animations in sequence");
                return;
            }
            
            currentSequence = DOTween.Sequence();
            
            foreach (var step in animationSequence)
            {
                if (step.animation == null) continue;
                
                // Prevent the animation from auto-playing
                step.animation.gameObject.SetActive(false);
                step.animation.gameObject.SetActive(true);
                
                // Add delay
                if (step.delay > 0)
                {
                    currentSequence.AppendInterval(step.delay);
                }
                
                // Create a callback to play the animation
                currentSequence.AppendCallback(() => {
                    step.animation.Play();
                });
                
                // Wait for completion if needed
                if (step.waitForCompletion)
                {
                    // Get the duration from the animation
                    float waitTime = step.animation.GetDuration();
                    currentSequence.AppendInterval(waitTime);
                }
            }
            
            if (loop)
            {
                currentSequence.AppendInterval(loopDelay);
                currentSequence.SetLoops(-1, LoopType.Restart);
            }
            
            currentSequence.Play();
        }
        
        /// <summary>
        /// Stop the animation sequence
        /// </summary>
        /// <param name="complete">If true, the sequence will complete (go to end)</param>
        public void Stop(bool complete)
        {
            if (currentSequence != null && currentSequence.IsActive())
            {
                currentSequence.Kill(complete);
                currentSequence = null;
            }
            
            // Also stop all individual animations
            foreach (var step in animationSequence)
            {
                if (step.animation != null)
                {
                    step.animation.Stop(complete);
                }
            }
        }
        
        /// <summary>
        /// Pause the animation sequence
        /// </summary>
        public void Pause()
        {
            if (currentSequence != null && currentSequence.IsActive())
            {
                currentSequence.Pause();
            }
        }
        
        /// <summary>
        /// Resume the animation sequence
        /// </summary>
        public void Resume()
        {
            if (currentSequence != null && currentSequence.IsPlaying() == false)
            {
                currentSequence.Play();
            }
        }
        
        /// <summary>
        /// Add an animation step to the sequence at runtime
        /// </summary>
        public void AddAnimationStep(UIAnimationBase animation, float delay = 0f, bool waitForCompletion = true)
        {
            if (animation == null) return;
            
            AnimationStep step = new AnimationStep
            {
                animation = animation,
                delay = delay,
                waitForCompletion = waitForCompletion
            };
            
            animationSequence.Add(step);
        }
        
        /// <summary>
        /// Clear all animation steps from the sequence
        /// </summary>
        public void ClearSequence()
        {
            Stop(false);
            animationSequence.Clear();
        }
    }
    
    // Extension method for UIAnimationBase to get duration
    public static class UIAnimationBaseExtensions
    {
        public static float GetDuration(this UIAnimationBase animation)
        {
            // Use reflection to get the duration field
            var durationField = typeof(UIAnimationBase).GetField("duration", 
                System.Reflection.BindingFlags.Instance | 
                System.Reflection.BindingFlags.NonPublic);
            
            if (durationField != null)
            {
                return (float)durationField.GetValue(animation);
            }
            
            return 0.5f; // Default duration if field not found
        }
    }
}
