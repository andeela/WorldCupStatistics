using DAL.Settings;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace DAL
{
    public class Utilities
    {
        public const char DELIMITER = ':';

        public static async Task<T> ReadJsonFileAsync<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found");
            }

            string json = await File.ReadAllTextAsync(path);
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new InvalidOperationException("File contains no JSON data");
            }

            T? result = JsonConvert.DeserializeObject<T>(json, JsonSettings.JsonSerializerSettings);
            if (result == null)
            {
                throw new InvalidOperationException("Failed to deserialize JSON data.");
            }

            return result;
        }

        public static async Task<T> GetJsonObjFromUrlAsync<T>(string url)
        {
            using var httpClient = new HttpClient();

            try
            {
                var response = await httpClient.GetAsync(url);
                // success: status code 200-299
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                T? result = JsonConvert.DeserializeObject<T>(json, JsonSettings.JsonSerializerSettings);

                if (result == null)
                {
                    throw new InvalidOperationException("Failed to deserialize JSON data.");
                }

                return result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HttpRequestException: {ex.Message}");
                throw new Exception("An error occurred while making the HTTP request.", ex);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JsonException: {ex.Message}");
                throw new Exception("An error occured while deserializing the JSON response.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw new Exception("An unexpected error occured.", ex);
            }

        }

        private static readonly Dictionary<string, Model.Enums.Language> LanguageComboBoxMap = new()
        {
            { "Croatian", Model.Enums.Language.CROATIAN },
            { "English", Model.Enums.Language.ENGLISH }
        };

        public static AppSettings SetLanguageByComboBoxSelection(AppSettings settings, string selectedLanguage)
        {
            if (LanguageComboBoxMap.TryGetValue(selectedLanguage, out var language))
            {
                settings.Language = language;
            }
            else
            {
                throw new ArgumentException($"Invalid language selection: {selectedLanguage}", nameof(selectedLanguage));
            }

            return settings;
        }

        public static string GetComboBoxSelectionFromSettings(AppSettings settings)
        {
            foreach (var pair in LanguageComboBoxMap)
            {
                if (pair.Value == settings.Language)
                {
                    return pair.Key;
                }
            }

            throw new InvalidOperationException($"Invalid language setting: {settings.Language}");
        }


        public static TEnum ParseEnumValue<TEnum>(string line) where TEnum : struct
        {
            var parts = line.Split(DELIMITER);
            if (parts.Length != 2)
            {
                throw new FormatException($"Invalid setting format: {line}");
            }

            if (!Enum.TryParse<TEnum>(parts[1], true, out var value))
            {
                throw new FormatException($"Invalid enum value: {parts[1]}");
            }

            return value;
        }

        public static string GetValueFromLine(string line)
        {
            int delimiterIndex = line.IndexOf(DELIMITER);
            if (delimiterIndex == -1 || delimiterIndex == line.Length - 1)
            {
                throw new ArgumentException("The line does not contain a valid delimiter or value.");
            }

            return line.Substring(delimiterIndex + 1).Trim();
        }

        public static string[] GetValuesFromLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                throw new ArgumentException("The line is null or empty.");
            }

            int delimiterIndex = line.IndexOf(Environment.NewLine);
            if (delimiterIndex == -1 || delimiterIndex == line.Length - 1)
            {
                throw new ArgumentException($"The line '{line}' does not contain a valid delimiter or value.");
            }

            string valuesPart = line.Substring(delimiterIndex + 1).Trim();
            return valuesPart.Split(Environment.NewLine);
        }

    }
}
