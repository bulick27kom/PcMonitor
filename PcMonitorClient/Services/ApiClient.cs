using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PcMonitorClient.Services
{
    /// <summary>
    /// Класс для отправки данных на веб-сервис.
    /// </summary>
    public class ApiClient
    {
        private readonly string _baseAddress;
        private readonly string _controller;
        private readonly HttpClient _httpClient;
        private readonly int _timeout;

        public ApiClient(string baseAddress, string controller, int timeout)
        {
            _baseAddress = baseAddress;
            _controller = controller;
            _timeout = timeout;

            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(_timeout) // Используем таймаут из конфигурации
            };
        }

        /// <summary>
        /// Отправляет данные на веб-сервис.
        /// </summary>
        /// <param name="data">Данные в формате JSON.</param>
        public async Task SendDataAsync(string data)
        {
            string url = $"http://{_baseAddress}/{_controller}";
            using (HttpContent content = new StringContent(data, Encoding.UTF8, "application/json"))
            {
                try
                {
                    Logger.Info($"Отправка данных на {url}... (таймаут: {_timeout} сек)");

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
}
