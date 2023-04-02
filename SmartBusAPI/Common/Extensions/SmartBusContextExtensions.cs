namespace SmartBusAPI.Common.Extensions
{
    public static class SmartBusContextExtensions
    {
        public static void ResetIdentityValue<TEntity>(this SmartBusContext context, string tableName) where TEntity : class
        {
            // Get the maximum ID value from the specified table
            int maxId = context.Set<TEntity>().AsEnumerable().Max(e => (int)e.GetType().GetProperty("ID").GetValue(e));

            // Create a new SqlCommand object
            DbCommand command = context.Database.GetDbConnection().CreateCommand();

            // Set the CommandText property to the DBCC CHECKIDENT command
            command.CommandText = $"DBCC CHECKIDENT ('{tableName}', RESEED, {maxId})";

            // Open the database connection
            context.Database.OpenConnection();
            try
            {
                // Execute the DBCC CHECKIDENT command
                command.ExecuteNonQuery();
            }
            finally
            {
                // Close the database connection
                context.Database.CloseConnection();
            }
        }
    }
}