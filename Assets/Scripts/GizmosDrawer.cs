using System;
using System.Diagnostics;
using UnityEngine;

namespace HlStudio
{
    public class GizmosDrawer : MonoBehaviour
    {
        [Conditional("UNITY_EDITOR")]
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, 2f);
        }
    }
}