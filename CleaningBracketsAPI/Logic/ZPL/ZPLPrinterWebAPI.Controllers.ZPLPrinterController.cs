// ZPLPrinterWebAPI.Controllers.ZPLPrinterController
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Neodynamic.SDK.ZPLPrinter;
using ZPLPrinterWebAPI.Controllers;

/// <summary>
/// The ZPL Printer Web API.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ZPLPrinterController : ControllerBase
{
	private readonly string baseConfigPath = Directory.GetCurrentDirectory() + "/PrinterConfig";

	private readonly string fontsFileName = "fonts.json";

	private readonly ILogger<ZPLPrinterController> _logger;

	public ZPLPrinterController(ILogger<ZPLPrinterController> logger)
	{
		_logger = logger;
		if (!Directory.Exists(baseConfigPath))
		{
			Directory.CreateDirectory(baseConfigPath);
		}
	}

	/// <summary>
	/// Processes the ZPLPrintJob object and generates the output for the specified ZPL commands in the format specified to the Accept header.
	/// </summary>
	/// <param name="zplPrintJob">
	/// The ZPLPrintJOb object to be processed. It includes the ZPL commands as well as the printer settings.
	/// </param>
	/// <returns>
	/// Returns the rendering output for the specified ZPLPrintJob object.
	/// </returns>
	/// <response code="200">Returns the rendering output for the specified ZPL commands and printer settings.</response>
	/// <response code="500">If an error occurs during the ZPLPrintJob object processing.</response>
	[HttpPost]
	[ProducesResponseType(typeof(FileContentResult), 200)]
	[ProducesResponseType(typeof(ProblemDetails), 500)]
	[Produces("image/png", new string[]
	{
		"image/jpeg", "application/pdf", "image/vnd.zbrush.pcx", "application/vnd.zpl", "application/vnd.epl", "application/vnd.fingerprint", "application/vnd.escpos", "image/png+json", "image/jpeg+json", "application/pdf+json",
		"image/vnd.zbrush.pcx+json", "application/vnd.zpl+json", "application/vnd.epl+json", "application/vnd.fingerprint+json", "application/vnd.escpos+json", "image/png+zip", "image/jpeg+zip", "application/pdf+zip", "image/vnd.zbrush.pcx+zip", "application/vnd.zpl+zip",
		"application/vnd.epl+zip", "application/vnd.fingerprint+zip", "application/vnd.escpos+zip", "application/json"
	})]
	public IActionResult Post(ZPLPrintJob zplPrintJob)
	{
		ZPLPrinter zplPrinter = new ZPLPrinter(Environment.GetEnvironmentVariable("LICENSE_OWNER"), Environment.GetEnvironmentVariable("LICENSE_KEY"));
		DoPrinterConfig(ref zplPrinter);
		if (zplPrintJob.Dpi == 152 || zplPrintJob.Dpi == 203 || zplPrintJob.Dpi == 300 || zplPrintJob.Dpi == 600)
		{
			zplPrinter.Dpi = zplPrintJob.Dpi;
		}
		RenderOutputFormat outFormat = RenderOutputFormat.PNG;
		string acceptHeaderVal = "image/png";
		bool outJson = false;
		bool outZip = false;
		StringValues acceptHeader = base.HttpContext.Request.Headers["Accept"];
		if (acceptHeader.Count > 0)
		{
			acceptHeaderVal = acceptHeader[0];
			outJson = acceptHeaderVal.EndsWith("+json") || acceptHeaderVal.EndsWith("/json");
			outZip = acceptHeaderVal.EndsWith("+zip");
			if (acceptHeaderVal.StartsWith("image/png") || acceptHeaderVal.StartsWith("application/json"))
			{
				outFormat = RenderOutputFormat.PNG;
			}
			else if (acceptHeaderVal.StartsWith("image/jpeg"))
			{
				outFormat = RenderOutputFormat.JPG;
			}
			else if (acceptHeaderVal.StartsWith("application/pdf"))
			{
				outFormat = RenderOutputFormat.PDF;
			}
			else if (acceptHeaderVal.StartsWith("image/vnd.zbrush.pcx"))
			{
				outFormat = RenderOutputFormat.PCX;
			}
			else if (acceptHeaderVal.StartsWith("text/plain") || acceptHeaderVal.StartsWith("application/vnd.zpl"))
			{
				outFormat = RenderOutputFormat.GRF;
			}
			else if (acceptHeaderVal.StartsWith("application/vnd.epl"))
			{
				outFormat = RenderOutputFormat.EPL;
			}
			else if (acceptHeaderVal.StartsWith("application/vnd.fingerprint"))
			{
				outFormat = RenderOutputFormat.FP;
			}
			else if (acceptHeaderVal.StartsWith("application/vnd.escpos"))
			{
				outFormat = RenderOutputFormat.NV;
			}
		}
		zplPrinter.RenderOutputFormat = outFormat;
		if (zplPrintJob.RenderOutputRotation > 0)
		{
			if (zplPrintJob.RenderOutputRotation == 90)
			{
				zplPrinter.RenderOutputRotation = RenderOutputRotation.Rot90Clockwise;
			}
			if (zplPrintJob.RenderOutputRotation == 180)
			{
				zplPrinter.RenderOutputRotation = RenderOutputRotation.Rot180;
			}
			if (zplPrintJob.RenderOutputRotation == 270)
			{
				zplPrinter.RenderOutputRotation = RenderOutputRotation.Rot270Clockwise;
			}
		}
		zplPrinter.AntiAlias = zplPrintJob.AntiAlias;
		if (zplPrintJob.LabelWidthInchUnit > 0f)
		{
			zplPrinter.LabelWidth = zplPrintJob.LabelWidthInchUnit * zplPrinter.Dpi;
		}
		if (zplPrintJob.LabelHeightInchUnit > 0f)
		{
			zplPrinter.LabelHeight = zplPrintJob.LabelHeightInchUnit * zplPrinter.Dpi;
		}
		if (!string.IsNullOrWhiteSpace(zplPrintJob.LabelBackColorHex))
		{
			try
			{
				zplPrinter.LabelBackColor = zplPrintJob.LabelBackColorHex;
			}
			catch
			{
			}
		}
		if (!string.IsNullOrWhiteSpace(zplPrintJob.RibbonColorHex))
		{
			try
			{
				zplPrinter.RibbonColor = zplPrintJob.RibbonColorHex;
			}
			catch
			{
			}
		}
		if (!string.IsNullOrWhiteSpace(zplPrintJob.BackgroundImageBase64))
		{
			try
			{
				zplPrinter.BackgroudImageBase64 = zplPrintJob.BackgroundImageBase64;
			}
			catch
			{
			}
		}
		if (!string.IsNullOrWhiteSpace(zplPrintJob.WatermarkImageBase64))
		{
			try
			{
				zplPrinter.WatermarkImageBase64 = zplPrintJob.WatermarkImageBase64;
				zplPrinter.WatermarkOpacity = zplPrintJob.WatermarkOpacity;
			}
			catch
			{
			}
		}
		if (!string.IsNullOrWhiteSpace(zplPrintJob.PdfMetadataAuthor))
		{
			zplPrinter.PdfMetadataAuthor = zplPrintJob.PdfMetadataAuthor;
		}
		if (!string.IsNullOrWhiteSpace(zplPrintJob.PdfMetadataCreator))
		{
			zplPrinter.PdfMetadataCreator = zplPrintJob.PdfMetadataCreator;
		}
		if (!string.IsNullOrWhiteSpace(zplPrintJob.PdfMetadataProducer))
		{
			zplPrinter.PdfMetadataProducer = zplPrintJob.PdfMetadataProducer;
		}
		if (!string.IsNullOrWhiteSpace(zplPrintJob.PdfMetadataSubject))
		{
			zplPrinter.PdfMetadataSubject = zplPrintJob.PdfMetadataSubject;
		}
		if (!string.IsNullOrWhiteSpace(zplPrintJob.PdfMetadataTitle))
		{
			zplPrinter.PdfMetadataTitle = zplPrintJob.PdfMetadataTitle;
		}
		if (zplPrintJob.ThumbnailSizePixelUnit > 0)
		{
			zplPrinter.ThumbnailSize = zplPrintJob.ThumbnailSizePixelUnit;
		}
		zplPrinter.ForceLabelWidth = zplPrintJob.ForceLabelWidth;
		zplPrinter.ForceLabelHeight = zplPrintJob.ForceLabelHeight;
		zplPrinter.DrawRFID = zplPrintJob.DrawRFID;
		zplPrinter.InvertColors = zplPrintJob.InvertColors;
		zplPrinter.CompressionQuality = zplPrintJob.CompressionQuality;
		Encoding zplCmdsEncoding = Encoding.GetEncoding(850);
		if (zplPrintJob.ZplCommandsCodePage > 0)
		{
			try
			{
				zplCmdsEncoding = Encoding.GetEncoding(zplPrintJob.ZplCommandsCodePage);
			}
			catch
			{
			}
		}
		try
		{
			List<byte[]> buffer = null;
			buffer = ((!zplPrintJob.IsZplCommandsBase64) ? zplPrinter.ProcessCommands(zplPrintJob.ZplCommands, zplCmdsEncoding, zplPrintJob.TraceRenderedElements) : zplPrinter.ProcessCommands(Convert.FromBase64String(zplPrintJob.ZplCommands), zplCmdsEncoding, zplPrintJob.TraceRenderedElements));
			if (outZip)
			{
				return new FileContentResult(CompressBuffer(ref buffer, zplPrinter.RenderOutputFormat), acceptHeaderVal);
			}
			if (outJson)
			{
				string tracedElements = ((zplPrinter.RenderedElements != null && zplPrinter.RenderedElements.Count > 0) ? zplPrinter.RenderedElementsAsJson : null);
				return new FileContentResult(JsonBuffer(ref buffer, zplPrinter.RenderOutputFormat, tracedElements), acceptHeaderVal);
			}
			return new FileContentResult(buffer[0], acceptHeaderVal);
		}
		catch (Exception ex)
		{
			return Problem(ex.Message, null, 500);
		}
	}

	private byte[] CompressBuffer(ref List<byte[]> buffer, RenderOutputFormat outFormat)
	{
		int j = buffer.Count;
		int zeros = (j + 1).ToString().Length;
		using MemoryStream outStream = new MemoryStream();
		using (ZipArchive archive = new ZipArchive(outStream, ZipArchiveMode.Create, leaveOpen: true))
		{
			for (int i = 0; i < j; i++)
			{
				ZipArchiveEntry fileInArchive = archive.CreateEntry("Label_" + (i + 1).ToString().PadLeft(zeros, '0') + "." + outFormat.ToString().ToLower(), CompressionLevel.Optimal);
				using Stream entryStream = fileInArchive.Open();
				using MemoryStream fileToCompressStream = new MemoryStream(buffer[i]);
				fileToCompressStream.CopyTo(entryStream);
			}
		}
		return outStream.ToArray();
	}

	private byte[] JsonBuffer(ref List<byte[]> buffer, RenderOutputFormat outFormat, string tracedElements)
	{
		string dataUri = "data:";
		switch (outFormat)
		{
		case RenderOutputFormat.PNG:
			dataUri += "image/png;base64,";
			break;
		case RenderOutputFormat.JPG:
			dataUri += "image/jpeg;base64,";
			break;
		case RenderOutputFormat.PDF:
			dataUri += "application/pdf;base64,";
			break;
		case RenderOutputFormat.PCX:
			dataUri += "image/vnd.zbrush.pcx;base64,";
			break;
		case RenderOutputFormat.GRF:
			dataUri += "application/vnd.zpl;base64,";
			break;
		case RenderOutputFormat.EPL:
			dataUri += "application/vnd.epl;base64,";
			break;
		case RenderOutputFormat.FP:
			dataUri += "application/vnd.fingerprint;base64,";
			break;
		case RenderOutputFormat.NV:
			dataUri += "application/vnd.escpos;base64,";
			break;
		}
		StringBuilder json = new StringBuilder();
		json.Append("{\"labels\":[");
		int j = buffer.Count;
		for (int i = 0; i < j; i++)
		{
			json.Append("\"");
			json.Append(dataUri);
			json.Append(Convert.ToBase64String(buffer[i]));
			json.Append("\"");
			if (i + 1 < j)
			{
				json.Append(",");
			}
		}
		json.Append("]");
		if (!string.IsNullOrWhiteSpace(tracedElements))
		{
			json.Append(",");
			json.Append("\"renderedElementsPerLabel\":");
			json.Append(tracedElements);
		}
		json.Append("}");
		return Encoding.ASCII.GetBytes(json.ToString());
	}

	private void DoPrinterConfig(ref ZPLPrinter printer)
	{
		try
		{
			string fonts_json = baseConfigPath + "/" + fontsFileName;
			if (!System.IO.File.Exists(fonts_json))
			{
				return;
			}
			CustomFont[] customFonts = JsonSerializer.Deserialize<CustomFont[]>(System.IO.File.ReadAllText(fonts_json), new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
			if (customFonts != null && customFonts.Length != 0)
			{
				CustomFont[] array = customFonts;
				foreach (CustomFont cf in array)
				{
					printer.AddFont(cf.Name, baseConfigPath + "/" + cf.FileName);
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
}
