using UnityEngine;

public class HapticFeedback : MonoBehaviour
{
    public static HapticFeedback instance;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
        else
            Destroy(gameObject);
    }
    public void HapticActivate(bool right, bool left, float duration)
    {
       
    }
}
