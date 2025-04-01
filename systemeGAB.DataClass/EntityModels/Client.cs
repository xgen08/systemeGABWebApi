namespace systemeGAB.DataClass.EntityModels
{
    public class Client
    {
        [Key]
        public int idClient { get; set; }
        public string? nomClient { get; set; }
        public string? prenomClient { get; set; }
        public string? adresseClient { get; set; }
        public string? emailClient { get; set; }
        public string? telephoneClient { get; set; }
    }
}
