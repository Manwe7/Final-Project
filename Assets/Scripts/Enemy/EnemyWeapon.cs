using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] GameObject Parent=null, bullet=null, barrel=null;
    
    private float _offset = 270;
    private GameObject Player;

    private void Start()
    {
        try
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        catch (System.Exception)
        {
            //
        }
    }
    
    private void Update()
    {
        if(Player != null)
        {
            Vector3 PlayerPos = Player.transform.position;

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

    public void Fire()
    {
        Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
    }

    private void RightSide()
    {
        //Rotate and change position to right
        Vector3 dir = Player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0f, rotZ + _offset);  

        transform.position = new Vector3(Parent.transform.position.x + 0.3f, Parent.transform.position.y, Parent.transform.position.z);
    }

    private void LeftSide()
    {
        //Rotate and change position to left
        Vector3 dir = Player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(-180f, 0f, -rotZ + _offset);  

        transform.position = new Vector3(Parent.transform.position.x -0.3f, Parent.transform.transform.position.y, Parent.transform.position.z);
    }
}
