using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSections : MonoBehaviour
{
    [Header("Sections")]
    [SerializeField] private GameObject[] LeftSection = null;
    [SerializeField] private GameObject[] CenterSection = null;    
    [SerializeField] private GameObject[] RightSection = null;

    private int _leftSectionMap, _CenterSectionMap, _rightSectionMap;

    private void Start()
    {
        for (int i = 0; i < LeftSection.Length; i++)
        {
            LeftSection[i].SetActive(false);
            CenterSection[i].SetActive(false);
            RightSection[i].SetActive(false);
        }

        _leftSectionMap = Random.Range(0, LeftSection.Length);
        _CenterSectionMap = Random.Range(0, CenterSection.Length);
        _rightSectionMap = Random.Range(0, RightSection.Length);

        LeftSection[_leftSectionMap].SetActive(true);
        CenterSection[_CenterSectionMap].SetActive(true);
        RightSection[_rightSectionMap].SetActive(true);
    }
}
