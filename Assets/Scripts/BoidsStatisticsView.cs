using TMPro;
using UnityEngine;

namespace HlStudio
{
    public class BoidsStatisticsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _numberOfEntitesText;
        
        public void UpdateStatistics(int numberOfEntities)
        {
            _numberOfEntitesText.text = $"Entities: " + numberOfEntities;
        }
    }
}