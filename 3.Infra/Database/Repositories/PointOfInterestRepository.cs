using _1.Domain;
using _2.Core;
using _3.Infra.Database;

namespace _3.Infra;
public class PointOfInterestRepository: BaseRepository<PointOfInterest>, IPointOfInterestRepository
{
    public PointOfInterestRepository(DataContext context) : base(context)
    {
    }
}