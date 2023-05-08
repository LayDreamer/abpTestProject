using LocalTest.Books;
using LocalTest.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace LocalTest.Web.Pages.Books
{
    public class FileModalModel : LocalTestPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateBookDto Book { get; set; }

        [BindProperty]
        public UpLoadFileDto UploadFileDto { get; set; }

        private readonly IBookAppService _bookAppService;
        private readonly IFileAppService _fileAppService;

        public FileModalModel(IBookAppService bookAppService, IFileAppService fileAppService)
        {
            _bookAppService = bookAppService;
            _fileAppService = fileAppService;
        }

        public async Task OnGetAsync()
        {
            var bookDto = await _bookAppService.GetAsync(Id);
            Book = ObjectMapper.Map<BookDto, CreateUpdateBookDto>(bookDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            using (var memoryStream = new MemoryStream())
            {
                UploadFileDto.Name = UploadFileDto.File.FileName;

                await UploadFileDto.File.CopyToAsync(memoryStream);
                await _fileAppService.SaveFileAsync(
                    new SaveFileInputDto
                    {
                        Name = UploadFileDto.Name,
                        Content = memoryStream.ToArray()
                    }
                );

                Book.Name = UploadFileDto.Name;
                await _bookAppService.UpdateAsync(Id, Book);
            }
            return NoContent();
            //await _bookAppService.UpdateAsync(Id, Book);
            //return NoContent();
        }
    }
}
