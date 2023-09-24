namespace api_documentos.Model
{
    public class GetPdfsRequestModel
    {
        public DateOnly? Date { get; set; }

        public required string NomeArquivo { get; set; }
    }
}