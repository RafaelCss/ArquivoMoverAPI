using api_documentos.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace api_documentos.Controllers;

[ApiController]
[Route("[controller]")]
public class GeradorPdfsController : ControllerBase
{
    private readonly ILogger<GeradorPdfsController> _logger;

    public GeradorPdfsController(ILogger<GeradorPdfsController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "PDFs")]
    public async Task<IActionResult> PostPdfsAsync()
    {

        try
        {
            var file = Request.Form.Files[0];
            if (file == null || file.Length == 0)
                return BadRequest("Arquivo não encontrado");

            var filePath = Path.Combine(Directory.GetCurrentDirectory() , "arquivos" , file.FileName);

            using (var stream = new FileStream(filePath , FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("Arquivo enviado com sucesso");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex , "Erro ao enviar o arquivo PDF");
            return StatusCode(500 , "Erro interno do servidor");
        }
    }


    [HttpGet("PDFs/{fileName}")]
    public IActionResult DownloadPdf(string fileName)
    {
        try
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory() , "arquivos" , fileName);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);

            var contentType = "application/pdf";
            var contentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };

            Response.Headers.Add("Content-Disposition" , contentDisposition.ToString());
            return File(fileBytes , contentType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex , "Erro ao fornecer o arquivo PDF");
            return StatusCode(500 , "Erro interno do servidor");
        }
    }

}

    
