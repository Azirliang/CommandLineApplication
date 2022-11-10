using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migration.CLI.Data
{
    public class DapperDbAdapter
    {
        private readonly Func<IDbConnection> _dbConn;

        public DapperDbAdapter(Func<IDbConnection> dbConn) => _dbConn = dbConn;

        public Task<int> ExecuteAsync(string sql, object param) => ExecuteSqlAsync(db => db.ExecuteAsync(sql, param));

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param) => ExecuteSqlAsync(db => db.QueryAsync<T>(sql, param));

        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param) => ExecuteSqlAsync(db => db.QueryFirstOrDefaultAsync<T>(sql, param));

        public Task<T> ExecuteScalarAsync<T>(string sql, object param) => ExecuteSqlAsync(db => db.ExecuteScalarAsync<T>(sql, param));

        private async Task<T> ExecuteSqlAsync<T>(Func<IDbConnection, Task<T>> action)
        {
            using var conn = _dbConn();
            conn.Open();
            return await action(conn);
        }
    }
}
