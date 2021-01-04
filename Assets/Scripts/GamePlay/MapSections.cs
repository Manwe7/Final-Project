using UnityEngine;

public class MapSections : MonoBehaviour
{
    [Header("Sections")]
    [SerializeField] private GameObject[] _leftSection;
    [SerializeField] private GameObject[] _centerSection;    
    [SerializeField] private GameObject[] _rightSection;

    private int _leftSectionMap, _centerSectionMap, _rightSectionMap;

    private void Start()
    {
        DeActivateAllSections();
        SelectRandomMap();
        ActivateChosenMap();        
    }

    private void DeActivateAllSections()
    {
        for (var i = 0; i < _leftSection.Length; i++)
        {
            _leftSection[i].SetActive(false);
            _centerSection[i].SetActive(false);
            _rightSection[i].SetActive(false);
        }
    }

    private void SelectRandomMap()
    {
        _leftSectionMap = Random.Range(0, _leftSection.Length);
        _centerSectionMap = Random.Range(0, _centerSection.Length);
        _rightSectionMap = Random.Range(0, _rightSection.Length);
    }

    private void ActivateChosenMap()
    {
        _leftSection[_leftSectionMap].SetActive(true);
        _centerSection[_centerSectionMap].SetActive(true);
        _rightSection[_rightSectionMap].SetActive(true);
    }
}
