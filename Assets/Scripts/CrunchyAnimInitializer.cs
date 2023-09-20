using UnityEngine;
using CrunchyAnim;

namespace CrunchyAnim
{
    [RequireComponent(typeof(CrunchyAnimUI))]
    public class CrunchyAnimInitializer : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}