using System.Diagnostics.CodeAnalysis;
using SASS.Chassis.Exceptions;

namespace SASS.Chassis.Utilities.Guards;

public static class GuardAgainstNotFoundExtensions
{
    extension(Guard guard)
    {
        /// <summary>
        /// Validates that the provided value is not null. If null, throws a <see cref="NotFoundException" />
        /// for the specified <paramref name="id" />.
        /// </summary>
        /// <typeparam name="T">The type of the value being checked.</typeparam>
        /// <param name="exists">If it exists => do nothing</param>
        /// <param name="id">The identifier associated with the entity being checked.</param>
        /// <exception cref="NotFoundException">
        /// Thrown when the value is null,
        /// indicating that the entity with the specified <paramref name="id" /> was not found.
        /// </exception>
        public void NotFound<T>(bool exists, string id)
        {
            if (exists)
            {
                return;
            }
            
            throw new NotFoundException($"Id: {id} not found");
        }

        /// <summary>
        /// Validates that the provided value is not null. If null, throws a <see cref="NotFoundException" />
        /// for the specified <paramref name="id" />.
        /// </summary>
        /// <typeparam name="T">The type of the value being checked.</typeparam>
        /// <param name="value">The value to check for null.</param>
        /// <param name="id">The identifier associated with the entity being checked.</param>
        /// <exception cref="NotFoundException">
        /// Thrown when the value is null,
        /// indicating that the entity with the specified <paramref name="id" /> was not found.
        /// </exception>
        public void NotFound<T>([NotNull] T? value, Guid id)
        {
            if (value is not null)
            {
                return;
            }

            throw new NotFoundException($"Id: {id} not found");
        }
    }
}
