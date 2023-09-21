using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrunchyAnim;

public class CrunchyAnimExamples : MonoBehaviour
{
    private void Foo()
    {
        CrunchUI.CrunchMoveX(GetComponent<RectTransform>(), 0, 100, 1, 20);
    }
}
