namespace Entities.Exceptions
{
    public class PriceOutofRangeBadRequestException: BadRequestException
    {
        public PriceOutofRangeBadRequestException(): base("Max. price must be less than 1000 and min. price must be greater than 10.")
        {
            
        }
    }
}
