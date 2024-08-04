using UnityEngine;

namespace HlStudio
{
    [CreateAssetMenu(fileName = "Session Preset", 
        menuName = "Sciptable Object/New Session Preset", order = 0)]
    public class SessionPreset : ScriptableObject
    {
        [field: SerializeField] public int NumberOfEntities { get; private set; }
        [field: SerializeField] public Vector3 Accelerations { get; private set; }
        [field: SerializeField] public float VelocityLimit { get; private set; }
        [field: SerializeField] public float DestinationTheshold { get; private set; }
    }
}