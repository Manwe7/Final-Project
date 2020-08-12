using UnityEngine;
using UnityEngine.UI;

public class SaveTesting : MonoBehaviour
{
    [SerializeField] private Text _moneyText, _recordText;

    private int _money, _record;

    private IRepo<SaveAttributes> _repoInt;

    SaveAttributes _saveAttributes = new SaveAttributes();

    private void Awake()
    {
        _saveAttributes = _repoInt.Get();
        _record = _saveAttributes.Record;

        ShowText();
    }

    private void ShowText()
    {
        _moneyText.text = $"{_money}";
        _recordText.text = $"{_record}";
    }

    private void SaveData()
    {
        _saveAttributes.Record = _record;
        _repoInt.Save(_saveAttributes);
        
        ShowText();
    }

    public void Add()
    {
        _money += 10;
        _record += 10;
        SaveData();
    }

    public void Subtract()
    {
        _money -= 5;
        _record -= 5;
        SaveData();
    }
}
