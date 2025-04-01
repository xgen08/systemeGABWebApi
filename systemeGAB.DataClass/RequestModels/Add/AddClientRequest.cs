namespace systemeGAB.DataClass.RequestModels.Add
{
    public class AddClientRequest
    {
        [Required]
        public string? nomClient { get; set; }
        [Required]
        public string? prenomClient { get; set; }
        [Required]
        public string? adresseClient { get; set; }
        [Required]
        public string? emailClient { get; set; }
        [Required]
        public string? telephoneClient { get; set; }
    }
}
