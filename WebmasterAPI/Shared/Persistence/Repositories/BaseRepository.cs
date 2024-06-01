using WebmasterAPI.Shared.Persistence.Contexts;

namespace WebmasterAPI.Shared.Persistence.Repositories;

public class BaseRepository
{
    protected readonly AppDbContext _Context;

    public BaseRepository(AppDbContext context)
    {
        _Context = context;
    }
}