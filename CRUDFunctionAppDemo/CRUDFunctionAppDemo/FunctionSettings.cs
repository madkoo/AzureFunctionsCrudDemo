namespace CRUDFunctionAppDemo
{
    public static class FunctionsSettings
    {
        public const string PartitionKey = "GABC";
        public const string TableName = "marvelheros";
        public const string AzureWebJobsStorage = "AzureWebJobsStorage";
        public const string RouteBase = "hero";
        public const string RouteWithId = RouteBase + "/{id}";


    }
}
