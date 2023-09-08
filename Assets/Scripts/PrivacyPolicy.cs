using UnityEngine;

public class PrivacyPolicy : MonoBehaviour
{
    [SerializeField]
    private string policyUrl;

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL(policyUrl);
    }
}