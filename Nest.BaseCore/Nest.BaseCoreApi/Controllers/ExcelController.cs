using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.Domain.ResponseModel;
using OfficeOpenXml;

namespace Nest.BaseCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        [HttpPost]
        [Route("Export")]
        public IActionResult Export()
        {
            var list = new List<Test>();

            list.Add(new Test()
            {
                Id = 1,
                Name = "Test",
                Age = 22,

            });

            byte[] fileContents;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(".Net Core 导出");
                worksheet.Cells[1, 1].Value = "序号";
                worksheet.Cells[1, 2].Value = "Id";
                worksheet.Cells[1, 3].Value = "名称";
                worksheet.Cells[1, 4].Value = "年龄";

                int i = 2;

                foreach (var item in list)
                {
                    worksheet.Cells["A" + i].Value = i - 1;
                    worksheet.Cells["B" + i].Value = item.Id;
                    worksheet.Cells["C" + i].Value = item.Name;
                    worksheet.Cells["D" + i].Value = item.Age;
                    i = i + 1;
                }

                fileContents = package.GetAsByteArray();
                if (fileContents == null || fileContents.Length == 0)
                {
                    return NotFound();
                }
            }
            return File(fileContents, "application/ms-excel", $"{Guid.NewGuid().ToString()}.xlsx");
        }

        [HttpPost]
        [Route("Import")]
        public async Task<IActionResult> Import(IFormFile excelFile)
        {

            var msg = "";
            if (excelFile == null || excelFile.Length <= 0)
            {
                msg = "请选择导入文件!";
                return Ok(msg);
            }


            if (!Path.GetExtension(excelFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                msg = "请选择导入文件为.xlsx的后缀名!";
                return Ok(msg);
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await excelFile.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        StringBuilder sb = new StringBuilder();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        int rowCount = worksheet.Dimension.Rows;
                        int ColCount = worksheet.Dimension.Columns;
                        bool bHeaderRow = true;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            Test model = new Test();
                            for (int col = 1; col <= ColCount; col++)
                            {
                                if (bHeaderRow)
                                {
                                    switch (col)
                                    {
                                        case 1:
                                            model.Id = int.Parse(worksheet.Cells[row, col].Value.ToString());
                                            break;
                                        case 2:
                                            model.Name = worksheet.Cells[row, col].Value.ToString();
                                            break;
                                        case 3:
                                            model.Age = int.Parse(worksheet.Cells[row, col].Value.ToString());
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (col)
                                    {
                                        case 1:
                                            model.Id = int.Parse(worksheet.Cells[row, col].Value.ToString());
                                            break;
                                        case 2:
                                            model.Name = worksheet.Cells[row, col].Value.ToString();
                                            break;

                                        case 3:
                                            model.Age = int.Parse(worksheet.Cells[row, col].Value.ToString());
                                            break;
                                    }
                                }
                            }

                            //插入model即可
                        }
                    }
                }
                msg = "导入成功!";
                return Ok(msg);
            }

            catch (Exception ex)
            {
                msg = ex.Message;
                return Ok(msg);
            }
        }
    }

    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}