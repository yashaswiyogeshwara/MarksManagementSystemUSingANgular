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
    public class MarksExcelData : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        public readonly DataContext _context;
        public MarksExcelData(IHostingEnvironment hostingEnvironment, DataContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }


        [HttpPost("AddMarks")]
        public async Task<IActionResult> Upload(List<IFormFile> _file)
        {
            try
            { int tempstuid = 0, tempsubid = 0;
               // String tempsubcode = "";
               Marks marks = new Marks();
                Student student = new Student();
                Subject subject = new Subject();
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
                                 
                                IRow row = sheet.GetRow(i);
                                if (row == null || row.Cells.All(d => d.CellType == CellType.Blank))
                                {
                                    throw new Exception(string.Format("Null row presnet in excel at row number , {0} ", i.ToString()));
                                };
                               
                                   
                                    Type type = typeof(Student);
                                    int NumberOfRecordsOfStudent = type.GetProperties().Length;

                                    Type types = typeof(Subject);
                                    int NumberOfRecordsOfsubject = types.GetProperties().Length;

                                    for (int k=0;k< NumberOfRecordsOfStudent; k++)
                                    {
                                        for (int m = 0; m < NumberOfRecordsOfsubject; m++)
                                        {
                                            if (row.ElementAt(0).Equals(student.Hallticket))
                                                {
                                                 tempstuid = student.Id;
                                                if (row.ElementAt(2).Equals(subject.Name))
                                                    {
                                                     tempsubid = subject.Id;
                                                   // tempsubcode = subject.Code;
                                                   }

                                                 }
                                            for (int j = row.FirstCellNum; j < cellCount; j++)
                                            {
                                                ICell cell = row.Cells.First(x => x.RowIndex == i && x.ColumnIndex == j);

                                                switch (j)
                                                {
                                                    case 0:

                                                        marks.StuId = tempstuid;
                                                        break;

                                                    case 2:
                                                        marks.SubId = tempsubid;

                                                        break;
                                                    case 3:
                                                        marks.Grade = cell.StringCellValue;

                                                        break;
                                                    case 4:
                                                        marks.GradePoint = Convert.ToInt32(cell.NumericCellValue);
                                                        break;
                                                }
                                            }
                                        }

                                    }


                                
                                _context.Marks.Add(marks);
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
