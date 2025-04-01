namespace systemeGAB.DataClass.EntityModels
{
    public class CarteBancaire
    {
        [Key]
        public int idCarte { get; set; }
        public int idCompte { get; set; }
        public string? numeroCarte { get; set; }
        public DateTime? dateExpiration { get; set; }
        public string? codePin { get; set; }
        public string? statut { get; set; } //Actif/Inactif
    }
}
