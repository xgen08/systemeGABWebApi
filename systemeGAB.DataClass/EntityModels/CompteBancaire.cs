namespace systemeGAB.DataClass.EntityModels
{
    public class CompteBancaire
    {
        [Key]
        public int idCompte { get; set; }
        public int idClient { get; set; }
        public string? numeroCompte { get; set; }
        public double? solde { get; set; }
        public string? typeCompte { get; set; } //Type de compte (courant, épargne, etc.)
        public DateTime? dateOuverture { get; set; }
    }
}
