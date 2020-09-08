using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileUpload.Web.Data;
using FileUpload.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FileUpload.Web.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FileUploadController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region load all files
        public async Task<IActionResult> IndexAsync()
        {
            var fileuploadViewModel = await LoadAllFiles();
            ViewBag.Message = TempData["Message"];
            return View(fileuploadViewModel);
        }
        private async Task<FileUploadViewModel> LoadAllFiles()
        {
            var viewModel = new FileUploadViewModel();
            viewModel.FileOnDatabaseModel = await _context.FileOnDatabaseModels.ToListAsync();
            viewModel.FilesOnFileSystem = await _context.FileOnFileSystemModels.ToListAsync();
            return viewModel;
        }
        #endregion

        #region File Upload into file, delete and download
        [HttpPost]
        public async Task<IActionResult> UploadToFileSystem(List<IFormFile> files, string description)
        {
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
                bool basePathExists = System.IO.Directory.Exists(basePath);

                if (!basePathExists)
                    Directory.CreateDirectory(basePath);

                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var filePath = Path.Combine(basePath, file.FileName);
                var extension = Path.GetExtension(file.FileName);

                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var fileModel = new FileOnFileSystemModel
                    {
                        CreatedOn = DateTime.UtcNow,
                        FileType = file.ContentType,
                        Extension = extension,
                        Name = fileName,
                        Description = description,
                        FilePath = filePath
                    };

                    _context.FileOnFileSystemModels.Add(fileModel);
                    _context.SaveChanges();
                }

            }

            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DownloadFileFromFileSystem(int id)
        {

            var file = await _context.FileOnFileSystemModels.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (file == null) return null;
            var memory = new MemoryStream();
            using (var stream = new FileStream(file.FilePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, file.FileType, file.Name + file.Extension);
        }

        public async Task<IActionResult> DeleteFileFromFileSystem(int id)
        {
            var file = await _context.FileOnFileSystemModels.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (file == null) 
                return null;

            if (System.IO.File.Exists(file.FilePath))
            {
                System.IO.File.Delete(file.FilePath);
            }

            _context.FileOnFileSystemModels.Remove(file);
            _context.SaveChanges();

            TempData["Message"] = $"Removed {file.Name + file.Extension} successfully from File System.";

            return RedirectToAction("Index");
        }

        #endregion

        #region File Upload into database, delete and download

        [HttpPost]
        public async Task<IActionResult> UploadToDatabase(List<IFormFile> files, string description)
        {
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);

                var fileModel = new FileOnDatabaseModel
                {
                    CreatedOn = DateTime.UtcNow,
                    FileType = file.ContentType,
                    Extension = extension,
                    Name = fileName,
                    Description = description
                };

                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    fileModel.Data = dataStream.ToArray();
                }

                _context.FileOnDatabaseModels.Add(fileModel);
                _context.SaveChanges();
            }

            TempData["Message"] = "File successfully uploaded to Database";

            return RedirectToAction("Index");
        }

        // File download system
        public async Task<IActionResult> DownloadFileFromDatabase(int id)
        {

            var file = await _context.FileOnDatabaseModels.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (file == null) return null;

            var stream = new MemoryStream(file.Data);

            stream.Position = 0;
            return File(stream, file.FileType, file.Name + file.Extension);
        }

        // For file delete
        public async Task<IActionResult> DeleteFileFromDatabase(int id)
        {
            var file = await _context.FileOnDatabaseModels.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.FileOnDatabaseModels.Remove(file);
            _context.SaveChanges();
            TempData["Message"] = $"Removed {file.Name + file.Extension} successfully from Database.";
            return RedirectToAction("Index");
        }

      
        #endregion
    }
}
