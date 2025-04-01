namespace systemeGAB.DataClass.RequestModels.Update
{
    public class UpdateClientRequest
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
