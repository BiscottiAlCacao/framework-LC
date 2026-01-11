using UnityEngine;

public class CharacthersPickerManager : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("Freddy", 0);
        PlayerPrefs.SetInt("Chica", 0);
    }

    public void PickFreddy()
    {
        PlayerPrefs.SetInt("Freddy", 1);
    }

    public void PickChica()
    {
        PlayerPrefs.SetInt("Chica", 1);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Freddy", 0);
        PlayerPrefs.SetInt("Chica", 0);
    }
}
