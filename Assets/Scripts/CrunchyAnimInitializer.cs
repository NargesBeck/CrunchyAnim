using UnityEngine;
using CrunchyAnim;

namespace CrunchyAnim
{
    public class CrunchyAnimInitializer : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}