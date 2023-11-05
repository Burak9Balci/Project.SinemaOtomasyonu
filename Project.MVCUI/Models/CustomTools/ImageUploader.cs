using System;
using System.IO;
using System.Web;

public static class ImageUploader
{
    public static string UploadImage(string serverPath, HttpPostedFileBase file, string name)
    {
        if (file != null)
        {
            Guid uniqueName = Guid.NewGuid();

            string[] fileArray = file.FileName.Split('.');

            string extension = fileArray[fileArray.Length - 1].ToLower();
            string fileName = $"{uniqueName}.{name}.{extension}";
            if (extension == "jpg" || extension == "jpeg" || extension == "gif" || extension == "png")
            {
                if (File.Exists(HttpContext.Current.Server.MapPath(serverPath + fileName)))
                {
                    return "1"; // guid kullandığımız için aynı dosya zaten olmaz ama eger 1 olursa o ısımde resim vardır
                }
                else
                {
                    string filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
                    file.SaveAs(filePath);
                    return $"{serverPath}{fileName}";
                }
            }
            else
            {
                return "2"; // Secilen dosya bir resim değildir
            }
        }
        else
        {
            return "3"; // dosya bulunamadı
        }
    }
}
