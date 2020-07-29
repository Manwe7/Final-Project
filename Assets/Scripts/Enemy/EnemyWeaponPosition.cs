using UnityEngine;

public class EnemyWeaponPosition : Weapon
{    
    private GameObject _player;

    private void Start()
    {
        _offset = 270;
    }

    public void Init(GameObject player)
    {
        _player = player;
    }

    protected override void ChangeWeaponPosition()
    {
        if(_player == null) { return; }
        
        Vector3 PlayerPos = _player.transform.position;

        _direction = _player.transform.position - transform.position;
        _rotationZ = Mathf.Atan2(_direction.y,_direction.x) * Mathf.Rad2Deg;

        if(PlayerPos.x < transform.position.x) 
        {
            SetToLeftSide();
        } 
        else
        {
            SetToRightSide();
        }
    }

    private void SetToRightSide()
    {                
        transform.rotation = Quaternion.Euler(0, 0f, _rotationZ + _offset);
        transform.position = GetPosition(0.3f);
    }

    private void SetToLeftSide()
    {     
        transform.rotation = Quaternion.Euler(-180f, 0f, -_rotationZ + _offset);
        transform.position = GetPosition(-0.3f);
    }
}
