using System.IO;
using LojaJkMisterG.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LojaJkMisterG.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RolesTypes.Admin + "," + RolesTypes.Vendedor)]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminImagensController(IWebHostEnvironment hostingEnvironment, IOptions<ConfigurationImagens> myConfiguration)
        {
            _hostingEnvironment = hostingEnvironment;
            _myConfig = myConfiguration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }

            if (files.Count > 10)
            {
                ViewData["Erro"] = "Error: Quantidade de arquivos excedeu o limite";
                return View(ViewData);
            }

            long size = files.Sum(f => f.Length);

            var filePathsName = new List<string>();

            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);

            foreach (var formFile in files)
            {
                if (formFile.FileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                    formFile.FileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                    formFile.FileName.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) ||
                    formFile.FileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                {
                    var fileNameWithPath = Path.Combine(filePath, formFile.FileName);
                    filePathsName.Add(fileNameWithPath);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            ViewData["Resultado"] = $"{filePathsName.Count} arquivos foram enviados ao servidor, com tamanho total de {size} bytes";
            ViewBag.Arquivos = filePathsName;

            return View(ViewData);
        }

        public IActionResult GetImagens()
        {
            var model = new FileManagerModel();
            var userImagesPath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);

            if (!Directory.Exists(userImagesPath))
            {
                ViewData["Erro"] = $"Pasta {userImagesPath} não encontrada";
                return View(model);
            }

            var files = new DirectoryInfo(userImagesPath).GetFiles();

            model.PathImagesProduto = _myConfig.NomePastaImagensProdutos;

            if (files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {userImagesPath}";
            }

            model.Files = files;

            return View(model);
        }

        public IActionResult Deletefile(string fname)
        {
            var userImagesPath = Path.Combine(_hostingEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);
            var fileNameWithPath = Path.Combine(userImagesPath, fname);

            if (System.IO.File.Exists(fileNameWithPath))
            {
                System.IO.File.Delete(fileNameWithPath);
                ViewData["Deletado"] = $"Arquivo {fname} deletado com sucesso";
            }

            return View("Index");
        }
    }

}
