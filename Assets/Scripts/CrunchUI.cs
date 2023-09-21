using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CrunchyAnim
{
    public class CrunchUI : MonoBehaviour
    {
        public enum LerpType { Linear, SoftEnd, CustomCurve }
        public enum MovementType { MoveX, MoveY, MoveXY, ScaleXY, Shake }
        public enum RepeatType { Once, Loop, PingPong }

        private static CrunchUI instance;
        private static CrunchUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<CrunchUI>();
                    if (instance == null)
                    {
                        Debug.Log("[CrunchyAnim] Make sure initalizer prefab is added to the scene.");
                    }
                }
                return instance;
            }
        }

        #region Public and Static. To be used by outsiders.
        public static void KillMovement(string id)
        {
            Instance.FindMovementByID(id).Kill();
        }

        public static void PauseMovement(string id)
        {
            Instance.FindMovementByID(id).Pause();
        }

        public static void UnpauseMovement(string id)
        {
            Instance.FindMovementByID(id).Unpause();
        }

        public static void CompleteMovement(string id)
        {
            Instance.FindMovementByID(id).Complete();
        }

        public static string CrunchMoveX(RectTransform target, float startAnchoredX, float endAnchoredXValue, float duration, int framesPerSecond,
            Action onComplete = null, LerpType lerpType = LerpType.Linear, RepeatType repeatType = RepeatType.Once, AnimationCurve speedCurve = null)
        {
            if (target == null)
            {
                Debug.Log("[CrunchyAnim] Please pass an existing target.");
                return null;
            }
            if (duration < 0)
            {
                Debug.Log("[CrunchyAnim] Please pass an valid non-negative duration.");
                return null;
            }
            if (framesPerSecond < 1)
            {
                Debug.Log("[CrunchyAnim] Please pass an valid non-negative fps value.");
                return null;
            }
            if (speedCurve != null)
            {
                lerpType = LerpType.CustomCurve;
            }
            return Instance.MoveX(target, startAnchoredX, endAnchoredXValue, duration, framesPerSecond, onComplete, lerpType, repeatType, speedCurve);
        }
        #endregion

        #region Hidden from outside. This is where it all takes place.

        private List<CrunchyUIMovement> AllCurrentCrunchyAnimMovements = new List<CrunchyUIMovement>();

        private void Update()
        {
            
        }

        private string GenerateIDForMovement(RectTransform target, LerpType lerpType, RepeatType repeatType, MovementType movementType)
        {
            return $"[{movementType}][{repeatType}][{lerpType}][{target.GetInstanceID()}]";
        }

        private CrunchyUIMovement FindMovementByID(string id)
        {
            return default;
        }

        private string MoveX(RectTransform target, float startAnchoredX, float endAnchoredXValue, float duration, int framesPerSecond,
            Action onComplete, LerpType lerpType, RepeatType repeatType, AnimationCurve speedCurve)
        {
            MovementType movementType = MovementType.MoveX;
            string id = GenerateIDForMovement(target, lerpType, repeatType, movementType);
            Vector2 start = new Vector2(startAnchoredX, target.anchoredPosition.y);
            Vector2 end = new Vector2(endAnchoredXValue, target.anchoredPosition.y);

            CrunchyUIMovement crunchyUIMovement = new CrunchyUIMovement(id, target, start, end, duration, framesPerSecond, onComplete, lerpType, repeatType, movementType, speedCurve);
            crunchyUIMovement.Play();

            return id;
        }

        #endregion

        private class CrunchyUIMovement
        {
            private string CrunchyMovementID;
            private RectTransform TargetRectTransform;
            private Vector2 StartValue;
            private Vector2 EndValue;
            private float Duration;
            private int FPS;
            private Action OnComplete;
            private CrunchUI.LerpType LerpType;
            private CrunchUI.RepeatType RepeatType;
            private CrunchUI.MovementType MovementType;
            private AnimationCurve SpeedCurve;
            public float CrunchedDeltaTime;
            public float T;

            public CrunchyUIMovement() { }

            public CrunchyUIMovement(string crunchMovementID, RectTransform targetRectTransform, Vector2 startValue, Vector2 endValue, float duration, int fPS,
                Action onComplete, CrunchUI.LerpType lerpType, CrunchUI.RepeatType repeatType, CrunchUI.MovementType movementType, AnimationCurve speedCurve)
            {
                CrunchyMovementID = crunchMovementID;
                TargetRectTransform = targetRectTransform;
                StartValue = startValue;
                EndValue = endValue;
                Duration = duration;
                FPS = fPS;
                OnComplete = onComplete;
                LerpType = lerpType;
                RepeatType = repeatType;
                MovementType = movementType;
                SpeedCurve = speedCurve;

                CrunchedDeltaTime = fPS * Time.unscaledDeltaTime / (1f / Time.unscaledDeltaTime);
            }

            public void OnUpdate(float deltaT)
            {
                T += deltaT;
                if (T >= CrunchedDeltaTime)
                {
                    T = 0;
                    switch(MovementType)
                    {
                        case MovementType.MoveX:

                            TargetRectTransform.anchoredPosition = CrunchyMover.MoveX(StartValue, EndValue, LerpType, RepeatType, SpeedCurve, out bool isDone);
                            if (isDone) { }
                            break;
                    }
                }

            }

            public void Play()
            {
                T = 0;
                Instance.AllCurrentCrunchyAnimMovements.Add(this);
            }

            public void Pause()
            {

            }

            public void Unpause()
            {

            }

            public void Kill()
            {
                Instance.AllCurrentCrunchyAnimMovements.Remove(this);
            }

            public void Complete()
            {

            }

            private void MovementHasCompleted()
            {
                if (RepeatType == RepeatType.Once)
                    Kill();
            }
        }
    }
}