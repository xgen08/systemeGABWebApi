namespace systemeGAB.DataClass.EntityModels
{
    public class Transaction
    {
        [Key]
        public int idTransaction { get; set; }
        public int idCompte { get; set; }
        public string? typeTransaction { get; set; } //Retrait, Dépôt, Consultation solde
        public double? montant { get; set; }
        public DateTime? dateHeure { get; set; }
        public string? statut { get; set; } //Réussi/Échoué
    }
}
