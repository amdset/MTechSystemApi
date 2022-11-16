using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace MTechSystemApi.DataAccess
{
    public class MysqlDataAccess : IDataAccess
    {
        public async Task<List<T>> LoadData<T>(string sql, object parameters, string connMysql)
        {
            using(IDbConnection connection = new MySqlConnection(connMysql))
            {
                var result = await connection.QueryAsync<T>(sql, parameters);
                return result.ToList();
            }
        }

        public async Task<int> SaveData<T>(string sql, object parameters, string connMysql)
        {
           using(IDbConnection connection =new MySqlConnection(connMysql))
            {
                var result = await connection.ExecuteAsync(sql, parameters);
                return result;
            }
        }
    }
}
