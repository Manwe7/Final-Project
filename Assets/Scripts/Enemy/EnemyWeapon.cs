using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] GameObject Parent = null;
    
    private float _offset = 270;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");       
    }
    
    private void Update()
    {
        if(_player != null)
        {
            Vector3 PlayerPos = _player.transform.position;

            //Change position depending in targets position
            if(PlayerPos.x < transform.position.x) 
            {
                LeftSide();
            } 
            else
            {
                RightSide();
            }
        }
    }

    private void RightSide()
    {
        //Rotate and change position to right
        Vector3 dir = _player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0f, rotZ + _offset);  

        transform.position = new Vector3(Parent.transform.position.x + 0.3f, Parent.transform.position.y, Parent.transform.position.z);
    }

    private void LeftSide()
    {
        //Rotate and change position to left
        Vector3 dir = _player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(-180f, 0f, -rotZ + _offset);  

        transform.position = new Vector3(Parent.transform.position.x - 0.3f, Parent.transform.position.y, Parent.transform.position.z);
    }
}
