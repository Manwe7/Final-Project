using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class DesignManager : MonoBehaviour
    {
        [TagSelector]
        [SerializeField] private string _platformsTag;

        [SerializeField] private Material _blueMaterial;
        [SerializeField] private Material _yellowMaterial;
        [SerializeField] private Material _orangeMaterial;
        [SerializeField] private Material _redMaterial;
        [SerializeField] private Material _violetMaterial;

        private readonly List<SpriteRenderer> _sprites = new List<SpriteRenderer>();

        private GameObject[] _allPlatforms;

        private int _difficultyLevel;

        private void Awake()
        {
            _difficultyLevel = PlayerPrefs.GetInt(SaveAttributes.DifficultyLevel, 0);
        }

        private void Start()
        {
            _allPlatforms = GameObject.FindGameObjectsWithTag(_platformsTag);

            foreach (GameObject t in _allPlatforms)
            {
                _sprites.Add(t.GetComponent<SpriteRenderer>());
            }

            switch (_difficultyLevel)
            {
                case 0:
                    ChangeColor(_blueMaterial, _blueMaterial);
                    break;
                case 1:
                    ChangeColor(_orangeMaterial, _redMaterial);
                    break;
                case 2:
                    ChangeColor(_redMaterial, _violetMaterial);
                    break;
                default:
                    ChangeColor(_blueMaterial, _blueMaterial);
                    break;
            }
        }

        private void ChangeColor(Material material1, Material material2)
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                _sprites[i].material = i % 2 == 0 ? material1 : material2;
            }
        }
    }
}
