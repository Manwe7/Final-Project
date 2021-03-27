using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DifficultyLevel
{
    public class RecordPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _record1Text;
        [SerializeField] private TextMeshProUGUI _record2Text;
        [SerializeField] private TextMeshProUGUI _record3Text;

        private int _record1;
        private int _record2;
        private int _record3;
        
        private void Awake()
        {
            _record1 = PlayerPrefs.GetInt(SaveAttributes.RecordDifficulty1, 0);
            _record2 = PlayerPrefs.GetInt(SaveAttributes.RecordDifficulty2, 0);
            _record3 = PlayerPrefs.GetInt(SaveAttributes.RecordDifficulty3, 0);

            _record1Text.text = $"{_record1}";
            _record2Text.text = $"{_record2}";
            _record3Text.text = $"{_record3}";
        }
    }
}
