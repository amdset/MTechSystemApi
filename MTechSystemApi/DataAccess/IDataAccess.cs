using Microsoft.AspNetCore.Mvc;

namespace MTechSystemApi.DataAccess
{
    public interface IDataAccess
    {
        Task<List<T>> LoadData<T>(string sql, object parameters, string connMysql);

        Task<T> LoadSigleRaw<T>(string sql, object parameters, string connMysql);

        Task<int> SaveData<T>(string sql, object parameters, string connMysql);
    }
}
