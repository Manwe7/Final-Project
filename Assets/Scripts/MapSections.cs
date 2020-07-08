using UnityEngine;

//Map sections
public class MapSections : MonoBehaviour
{
    [Header("Sections")]
    [SerializeField] private GameObject[] LeftSection = null;
    [SerializeField] private GameObject[] CenterSection = null;    
    [SerializeField] private GameObject[] RightSection = null;

    private int _leftSectionMap, _CenterSectionMap, _rightSectionMap;

    private void Start()
    {
        //DeActivate all sections
        for (int i = 0; i < LeftSection.Length; i++)
        {
            LeftSection[i].SetActive(false);
            CenterSection[i].SetActive(false);
            RightSection[i].SetActive(false);
        }

        //Choose random map in each section
        _leftSectionMap = Random.Range(0, LeftSection.Length);
        _CenterSectionMap = Random.Range(0, CenterSection.Length);
        _rightSectionMap = Random.Range(0, RightSection.Length);

        //Activate chosen map in each section
        LeftSection[_leftSectionMap].SetActive(true);
        CenterSection[_CenterSectionMap].SetActive(true);
        RightSection[_rightSectionMap].SetActive(true);
    }
}
