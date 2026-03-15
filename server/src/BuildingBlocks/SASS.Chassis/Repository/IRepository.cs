using SASS.SharedKernel.SeedWork;

namespace SASS.Chassis.Repository;

public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
