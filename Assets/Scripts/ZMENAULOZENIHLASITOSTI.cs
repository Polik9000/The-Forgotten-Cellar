using UnityEngine;
using UnityEngine.UI;

public class ZMENAULOZENIHLASITOSTI : MonoBehaviour
{
    public Slider volumeSlider;       // Odkaz na slider
    public AudioSource audioSource;  // Odkaz na AudioSource komponentu
    private const string VolumeKey = "Volume"; // Klíč pro ukládání hlasitosti do PlayerPrefs

    private void Start()
    {
        // Načtení uložené hlasitosti nebo výchozí hodnoty
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.5f); // Defaultní hlasitost je 0.5
        audioSource.volume = savedVolume; // Nastavení hlasitosti AudioSource
        volumeSlider.value = savedVolume; // Nastavení hodnoty slideru

        // Přidání listeneru pro změnu hlasitosti
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        // Změna hlasitosti a uložení nové hodnoty
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(VolumeKey, volume);
    }
}
