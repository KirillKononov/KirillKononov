using Microsoft.Extensions.Logging;

namespace BLL.Infrastructure
{
    public static class Validator
    {
        public static void IdValidation(int? id, ILogger logger)
        {
            if (id == null)
            {
                logger.LogError("The id hasn't entered");
                throw new ValidationException("The id hasn't entered");
            }
        }

        public static void EntityValidation<T>(T entity, ILogger logger, string type) where T : class
        {
            if (entity == null)
            {
                logger.LogError($"There is no {type} in database with this id");
                throw new ValidationException($"There is no {type} in database with this id");
            }
        }
    }
}
