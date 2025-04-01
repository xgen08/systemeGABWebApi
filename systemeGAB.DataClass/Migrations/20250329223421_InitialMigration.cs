#nullable disable

namespace systemeGAB.DataClass.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "carteBancaire",
                columns: table => new
                {
                    idCarte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCompte = table.Column<int>(type: "int", nullable: false),
                    numeroCarte = table.Column<int>(type: "int", nullable: false),
                    dateExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    codePin = table.Column<int>(type: "int", nullable: false),
                    statut = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carteBancaire", x => x.idCarte);
                });

            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    idClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prenomClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adresseClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emailClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telephoneClient = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.idClient);
                });

            migrationBuilder.CreateTable(
                name: "compteBancaire",
                columns: table => new
                {
                    idCompte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idClient = table.Column<int>(type: "int", nullable: false),
                    numeroCompte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    solde = table.Column<double>(type: "float", nullable: true),
                    typeCompte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateOuverture = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compteBancaire", x => x.idCompte);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    idTransaction = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCompte = table.Column<int>(type: "int", nullable: false),
                    typeTransaction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    montant = table.Column<double>(type: "float", nullable: true),
                    dateHeure = table.Column<DateTime>(type: "datetime2", nullable: true),
                    statut = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.idTransaction);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carteBancaire");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropTable(
                name: "compteBancaire");

            migrationBuilder.DropTable(
                name: "gab");

            migrationBuilder.DropTable(
                name: "transaction");
        }
    }
}
