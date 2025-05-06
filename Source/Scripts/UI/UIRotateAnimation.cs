using UnityEngine;
using DG.Tweening;

namespace UIAnimations
{
    /// <summary>
    /// Handles rotation animations for UI elements
    /// </summary>
    [AddComponentMenu("UI Animations/Rotate Animation")]
    public class UIRotateAnimation : UIAnimationBase
    {
        public enum RotateType
        {
            RotateTo,
            RotateBy,
            RotateContinuous
        }
        
        [Header("Rotation Settings")]
        [SerializeField] private RotateType rotateType = RotateType.RotateTo;
        [SerializeField] private Vector3 startRotation = Vector3.zero;
        [SerializeField] private Vector3 endRotation = new Vector3(0, 0, 360);
        [SerializeField] private bool useLocalRotation = true;
        
        private Vector3 originalRotation;
        
        protected override void Awake()
        {
            base.Awake();
            originalRotation = useLocalRotation ? rectTransform.localEulerAngles : rectTransform.eulerAngles;
        }
        
        protected override void OnEnable()
        {
            // Set initial rotation
            if (rotateType != RotateType.RotateContinuous)
            {
                SetRotation(startRotation);
            }
            
            base.OnEnable();
        }
        
        protected override void CreateAnimation()
        {
            if (rotateType == RotateType.RotateTo)
            {
                if (useLocalRotation)
                {
                    currentTween = rectTransform.DOLocalRotate(endRotation, duration, RotateMode.FastBeyond360);
                }
                else
                {
                    currentTween = rectTransform.DORotate(endRotation, duration, RotateMode.FastBeyond360);
                }
            }
            else if (rotateType == RotateType.RotateBy)
            {
                if (useLocalRotation)
                {
                    currentTween = rectTransform.DOLocalRotateQuaternion(
                        Quaternion.Euler(rectTransform.localEulerAngles + endRotation), 
                        duration);
                }
                else
                {
                    currentTween = rectTransform.DORotateQuaternion(
                        Quaternion.Euler(rectTransform.eulerAngles + endRotation), 
                        duration);
                }
            }
            else if (rotateType == RotateType.RotateContinuous)
            {
                // For continuous rotation, we always loop
                loop = true;
                if (loopCount == 0) loopCount = -1; // Infinite if not set
                
                if (useLocalRotation)
                {
                    currentTween = rectTransform.DOLocalRotate(endRotation, duration, RotateMode.FastBeyond360);
                }
                else
                {
                    currentTween = rectTransform.DORotate(endRotation, duration, RotateMode.FastBeyond360);
                }
            }
        }
        
        private void SetRotation(Vector3 rotation)
        {
            if (useLocalRotation)
            {
                rectTransform.localEulerAngles = rotation;
            }
            else
            {
                rectTransform.eulerAngles = rotation;
            }
        }
        
        /// <summary>
        /// Set the rotate type at runtime
        /// </summary>
        public void SetRotateType(RotateType type)
        {
            rotateType = type;
            
            if (rotateType != RotateType.RotateContinuous)
            {
                SetRotation(startRotation);
            }
        }
        
        /// <summary>
        /// Set rotation values at runtime
        /// </summary>
        public void SetRotationValues(Vector3 start, Vector3 end)
        {
            startRotation = start;
            endRotation = end;
        }
        
        /// <summary>
        /// Reset to original rotation
        /// </summary>
        public void ResetToOriginal()
        {
            Stop(false);
            SetRotation(originalRotation);
        }
    }
}
