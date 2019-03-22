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

        // 
        [HttpPost("AddMarks")]
        public async Task<IActionResult> Upload(List<IFormFile> _file)
        {
            try
            {
                bool accessNotAllowed =
                String.Equals(Request.Headers.First(x => x.Key == "Authorization").Value, "unauth");
                if (accessNotAllowed)
                {
                    return Ok(new { success = false, mess = "Not authenticated" });
                }
                List<MarksExcel> excelMarksList = new List<MarksExcel>();
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
                                //Hallticket No	Subject Code	Subject Name	Grade	Grade Points			
                                IRow row = sheet.GetRow(i);
                                string Hallticket = row.GetCell(0).StringCellValue;
                                string SubjectCode = row.GetCell(1).StringCellValue;
                                string SubjectName = row.GetCell(2).StringCellValue;
                                string Grade = row.GetCell(3).StringCellValue;
                                int GradePoints = (int)(row.GetCell(4).NumericCellValue);
                                excelMarksList.Add(new MarksExcel() {
                                    Hallticket = row.GetCell(0).StringCellValue,
                                    SubjectCode = row.GetCell(1).StringCellValue,
                                    SubjectName = row.GetCell(2).StringCellValue,
                                    Grade = row.GetCell(3).StringCellValue,
                                    GradePoints = (int)(row.GetCell(4).NumericCellValue)
                                }); 

                            }

                            /*
                              public int Id { get; set; }
                              public int SubjectId { get; set; }
                              public int StudentId { get; set; }
                              public int StandardId { get; set; }
                              public string Grade { get; set; }
                              public int GradePoint { get; set; }
                             */

                            List<Marks> marksList = (from temp in excelMarksList
                                                     join stu in _context.Students
                                                     on temp.Hallticket equals stu.Hallticket
                                                     join sub in _context.Subject on temp.SubjectCode equals sub.Code
                                                     join std in _context.Standard on sub.StandardId equals std.Id
                                                     select new Marks
                                                     {
                                                         SubjectId = sub.Id,
                                                         StudentId = stu.Id,
                                                         StandardId = std.Id,
                                                         Grade = temp.Grade,
                                                         GradePoint = temp.GradePoints
                                                     }).ToList<Marks>();

                            List<Marks> dbMarksList = (from m in _context.Marks
                                                       select m).ToList<Marks>();
                            List<Student> students = (from s in _context.Students
                                                      select s).ToList<Student>(); 
                            marksList.ForEach(mrk =>
                            {
                                Marks record = dbMarksList.Where(m => m.StudentId == mrk.StudentId && m.SubjectId == mrk.SubjectId).FirstOrDefault();
                                if (record != null)
                                {
                                    record.Grade = mrk.Grade;
                                    record.GradePoint = mrk.GradePoint;
                                    _context.Marks.Update(record);
                                }
                                else
                                {
                                    if (mrk.Grade.ToLower() == "f") {
                                        Student _student = students.Where(s => s.Id == mrk.StudentId).FirstOrDefault();
                                        _student.NAAC++;
                                        _context.Students.Update(_student);
                                    }
                                    _context.Marks.Add(mrk);
                                }
                            });
                           _context.SaveChanges();

                        }

                        //sb.Append("</table>");
                    }
                }

            }
            catch (Exception ex)
            {
                return Ok(new { success = false, mess = ex.Message });
            }

            return Ok(new { success = true, mess = "Successfully loaded data" });
        }
    }


}
