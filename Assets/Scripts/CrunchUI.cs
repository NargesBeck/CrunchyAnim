using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrunchyAnim
{
    public class CrunchUI : MonoBehaviour
    {
        public enum LerpType { Linear, SoftEnd }

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
        public static void CrunchMoveX(RectTransform target, float startAnchoredX, float endAnchoredXValue, float t, int framesPerSecond, Action onComplete = null, LerpType lerpType = LerpType.Linear)
        {
            if (target == null)
            {
                Debug.Log("[CrunchyAnim] Please pass an existing target.");
                return;
            }
            if (t < 0)
            {
                Debug.Log("[CrunchyAnim] Please pass an valid non-negative duration.");
                return;
            }
            if (framesPerSecond < 1)
            {
                Debug.Log("[CrunchyAnim] Please pass an valid non-negative fps value.");
                return;
            }
            Instance.MoveX(target, startAnchoredX, endAnchoredXValue, t, framesPerSecond, onComplete, lerpType);
        }
        #endregion

        #region Hidden from outside. This is where it all takes place.
        private void MoveX(RectTransform target, float startAnchoredX, float endAnchoredXValue, float t, int framesPerSecond, Action onComplete, LerpType lerpType)
        {

        }

        #endregion
    }
}