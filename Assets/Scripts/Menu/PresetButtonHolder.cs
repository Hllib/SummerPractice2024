using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HlStudio
{
    public class PresetButtonHolder : MonoBehaviour
    {
        [SerializeField] private List<SelectPresetButton> _selectPresetButtons;
        [SerializeField] private Slider _numberOfEntitiesSlider;
        [SerializeField] private TMP_Text _presetDecsription;

        private void Start()
        {
            foreach (var button in _selectPresetButtons)
            {
                button.Chosen += Chosen;
            }

            _selectPresetButtons[0].ForceSelect();
        }

        private void Chosen(int hashCode)
        {
            var selectPresetButton = _selectPresetButtons.Find(button => button.GetHashCode() == hashCode);
            selectPresetButton.SetAvailability(false);

            SetDescription(selectPresetButton.Preset);
            _numberOfEntitiesSlider.value = selectPresetButton.Preset.NumberOfEntities;

            foreach (var button in _selectPresetButtons.Where(button => button.GetHashCode() != hashCode))
            {
                button.SetAvailability(true);
            }
        }

        private void SetDescription(SessionPreset preset)
        {
            _presetDecsription.text =
                $"Entities Speed: <color=red>{preset.VelocityLimit}</color>. M" +
                $"ovement Offset: <color=red>{preset.DestinationTheshold}</color> <br> " +
                $"Accelerations: <color=red>{preset.Accelerations}</color>";
        }
    }
}