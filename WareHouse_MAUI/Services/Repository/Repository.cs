

using WareHouse_MAUI.Models;
using System.Text;
using System.Text.Json;


namespace WareHouse_MAUI.Services.Repository
{
    internal class Repository : IRepository
    {


        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;


        public Repository()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }


        public async Task<bool> Ping()
        {

            try
            {
                HttpResponseMessage response = await _client.GetAsync(new Uri(Constants.ServerPaths.PING_URL));

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ping http - " + e.Message);
                throw new Exception("Server Connsction Error");
            }

            return false;
        }

        public async Task<bool> DeleteAsync<T>(int id) where T : class, new()
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(GetPath<T>() + $"/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error delete http - " + e.Message);
                throw new Exception("Server Connsction Error");
            }

            return false;
        }

        public async Task<List<T>> GetDataAsync<T>() where T : class, new()
        {
            var Items = new List<T>();

            Uri uri = new Uri(uriString: string.Format(GetPath<T>(), string.Empty));

            try
            {

                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = System.Text.Json.JsonSerializer.Deserialize<List<T>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"ERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<List<T>> GetDataAsync<T>(int id) where T : class, new()
        {
            var Items = new List<T>();

            Uri uri = new Uri(uriString: string.Format(GetPath<T>() + $"/{id}", string.Empty));
            try
            {

                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = System.Text.Json.JsonSerializer.Deserialize<List<T>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"ERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<T> GetDataAsync<T>(string from, string val) where T : class, new()
        {
            string str = from + " " + val;
            T login = null;

            try
            {
                StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(str),
                                                              Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(GetPath<T>() + $"/auth", content);

                if (response.IsSuccessStatusCode)
                {
                    string cont = await response.Content.ReadAsStringAsync();
                    login = new T();
                    login = System.Text.Json.JsonSerializer.Deserialize<T>(cont, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"ERROR {0}", ex.Message);
                //throw new Exception("Server Connection Error");
            }

            return login;
        }

        public async Task<bool> InsertAsync<T>(T item) where T : class, new()
        {
            try
            {
                StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(item),
                                                             Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(GetPath<T>(), content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Insert http - " + e.Message);
                throw new Exception("Server Connection Error");
            }

            return false;
        }

        public async Task<bool> UpdateDataAsync<T>(int id, T item) where T : class, new()
        {
            try
            {
                StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(item),
                                                             Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PutAsync(GetPath<T>() + $"/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error update http - " + e.Message);
                throw new Exception("Server Connection Error");
            }

            return false;
        }

        private string GetPath<T>() where T : class, new()
        {
            string path = null;

            if (typeof(T) == typeof(Product))
            {
                path = Constants.ServerPaths.PRODUCTS_URL;
            }
            else if (typeof(T) == typeof(ClientModel))
            {
                path = Constants.ServerPaths.CLIENTS_GET_URL;
            }
            else if (typeof(T) == typeof(Employee))
            {
                path = Constants.ServerPaths.EMPLOYEES_GET_URL;
            }
            else if (typeof(T) == typeof(Categories))
            {
                path = Constants.ServerPaths.CATEGORY_URL;
            }
            else if (typeof(T) == typeof(ProductType))
            {
                path = Constants.ServerPaths.PRODUCT_TYPE_URL;
            }
            else if (typeof(T) == typeof(Shipper))
            {
                path = Constants.ServerPaths.SHIPPER_URL;
            }
            else if (typeof(T) == typeof(ArrivalOfGoods))
            {
                path = Constants.ServerPaths.ARRIVAL_URL;
            }
            else if (typeof(T) == typeof(TransferOfGoods))
            {
                path = Constants.ServerPaths.TRANSFER_URL;
            }

            return path;
        }
    }
}
