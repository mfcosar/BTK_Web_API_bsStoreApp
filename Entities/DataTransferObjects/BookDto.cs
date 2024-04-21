namespace Entities.DataTransferObjects
{
    //[Serializable] - açık prop ifadeleri varsa gerek kalmaz
    public record BookDto
    {
        public int Id { get; init; }
        public String Title { get; init; }
        public decimal Price { get; init; }

        public int CategoryId { get; init; }

    } //(int Id, String Title, decimal Price);
}
