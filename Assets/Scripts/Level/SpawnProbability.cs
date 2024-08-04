using System.Collections.Generic;
using UnityEngine;

namespace HlStudio
{
    [CreateAssetMenu(fileName = "Spawn Probability", 
        menuName = "Sciptable Object/New Spawn Probability", order = 0)]
    public class SpawnProbability : ScriptableObject
    {
        [System.Serializable]
        public struct EntityProbability
        {
            [field: SerializeField] public string Title { get; private set; }
            [field: SerializeField, Range(0, 100)] public int Probability { get; private set; }
            [field: SerializeField] public GameObject Entity { get; private set; }
        }

        [field: SerializeField] public List<EntityProbability> EntityProbabilities { get; private set; }
        
        public EntityProbability GetEntity()
        {
            int totalProbability = 0;
            foreach (var item in this.EntityProbabilities)
            {
                totalProbability += item.Probability;
            }

            int randomValue = Random.Range(0, totalProbability);
            int cumulativeProbability = 0;

            foreach (var item in this.EntityProbabilities)
            {
                cumulativeProbability += item.Probability;
                if (randomValue < cumulativeProbability)
                {
                    return item;
                }
            }

            return default; // This should never happen if probabilities are set correctly
        }
        
    }
}