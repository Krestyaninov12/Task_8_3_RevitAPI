using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_8_3
{//
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;


            using (var ts = new Transaction(doc, "export image"))
            {
                ts.Start();

                var desktop_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var view = doc.ActiveView;
                var filepath = Path.Combine(desktop_path, view.Name);

                var imageOption = new ImageExportOptions
                {
                    ZoomType = ZoomFitType.FitToPage,
                    PixelSize = 1024,
                    FilePath = filepath,
                    FitDirection = FitDirectionType.Horizontal,
                    HLRandWFViewsFileType = ImageFileType.JPEGLossless,
                    ImageResolution = ImageResolution.DPI_600,
                    ExportRange = ExportRange.CurrentView,
                    ViewName = "Level 1",
                };
                doc.ExportImage(imageOption);

                ts.Commit();
            }
            return Result.Succeeded;

        }
    }
}
