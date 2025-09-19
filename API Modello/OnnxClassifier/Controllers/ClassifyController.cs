using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.Drawing;
using System.Reflection.Emit;

[ApiController]
[Route("classify")]
public class ClassifyController : ControllerBase
{
    private readonly InferenceSession _session;

    public ClassifyController()
    {
        _session = new InferenceSession("models/model.onnx");

        foreach (var input in _session.InputMetadata)
        {
            Console.WriteLine($"Input Name: {input.Key}, Type: {input.Value.ElementType}, Dimensions: {string.Join(",", input.Value.Dimensions)}");
        }

        foreach (var output in _session.OutputMetadata)
        {
            Console.WriteLine($"Output Name: {output.Key}, Type: {output.Value.ElementType}, Dimensions: {string.Join(",", output.Value.Dimensions)}");
        }
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Post([FromForm] IFormFile file)
    {
        Console.WriteLine("Richiesta ricevuta.");

        if (file == null || file.Length == 0)
        {
            Console.WriteLine("Nessun file caricato.");
            return BadRequest(new { error = "No file uploaded." });
        }

        try
        {
            using var stream = file.OpenReadStream();
            using var bitmap = new Bitmap(stream);

            var input = PreprocessImage(bitmap);
            var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("keras_tensor", input)
        };

            using var results = _session.Run(inputs);
            var output = results.First().AsEnumerable<float>().ToArray();

            int predictedLabel = Array.IndexOf(output, output.Max());

            Console.WriteLine($"Predizione: {predictedLabel}");

            string labelText;

            if (predictedLabel == 0)
                labelText = "Limite 50";
            else if (predictedLabel == 1)
                labelText = "Limite 30";
            else if (predictedLabel == 2)
                labelText = "Limite 100";
            else
                labelText = "Unknown";

            return Ok(new
            {
                label = labelText,

                confidence = output.Max()
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore: " + ex.Message);
            return StatusCode(500, new { error = ex.Message });
        }
    }

    /*[HttpGet]
    public IActionResult Get()
    {
        return Ok("API attiva su /classify");
    }*/



    private DenseTensor<float> PreprocessImage(Bitmap image)
    {
        int width = 64, height = 64;
        var resized = new Bitmap(image, new Size(width, height));
        var tensor = new DenseTensor<float>(new[] { 1, height, width, 3 });

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                var pixel = resized.GetPixel(x, y);
                tensor[0, y, x, 0] = pixel.R / 255f;
                tensor[0, y, x, 1] = pixel.G / 255f;
                tensor[0, y, x, 2] = pixel.B / 255f;
            }

        return tensor;
    }
}
