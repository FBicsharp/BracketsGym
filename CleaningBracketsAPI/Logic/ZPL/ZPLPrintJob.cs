// ZPLPrintJob
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// It specifies a ZPL Print Job including the ZPL commands to be processed and the printer settings.
/// </summary>
public class ZPLPrintJob
{
	/// <summary>
	/// The printer resolution in DPI (Dots per inch) value. Valid values are: 152 (6 dpmm), 203 (8 dpmm), 300 (12 dpmm) or 600 (24 dpmm).
	/// </summary>
	/// <example>203</example>
	[DefaultValue(203)]
	public int Dpi { get; set; } = 203;


	/// <summary>
	/// The ZPL commands to be processed. The string containing the ZPL commands is converted to an array of bytes prior processing it. That conversion will use CodePage or Encoding 850 (Latin Character Set).
	/// </summary>
	/// <example>^XA^FO30,40^ADN,36,20^FDHello World^FS^FO30,80^BY4^B3N,,200^FD12345678^FS^XZ</example>
	[Required]
	public string ZplCommands { get; set; } = "";


	/// <summary>
	/// The Encoding or CodePage of the ZPL string content. Default is CodePage 850 (Latin Character Set). Valid CodePage numbers are listed here https://github.com/neodynamic/ZPL-Printer-Web-API-Docker/blob/master/README.md#supported-codepagesencodings
	/// </summary>
	/// <example>850</example>
	[DefaultValue(850)]
	public int ZplCommandsCodePage { get; set; } = 850;


	/// <summary>
	/// Whether the specified ZPL Commands string is in Base64 format. Useful for ZPL commands containing non-readable/printable chars. Default is false.
	/// </summary>
	/// <example>false</example>
	[DefaultValue(false)]
	public bool IsZplCommandsBase64 { get; set; }

	/// <summary>
	/// The rotation for the output rendering process. Default is 0 (None). Valid values are: 0 (None), 90 (Clockwise), 180, 270 (Clockwise).
	/// </summary>
	/// <example>0</example>
	public int RenderOutputRotation { get; set; }

	/// <summary>
	/// Whether anti-aliasing rendering is enabled. Default is true.
	/// </summary>
	/// <example>true</example>
	[DefaultValue(true)]
	public bool AntiAlias { get; set; } = true;


	/// <summary>
	/// The default label width in Inch units. Default is 4in.
	/// </summary>
	/// <example>4</example>
	[DefaultValue(4)]
	public float LabelWidthInchUnit { get; set; } = 4f;


	/// <summary>
	/// The default label height in Inch units. Default is 6in.
	/// </summary>
	/// <example>6</example>
	[DefaultValue(6)]
	public float LabelHeightInchUnit { get; set; } = 6f;


	/// <summary>
	/// The label's background color in hex notation. Default is #fff (White).
	/// </summary>
	/// <example></example>
	public string LabelBackColorHex { get; set; } = "";


	/// <summary>
	/// The label's ribbon color in hex notation. Default is #000 (Black).
	/// </summary>
	/// <example></example>
	[DefaultValue("")]
	public string RibbonColorHex { get; set; } = "";


	/// <summary>
	/// The label's custom background image from a string in Base64 format. Image format mut be PNG or JPG.
	/// </summary>
	/// <example></example>
	[DefaultValue("")]
	public string BackgroundImageBase64 { get; set; } = "";


	/// <summary>
	/// Gets or sets the label's watermark image from a string in Base64 format. Image format mut be PNG or JPG.
	/// </summary>
	/// <example></example>
	public string WatermarkImageBase64 { get; set; } = "";


	/// <summary>
	/// Gets or sets the label's watermark image opacity level. Values range from 0 to 100. Default is 50.
	/// </summary>
	/// <example>50</example>
	public int WatermarkOpacity { get; set; } = 50;


	/// <summary>
	/// Gets or sets whether a RFID image must be drawn on the label. Default is true.
	/// </summary>
	/// <remarks>
	/// The RFID will be drawn if any supported RFID commands are found in the label.
	/// </remarks>
	/// <example>true</example>
	public bool DrawRFID { get; set; } = true;


	/// <summary>
	/// The thumbnail size for the output rendering in Pixel unit. Default is 0 (zero) which means thumbnail generation is disabled.
	/// </summary>
	/// <example>0</example>
	[DefaultValue(0)]
	public int ThumbnailSizePixelUnit { get; set; }

	/// <summary>
	/// The PDF Metadata Author attribute.
	/// </summary>
	/// <example></example>
	[DefaultValue("")]
	public string PdfMetadataAuthor { get; set; } = "";


	/// <summary>
	/// The PDF Metadata Creator attribute.
	/// </summary>
	/// <example></example>
	[DefaultValue("")]
	public string PdfMetadataCreator { get; set; } = "";


	/// <summary>
	/// The PDF Metadata Producer attribute.
	/// </summary>
	/// <example></example>
	[DefaultValue("")]
	public string PdfMetadataProducer { get; set; } = "";


	/// <summary>
	/// The PDF Metadata Subject attribute.
	/// </summary>
	/// <example></example>
	[DefaultValue("")]
	public string PdfMetadataSubject { get; set; } = "";


	/// <summary>
	/// The PDF Metadata Title attribute.
	/// </summary>
	/// <example></example>
	[DefaultValue("")]
	public string PdfMetadataTitle { get; set; } = "";


	/// <summary>
	/// Gets or sets whether the LabelHeight property value must override the ^LL command. Default is false.
	/// </summary>
	/// <example>false</example>
	[DefaultValue(false)]
	public bool ForceLabelHeight { get; set; }

	/// <summary>
	/// Gets or sets whether the LabelWidth property value must override the ^PW command. Default is false.
	/// </summary>
	/// <example>false</example>
	[DefaultValue(false)]
	public bool ForceLabelWidth { get; set; }

	/// <summary>
	/// Gets or sets whether to invert the colors of the output image. Default is false.
	/// </summary>
	/// <example>false</example>
	[DefaultValue(false)]
	public bool InvertColors { get; set; }

	/// <summary>
	/// Gets or sets the compression quality level to use for the output image. Values range from 0 to 100.
	/// </summary>
	/// <example>100</example>
	[DefaultValue(100)]
	public int CompressionQuality { get; set; } = 100;


	/// <summary>
	/// Gets or sets whether to trace the ZPL elements rendered based on the ZPL commands to be processed. Only available for JSON output. Default is false.
	/// </summary>
	/// <example>false</example>
	[DefaultValue(false)]
	public bool TraceRenderedElements { get; set; }
}
