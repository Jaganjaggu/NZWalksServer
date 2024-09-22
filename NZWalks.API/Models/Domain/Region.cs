namespace NZWalks.API.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        //here only regionurl can have null values other cannot
        public string? RegionImageUrl { get; set; }


    }
}
