using UnityEngine;

public class DisableAnimator : MonoBehaviour
{
    public void TurnOffAnimator()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
