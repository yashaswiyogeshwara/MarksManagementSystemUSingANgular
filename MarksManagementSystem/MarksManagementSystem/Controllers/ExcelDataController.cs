using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarksManagementSystem.DAL;
using MarksManagementSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MarksManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelDataController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        public readonly DataContext _context;
        public ExcelDataController(IHostingEnvironment hostingEnvironment, DataContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }


        [HttpPost("AddStudents")]
        public async Task<IActionResult> Upload(List<IFormFile> _file)
        {
            try
            {
                IFormFile file = Request.Form.Files[0];
                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                StringBuilder sb = new StringBuilder();
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                    ISheet sheet;
                    string fullPath = Path.Combine(newPath, file.FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        stream.Position = 0;
                        if (sFileExtension == ".xls")
                        {
                            HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                        }
                        else
                        {
                            XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                        }
                        IRow headerRow = sheet.GetRow(0); //Get Header Row
                        int cellCount = headerRow.LastCellNum;
                        //sb.Append("<table class='table'><tr>");
                        //for (int j = 0; j < cellCount; j++)
                        //{
                        //    NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        //    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        //    sb.Append("<th>" + cell.ToString() + "</th>");
                        //}
                        //sb.Append("</tr>");
                        //sb.AppendLine("<tr>");
                        using (DataContext dbContext = _context)
                        {
                            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                            {
                                Student student = new Student();
                                IRow row = sheet.GetRow(i);
                                if (row == null || row.Cells.All(d => d.CellType == CellType.Blank))
                                {
                                    throw new Exception(string.Format("Null row presnet in excel at row number , {0} ", i.ToString()));
                                };
                                for (int j = row.FirstCellNum; j < cellCount; j++)
                                {
                                    ICell cell = row.Cells.First(x => x.RowIndex == i && x.ColumnIndex == j);
                                    switch (j)
                                    {
                                        case 0:
                                            student.Hallticket = cell.StringCellValue;
                                            break;
                                        case 1:
                                            student.Yearofjoin = Convert.ToInt32(cell.NumericCellValue);
                                            break;
                                        case 2:
                                            student.Dept = cell.StringCellValue;
                                            break;
                                        case 3:
                                            student.Section = cell.NumericCellValue.ToString();
                                            break;

                                    }
                                }
                                _context.Students.Add(student);
                                //sb.AppendLine("</tr>");
                            }
                            _context.SaveChanges();
                        }

                        //sb.Append("</table>");
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new { mess = ex.Message });
            }

            return Ok(new { mess = "Successfully loaded data" });
        }
    }
}