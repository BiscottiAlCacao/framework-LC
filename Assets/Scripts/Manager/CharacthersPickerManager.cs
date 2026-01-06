using UnityEngine;

public class CharacthersPickerManager : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("Alice", 0);
        PlayerPrefs.SetInt("MadHatter", 0);
    }

    public void PickAlice()
    {
        PlayerPrefs.SetInt("Alice", 1);
    }

    public void PickMadHut()
    {
        PlayerPrefs.SetInt("MadHatter", 1);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Alice", 0);
        PlayerPrefs.SetInt("MadHatter", 0);
    }
}
