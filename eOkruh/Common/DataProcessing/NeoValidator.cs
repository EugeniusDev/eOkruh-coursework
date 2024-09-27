using Neo4j.Driver;

namespace eOkruh.Common.DataProcessing
{
    public static class NeoValidator
    {
        public static async Task<bool> NodeExists(string nodeType, string nodeKeyProperty, string nodeKeyPropValue)
        {
            using var session = NeoAccessor.driver.AsyncSession();
            var query = $@"
                MATCH (n:{nodeType} {{{nodeKeyProperty}: $propertyValue}})
                RETURN count(n) > 0 AS nodeExists";

            string trimmedValue = nodeKeyProperty.Trim();
            var result = await session.ExecuteReadAsync(async tx =>
            {
                var resultCursor = await tx.RunAsync(query, new { trimmedValue });
                var record = await resultCursor.SingleAsync();

                if (record != null)
                {
                    return record["nodeExists"].As<bool>();
                }

                return false;
            });

            return result;
        }
    }
}
