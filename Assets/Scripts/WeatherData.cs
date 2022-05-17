using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeatherData : MonoBehaviour
{
    [Serializable]
    public struct WeatherDetails
    {
        public int id;
        public string main;
    }
    [Serializable]
    public struct WeatherAmbient
    {
        public float temp;
        public float feels_like;
    }
    [Serializable]
    public struct WeatherWind
    {
        public float speed;
        public float deg;
    }
    [Serializable]
    public struct WeatherInfo
    {
        public int id;
        public string name;
        public string message;
        public int cod;
        public List<WeatherDetails> weather;
        public WeatherAmbient main;
        public WeatherWind wind;
    }

    public string city;

    private GameObject weatherInfos;

    private const string apiKey = "9b937757debd5c742fee202a4f67766a";
    private float timer = 3;


    void Start()
    {
        weatherInfos = GameObject.Find("WeatherInfos");
        StartCoroutine(GetWeather());
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 3;
            StartCoroutine(GetWeather());
        }
    }

    IEnumerator GetWeather()
    {
        // If no city is entered d'ont procees
        if (city == "")
            yield break;
        
        using (UnityWebRequest req = UnityWebRequest.Get(String.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", city, apiKey)))
        {
            // Send the request
            yield return req.Send();

            // Wait for the request to finish
            while (!req.isDone)
                yield return null;
            
            // Fetch the values within the request response
            byte[] result = req.downloadHandler.data;

            // Parse the informations in order to send them back
            string weatherJSON = System.Text.Encoding.Default.GetString(result);
            WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(weatherJSON);

            // Check if the the city used exists using the response's code and message
            if (info.cod == 404)
            {
                if (info.message == "city not found")
                    yield break;
            }

            // Set the UI text with the values obtained, in order: name, temperature (Kelvin/Celsius), temperature felt (Kelvin/Celsius), wind speed (K/h), wind angle
            weatherInfos.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Weather for " + info.name + " : " + info.weather[0].main;
            weatherInfos.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Temperature: " + info.main.temp.ToString() + " K / " + (info.main.temp - 273.15).ToString("F2") + " °C";
            weatherInfos.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Felt Temperature: " + info.main.feels_like.ToString() + " K / " + (info.main.feels_like - 273.15).ToString("F2") + " °C";;
            weatherInfos.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Wind speed: " + (info.wind.speed * 3.6).ToString("F2") + " K/h";
            weatherInfos.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Wind angle: " + info.wind.deg.ToString();
        }
    }
}
