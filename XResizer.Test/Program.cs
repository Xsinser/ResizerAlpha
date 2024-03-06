using Microsoft.Extensions.Logging;
using ImResizer.Models.ESRGANSR4;
using SkiaSharp;
using static System.Net.Mime.MediaTypeNames;

namespace XResizer.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger("Program");
            var processor = new EsrganSRFourProcessor(
                Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("OnnxModels", "ESRGAN_SRx4_DF2KOST_official-ff704c303320.onnx")),//// Путь до обученной либы нейронки
                1,//// Количество паралельных потоков для увеличения разрешения в рамках одного Resize
                logger);/// Logger

            var originalImage =  SKBitmap.Decode(("old.jpg"));
            var resultImage = processor.Resize(originalImage).Result;

            using (var data = resultImage.Encode(SKEncodedImageFormat.Jpeg, 100))
            using (var stream = File.OpenWrite("new.jpg"))
            {
                data.SaveTo(stream);
            }
        }
    }
}