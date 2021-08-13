namespace ShoppingList2021.Database
{
    public interface IDbServiceFactory
    {
        IDbService CreateNew();
    }
}
