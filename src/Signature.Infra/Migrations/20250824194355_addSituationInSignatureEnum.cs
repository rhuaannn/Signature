using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Signature.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addSituationInSignatureEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Remove o valor padrão antigo da coluna para evitar o erro de conversão.
            migrationBuilder.Sql("ALTER TABLE \"Signatures\" ALTER COLUMN \"situation\" DROP DEFAULT;");

            // 2. Renomeia a coluna.
            migrationBuilder.RenameColumn(
                name: "situation",
                table: "Signatures",
                newName: "Situation");

            // 3. Altera o tipo da coluna de `boolean` para `integer`.
            // A cláusula USING converte explicitamente true para 1 e false para 0.
            migrationBuilder.Sql(@"
                ALTER TABLE ""Signatures""
                ALTER COLUMN ""Situation"" TYPE integer
                USING CASE
                    WHEN ""Situation"" = true THEN 1
                    ELSE 0
                END;
            ");

            // 4. Define o novo valor padrão para a coluna, agora do tipo `integer`.
            migrationBuilder.Sql("ALTER TABLE \"Signatures\" ALTER COLUMN \"Situation\" SET DEFAULT 0;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Os passos inversos para reverter a migração, caso necessário.
            // 1. Remove o valor padrão do tipo `integer`.
            migrationBuilder.Sql("ALTER TABLE \"Signatures\" ALTER COLUMN \"Situation\" DROP DEFAULT;");

            // 2. Altera o tipo da coluna de volta para `boolean`.
            migrationBuilder.Sql(@"
                ALTER TABLE ""Signatures""
                ALTER COLUMN ""Situation"" TYPE boolean
                USING CASE
                    WHEN ""Situation"" = 1 THEN true
                    ELSE false
                END;
            ");

            // 3. Adiciona o valor padrão original, agora para o tipo `boolean`.
            migrationBuilder.Sql("ALTER TABLE \"Signatures\" ALTER COLUMN \"Situation\" SET DEFAULT false;");

            // 4. Renomeia a coluna de volta para o nome original.
            migrationBuilder.RenameColumn(
                name: "Situation",
                table: "Signatures",
                newName: "situation");
        }
    }
}