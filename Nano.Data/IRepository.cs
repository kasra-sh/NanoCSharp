namespace Nano.Data
{
    public interface IRepository
    {
        NanoDbContext GetDbContext();
    }
}