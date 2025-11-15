namespace MyMvcShopDb.Core.DTO
{
    public class ManufacturerDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Country { get; set; }
    }
}