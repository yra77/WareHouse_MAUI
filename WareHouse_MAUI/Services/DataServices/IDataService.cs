

namespace WareHouse_MAUI.Services.DataServices
{
    internal interface IDataService
    {
        Task<List<T>> GetDataAsync<T>() where T : class, new();
        Task<List<T>> GetDataAsync<T>(int id) where T : class, new();
        Task<bool> InsertUpdateAsync<T>(int id, T item) where T : class, new();
        Task<bool> DeleteAsync<T>(int id) where T : class, new();
    }
}
