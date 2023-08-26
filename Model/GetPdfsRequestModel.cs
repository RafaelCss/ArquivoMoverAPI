namespace api_documentos.Model
{
    public class GetPdfsRequestModel
    {
        public DateOnly? Date { get; set; }

        public string NomeArquivo { get; set; }
    }
}