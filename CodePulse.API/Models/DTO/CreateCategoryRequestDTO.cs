namespace CodePulse.API.Models.DTO
{
    public class CreateCategoryRequestDTO
    {
        //Id will be set by SQL, so we don't need to pass in request
        public string Name { get; set; }
        public string UrlHandle { get; set; }
    }
}
