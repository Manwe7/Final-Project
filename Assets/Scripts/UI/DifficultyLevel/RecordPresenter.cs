using BaseClasses;
using Interfaces;
using TMPro;
using UnityEngine;

namespace UI.DifficultyLevel
{
    public class RecordPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _recordTexts;
        
        private IRepo<SaveAttributes> _repo;
        private SaveAttributes _saveAttributes;
        
        private void Awake()
        {
            _repo = new SaveClassRepo();
            _saveAttributes = _repo.Get();
            
            for (int i = 0; i < _recordTexts.Length; i++)
            {
                _recordTexts[i].text = $"{_saveAttributes.Records[i]}";
            }
        }
    }
}
