using System.Collections.Generic;
using UnityEngine;

namespace HlStudio
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField, Header("Init order")] private List<GameObject> _initQueue;
        [SerializeField] private bool _debug;
        
        private async void Start()
        {
            foreach (var obj in _initQueue)
            {
                if (obj.TryGetComponent(out IInitializable initializable))
                {
                    if(_debug) print($"<color=yellow>{initializable}</color> <color=red>started</color> Init at {Time.time}");
                    
                    await initializable.Init();
                    initializable.Initialized = true;
                    
                    if(_debug) print($"<color=yellow>{initializable}</color> <color=green>finished</color> Init at {Time.time}");
                }
            }
        }
    }
}