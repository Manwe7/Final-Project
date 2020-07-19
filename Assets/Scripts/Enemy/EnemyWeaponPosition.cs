using UnityEngine;

public class EnemyWeaponPosition : MonoBehaviour
{
    [SerializeField] private Transform _parent = null;
    private float _offset = 270;
    private GameObject _player;
    private Vector3 _direction;
    private float _rotationZ;

    public void Init(GameObject player)
    {
        _player = player;
    }

    private void Update()
    {
        if(_player != null)
        {
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

    private Vector3 GetPosition(float offsetX)
    {
        return new Vector3(_parent.position.x + offsetX, _parent.position.y, _parent.position.z);
    }
}
