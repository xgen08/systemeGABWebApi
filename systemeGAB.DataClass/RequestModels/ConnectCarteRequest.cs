namespace systemeGAB.DataClass.RequestModels
{
    public class ConnectCarteRequest
    {
        [Required]
        public string? numeroCarte { get; set; }
        [Required]
        public string? codePin { get; set; }
    }
}
