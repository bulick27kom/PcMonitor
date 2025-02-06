using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PcMonitorClient.Services
{
    /// <summary>
    /// Класс для отправки данных на веб-сервис.
    /// </summary>
    public class WebServiceSender
    {
        private readonly string _baseAddress;
        private readonly string _controller;
        private readonly HttpClient _httpClient;

        public WebServiceSender(ConfigReader config)
        {
            _baseAddress = config.GetBaseUrl();
            _controller = config.ControllerName;

            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(config.RequestTimeout)
            };
        }

        /// <summary>
        /// Отправляет JSON-данные на веб-сервис.
        /// </summary>
        /// <param name="data">Объект с данными.</param>
        public async Task SendToWebServiceAsync(object data)
        {
            string url = $"{_baseAddress}/{_controller}";

            try
            {
                string json = JsonConvert.SerializeObject(data);
                using (HttpContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    Logger.Info($"Отправка данных на {url}...");

                    HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Logger.Info("Данные успешно отправлены.");
                    }
                    else
                    {
                        Logger.Error($"Ошибка отправки данных: {response.StatusCode}");
                    }
                }
            }
            catch (TaskCanceledException)
            {
                Logger.Error("Ошибка: Таймаут ожидания ответа от сервера.");
            }
            catch (Exception ex)
            {
                Logger.Error($"Исключение при отправке данных: {ex.Message}");
            }
        }
    }
}
