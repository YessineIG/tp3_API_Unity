using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; // Assurez-vous d'utiliser TextMeshPro pour les affichages textuels

public class WeatherAPI : MonoBehaviour
{
    public TextMeshProUGUI weatherText; // Zone de texte pour afficher la m�t�o

    private string url = "https://api.open-meteo.com/v1/forecast?latitude=45.4215&longitude=-75.6972&current_weather=true"; // API pour la m�t�o de Montr�al

    void Start()
    {
        StartCoroutine(GetWeatherData());
    }

    IEnumerator GetWeatherData()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest(); // Attendre la r�ponse de l'API

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erreur lors de la r�cup�ration des donn�es m�t�o");
        }
        else
        {
            string json = request.downloadHandler.text;
            WeatherResponse weatherResponse = JsonUtility.FromJson<WeatherResponse>(json); // Convertir JSON en objet
            DisplayWeather(weatherResponse);
        }
    }

    void DisplayWeather(WeatherResponse weather)
    {
        // Afficher les donn�es dans l'interface utilisateur
        weatherText.text = "Temp�rature : " + weather.current_weather.temperature + "�C\n" +
                           "Humidit� : " + weather.current_weather.humidity + "%";
    }

    [System.Serializable]
    public class WeatherResponse
    {
        public CurrentWeather current_weather;
    }

    [System.Serializable]
    public class CurrentWeather
    {
        public float temperature;
        public int humidity;
    }
}
