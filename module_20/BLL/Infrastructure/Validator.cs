using Microsoft.Extensions.Logging;

namespace BLL.Infrastructure
{
    public class Validator
    {
        public void IdValidation(int? id, ILogger logger)
        {
            if (id == null)
            {
                logger.LogWarning("The id hasn't entered");
                throw new ValidationException("The id hasn't entered");
            }
        }

        public void EntityValidation<T>(T entity, ILogger logger, string type) where T : class
        {
            if (entity == null)
            {
                logger.LogWarning($"There is no {type} in database with this id");
                throw new ValidationException($"There is no {type} in database with this id");
            }
        }
    }
}
