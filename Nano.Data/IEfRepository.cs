namespace Nano.Data
{
    public interface IEfRepository
    {
        NanoDbContext GetDbContext();
    }
}