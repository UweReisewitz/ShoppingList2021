namespace ShoppingList2021.Database.Types
{
    public interface IDbServiceFactory
    {
        IDbService CreateNew();
    }
}
