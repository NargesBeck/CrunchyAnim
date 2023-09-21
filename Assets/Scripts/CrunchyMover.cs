using UnityEngine;

namespace CrunchyAnim
{
    public static class CrunchyMover
    {
        public static Vector2 MoveX(Vector2 startPos, Vector2 currentPos, Vector2 endPos, CrunchUI.LerpType lerpType,
            AnimationCurve speedCurve, float t)
        {
            float x = default;
            if (lerpType == CrunchUI.LerpType.Linear)
            {
                x = Mathf.Lerp(startPos.x, endPos.x, t);
            }
            else if (lerpType == CrunchUI.LerpType.SoftEnd)
            {
                x = Mathf.Lerp(currentPos.x, endPos.x, t);
            }
            else if (lerpType == CrunchUI.LerpType.CustomCurve)
            {
                float tLerp = speedCurve.Evaluate(t);
                tLerp = Mathf.Clamp01(tLerp);
                x = Mathf.Lerp(startPos.x, endPos.x, tLerp);
            }
            return new Vector2(x, startPos.y);
        }
    }
}