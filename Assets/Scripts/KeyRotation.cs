using UnityEngine;

public class KeyRotation : MonoBehaviour
{
    public float speed = 100.0f;  // rotation speed

    void Update()
    {
        if(this.CompareTag("keycard")){
            transform.Rotate(speed * Time.deltaTime, 0, 0);
        }else{
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }

    }
}