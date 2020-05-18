using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{    
    [SerializeField] GameObject Parent = null, bullet = null, barrel = null;
    [SerializeField] Joystick _joystick = null;
    [SerializeField] RectTransform _joystickHandle = null;
    private float _offset = 180;
       
    private void Update()
    {
        //Shoot when LMS is clicked
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
        }

        //Find mouse position
        Vector3 mouse = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        
        //Change position depending on mouse position
        if(_joystickHandle.anchoredPosition.x < 0)//(mouse.x < playerScreenPoint.x) 
        {
            LeftSide();
        } 
        else
        {
            RightSide();
        }
    }

    private void RightSide()
    {
        //Rotate and change position to right
        /*Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);*/

        Vector3 direction = Vector3.up * _joystick.Vertical + Vector3.right * _joystick.Horizontal;
        float rotZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, -rotZ);

        transform.position = new Vector3(Parent.transform.position.x + 0.3f, Parent.transform.position.y, Parent.transform.position.z);
    }

    private void LeftSide()
    {
        //Rotate and change position to left
        /*Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(-180f, 0f, -rotZ + offset);  */

        Vector3 direction = Vector3.up * _joystick.Vertical + Vector3.right * _joystick.Horizontal;
        float rotZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(-180f, 0f, rotZ + _offset);

        transform.position = new Vector3(Parent.transform.position.x -0.3f, Parent.transform.transform.position.y, Parent.transform.position.z);
    }
}
